using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 墙壁悬挂，类似刺客信条里的爬墙悬挂吧
/// </summary>
public class LedgeGrabbing : MonoBehaviour
{
    [Header("Peferences")]
    public PlayerMovement pm;
    public Transform orientation;
    public Transform cam;
    private Rigidbody rb;

    [Header("Ledge Grabbing")]
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;

    public float minTimeOnLedge;
    private float timeOnLedge;

    public bool holding;

    [Header("Ledge Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float ledgeJumpForwardForce;
    public float ledgeJumpUpWardForce;

    [Header("Ledge Detection")]
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private Transform lastLedge;
    private Transform currLedge;

    private RaycastHit ledgeHit;

    [Header("Exiting")]
    public bool exitingLedge;
    public float exitLedgeTime;
    private float exitLedgeTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LedgeDetection();
        SubStateMachine();
    }

    private void SubStateMachine()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool anyInputKeyPressed = horizontalInput != 0 || verticalInput != 0;

        if (holding)
        {
            FreezeRigidbodyOnLegde();

            timeOnLedge += Time.deltaTime;

            if (timeOnLedge > minTimeOnLedge && anyInputKeyPressed)
            {
                ExitLedgeHold();
            }

            if (Input.GetKeyDown(jumpKey))
            {
                LedgeJump();
            }
        }
        else if (exitingLedge)
        {
            if (exitLedgeTimer > 0)
            {
                exitLedgeTimer -= Time.deltaTime;
            }
            else
            {
                exitingLedge = false;
            }
        }
    }

    private void LedgeDetection()
    {
        bool ledgeDetected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, whatIsLedge);

        if (!ledgeDetected)
        {
            return;
        }

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (ledgeHit.transform == lastLedge) return;

        if (distanceToLedge < maxLedgeGrabDistance && !holding) EnterLedgeHold();
    }

    private void LedgeJump()
    {
        ExitLedgeHold();

        Invoke(nameof(DelayedJumpForce), 0.05f);
    }

    private void DelayedJumpForce()
    {
        Vector3 forceToAdd = cam.forward * ledgeJumpForwardForce + orientation.up * ledgeJumpUpWardForce;
        rb.velocity = Vector3.zero;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    /// <summary>
    /// 进入爬墙模式，取消重力，消除velocity
    /// </summary>
    private void EnterLedgeHold()
    {
        exitingLedge = true;
        exitLedgeTimer = exitLedgeTime;

        holding = true;

        pm.unlimited = true;
        pm.restricted = true;

        currLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

    private void FreezeRigidbodyOnLegde()
    {
        rb.useGravity = false;

        Vector3 directionToLedge = currLedge.position - transform.position;
        float distanceToLedge = Vector3.Distance(transform.position, currLedge.position);

        if (distanceToLedge > 1f)
        {
            if (rb.velocity.magnitude < moveToLedgeSpeed)
            {
                rb.AddForce(directionToLedge.normalized * moveToLedgeSpeed * Time.deltaTime * 1000f);
            }
        }
        else
        {
            if (!pm.freeze)
                pm.freeze = true;
            if (pm.unlimited)
                pm.unlimited = false;
        }

        if (distanceToLedge > maxLedgeGrabDistance)
        {
            ExitLedgeHold();
        }
    }

    private void ExitLedgeHold()
    {
        holding = false;
        timeOnLedge = 0;

        pm.restricted = false;
        pm.freeze = false;
        pm.unlimited = false;

        rb.useGravity = true;

        StopAllCoroutines();
        Invoke(nameof(RestartLastLedge), 1f);
    }

    public void RestartLastLedge()
    {
        lastLedge = null;
    }
}
