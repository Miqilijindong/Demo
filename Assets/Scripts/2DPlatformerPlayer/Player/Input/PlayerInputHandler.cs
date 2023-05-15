using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �������ϵͳ���°��
/// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    /// <summary>
    /// δ�ӹ���Dash����
    /// </summary>
    public Vector2 RawDashDirectionInput { get; private set; }
    /// <summary>
    /// Dash����̶�45��Ǳ���
    /// </summary>
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    public bool[] AttackInputs { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];

        cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnPrimaryAttactInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttactInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        // ��������Ϊ�ֱ�����ʱ��RawMovementInput.x����.y��һ������1,���¶����������ƶ���ͳһ��������Ҫ����normalized
        /*if (Mathf.Abs(RawMovementInput.x) > 0.25f)
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }*/

        /*if (Mathf.Abs(RawMovementInput.y) > 0.25f)
        {
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }*/

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // context.started��context.performed��ʵ���߶��ǲ���
        /*if (context.started)
        {
            Debug.Log("Jump button pushed down now");
        }

        if (context.performed)
        {
            Debug.Log("Jump is being held down");
        }

        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }*/

        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    /// <summary>
    /// 2023��5��11��17:27:48 --- ����2Dƽ̨��Ϸ�������������Ƴ�̷���ʱ��������һ�����⡣
    /// �����޷�ͨ��input actions��ȡ����ƶ�Position��
    /// ���淢����game��ͼ����Ҫ��PlayFocused�ĳ�PlayMaximized�ſ���
    /// </summary>
    /// <param name="context"></param>
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard")
        {
            //RawDashDirectionInput = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - RawDashDirectionInput.x, cam.pixelHeight - RawDashDirectionInput.y, -100f)) - transform.position;
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        // ����Ϊ����45���ƶ�
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnDashDirectionInputTest(InputValue inputValue)
    {
        RawDashDirectionInput = inputValue.Get<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - RawDashDirectionInput.x, cam.pixelHeight - RawDashDirectionInput.y, -100f)) - transform.position;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }

}


public enum CombatInputs
{
    primary, 
    secondary
}