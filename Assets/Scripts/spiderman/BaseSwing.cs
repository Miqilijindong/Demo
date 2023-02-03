using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSwing : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    public Transform guntip, cam, player;
    public LayerMask whatIsGrappleable;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("Input")]
    public KeyCode swingCode = KeyCode.Mouse0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(swingCode))
        {
            StartSwing();
        }
        if (Input.GetKeyUp(swingCode))
        {
            StopSwing();
        }
    }

    private void StartSwing()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 2.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
        }
    }

    private void StopSwing()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}