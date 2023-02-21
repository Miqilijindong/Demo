using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovementGrappling : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    /// <summary>
    /// ����ٶ�
    /// </summary>
    public float sprintSpeed;
    /// <summary>
    ///  �����ٶ�
    /// </summary>
    public float slideSpeed;
    public float wallRunSpeed;
    public float climbSpeed;
    public float vaultSpeed;
    public float airMinSpeed;
    /// <summary>
    /// �����ٶ�
    /// </summary>
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYSpeed;

    /// <summary>
    /// �趨������
    /// </summary>
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    /// <summary>
    /// ��������
    /// </summary>
    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    /// <summary>
    /// �����ж�
    /// </summary>
    [Header("Ground Check")]
    //[Tooltip("�����ж�")]// �༭��ע��
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Camera Effects")]
    public PlayerCam cam;
    public float grappleFov = 95f;

    [Header("References")]
    public Climbing climbingScript;

    public Transform orientation;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        wallruning,
        climbing,
        vaulting,
        crouching,
        dashing,
        sliding,
        air
    }

    public bool sliding;
    public bool crouching;
    /// <summary>
    /// �Ƿ��ڿ�ǽ��
    /// </summary>
    public bool wallrunning;
    public bool climbing;
    public bool vaulting;
    /// <summary>
    /// ��ֹ
    /// </summary>
    public bool freeze;
    public bool unlimited;
    public bool dashing;

    public bool restricted;

    public bool activeGrapple;

    public TMP_Text text_speed;
    public TMP_Text text_mode;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        playerHeight = GetComponent<CapsuleCollider>().height;

        readyToJump = true;

        startYScale = transform.localScale.y;

        rb.freezeRotation = true;
        //-None          ��Ӧ�ò�ֵ��
        //-Interpolate   ����ǰһ֡�ı任��ƽ���任��
        //-Extrapolate   ������һ֡�Ĺ��Ʊ任��ƽ���任��
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        //Collision Detection ���ڷ�ֹ�����ƶ��Ķ��󴩹�����������������ײ��
        //-Discrete                 �Գ����е�����������ײ��ʹ����ɢ��ײ��⡣������ײ���ڲ�����ײʱ��ʹ����ɢ��ײ��⡣����������ײ������Ĭ��ֵ����
        //-Continuous               �Զ�̬��ײ�壨���и��壩ʹ����ɢ��ײ��⣬���Ծ�̬��ײ�壨û�и��壩ʹ�û���ɨ�ӵ�������ײ��⡣����Ϊ__������̬(Continuous Dynamic)__ �ĸ��彫�ڲ�����ø������ײʱʹ��������ײ��⡣�������彫ʹ����ɢ��ײ��⡣����__������̬(Continuous Dynamic)__ �����Ҫ��ײ�Ķ��󡣣������Զ����������кܴ�Ӱ�죬���û�п��ٶ������ײ���⣬�뽫�䱣��Ϊ Discrete ���ã�
        //-Continuous Dynamic       ������Ϊ__����(Continuous)__ ��__������̬(Continuous Dynamic)__ ��ײ����Ϸ����ʹ�û���ɨ�ӵ�������ײ��⡣�����Ծ�̬��ײ�壨û�и��壩ʹ��������ײ��⡣��������������ײ�壬ʹ����ɢ��ײ��⡣���ڿ����ƶ��Ķ���
        //-Continuous Speculative   �Ը������ײ��ʹ���Ʋ���������ײ��⡣��Ҳ�ǿ��������˶������Ψһ CCD ģʽ���÷���ͨ���Ȼ���ɨ�ӵ�������ײ���ĳɱ����͡�
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        // ���������ж�
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        grounded = grounded ? grounded : Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsWall);

        MyInput();
        SpeedControl();
        StateHandler();
        TextStuff();

        // �ж�����ڵ��棬ͬʱû���ù�צ�����е���Ħ��
        if (grounded && !activeGrapple)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        // ��ʼ����
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // ֹͣ����
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    /// <summary>
    /// ���ֶ�̬����
    /// ���ƻ���һ��
    /// </summary>
    public bool keepMomentum;
    private MovementState lastState;
    public void StateHandler()
    {
        if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
            //keepMomentum = true;// ����������⣬�ᵼ�³��ǰ���ٶȱ�������
        }
        else if (freeze)
        {
            state = MovementState.freeze;
            rb.velocity = Vector3.zero;
            desiredMoveSpeed = 0f;
        }
        else if (unlimited)
        {
            state = MovementState.unlimited;
            desiredMoveSpeed = 999f;
        }
        else if (vaulting)
        {
            state = MovementState.vaulting;
            desiredMoveSpeed = vaultSpeed;
        }

        else if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }
        else if (wallrunning)
        {
            state = MovementState.wallruning;
            desiredMoveSpeed = wallRunSpeed;
        }
        else if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
                keepMomentum = true;
            }
            else
            {
                desiredMoveSpeed = crouchSpeed;
            }
        }

        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(SprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;

            if (moveSpeed < airMinSpeed)
            {
                desiredMoveSpeed = airMinSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }


        /*if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;*/
        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

        // deactivate keepMomentum
        if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) keepMomentum = false;
    }

    private float speedChangeFactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            // ���ƽ�ʱ�� == ����ƽ�ʱ��
            else if (boostFactor == dashSpeedChangeFactor)
            {
                time += Time.deltaTime * boostFactor;

                yield return null;
            }
            else
            {
                time += Time.deltaTime * speedIncreaseMultiplier;
            }
            yield return null;
        }

        speedChangeFactor = 1f;
        moveSpeed = desiredMoveSpeed;
        keepMomentum = false;
    }

    public void MovePlayer()
    {
        if (activeGrapple) return;
        if (restricted) return;
        //if (state == MovementState.dashing) return;// ����ע�͵�����Ϊ����ڵ��ϳ�̵Ļ������ڵڶ�֡�Ϳ�ʼ�����޷��ƶ���Ȼ��Ͳ�֪��Ϊʲô�Ŀ�ʼ�����ˣ�Ȼ������Ϊ״̬���walking�������ٶ��ֿ��Լ��ˣ��ͻ��7/18�����12/16��������Ч�����ǳ����һ�㣬Ȼ���ֳ����һ�㣬���ֶ��γ�̵ĸо�

        if (climbingScript != null && climbingScript.exitingWall)
        {
            return;
        }

        // �����ƶ�λ��
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // �ж��Ƿ���б��
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                // ������ӦΪ����б�£�Ȼ��ᵯ��
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else if (grounded)
        {
            // Acceleration��Force��Impulse �� VelocityChange
            //Acceleration��     �������������������ʩ�Ӽ��ٶȡ�
            //Force��            �����ʩ��������������ζ�������ܵ�force����������ʱ��Ϊһ֡��ʱ�䣩����������������ͬ������ʩ����Խ�ص������ϲ����ļ��ٶ�ԽС��
            //Impulse��          �����ʩ��˲ʱ�������൱������˲�����ܵ�force��������Ӱ��һ����ʱ���Ч��������������������ͬ������ʩ����Խ�ص������ϲ����ļ��ٶ�ԽС��
            //VelocityChange��   ���Ӹ�������������ԭ���ٶȵĻ����ϸ�����ʩ��һ���ٶ�������
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if (!wallrunning)
        {
            rb.useGravity = !OnSlope();
        }
    }

    float test;

    /// <summary>
    /// �����ٶ�
    /// �����и����⣬��Ϊ����ʩ��������FixedUpdate��ӵģ�Ȼ������ٶ�ȴ����Update��ӵģ����»��ж϶��������ٶȳ���moveSpeed
    /// ���֡�ٹ��ͣ��ͻᵼ���ٶ�ͻ��moveSpeed
    /// </summary>
    private void SpeedControl()
    {
        if (activeGrapple) return;
        bool v = OnSlope();
        if (v && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else
        {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }

        //if (rb.velocity.magnitude < test)
        //{
        //    Debug.Log("��ӡ��־");
        //}
        //test = rb.velocity.magnitude;
        //SpeedText.text = "Speed:" + rb.velocity.magnitude.ToString("f1");
    }

    public void Jump()
    {
        exitingSlope = true;

        // ��������
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // ʩ��һ��˲ʱ����
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        exitingSlope = false;

        readyToJump = true;
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            // raycashhit.normal = ����
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void TextStuff()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (OnSlope())
            text_speed?.SetText("Speed: " + Round(rb.velocity.magnitude, 1) + " / " + Round(moveSpeed, 1));

        else
            text_speed?.SetText("Speed: " + Round(flatVel.magnitude, 1) + " / " + Round(moveSpeed, 1));

        text_mode?.SetText(state.ToString());
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        cam.DoFov(grappleFov);
    }

    /// <summary>
    /// ���㹳צ��Ծvelocity
    /// </summary>
    /// <param name="starPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="trajectoryHeight"></param>
    /// <returns></returns>
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * MathF.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (MathF.Sqrt(-2 * trajectoryHeight / gravity)
            + MathF.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        // ������Ծ��ʱ���Ҽ���һ����λ����Ȼ�����ȶ�ͣ����ǽ�ϣ����Ǳ���Ҫ��׼�߽ǲſ���
        return velocityXZ + velocityY + (velocityXZ + velocityY).normalized * capsuleCollider.radius * 2;
    }
}
