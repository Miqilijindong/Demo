using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �������ϵͳ���°��
/// 
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        // ��������Ϊ�ֱ�����ʱ��RawMovementInput.x����.y��һ������1,���¶����������ƶ���ͳһ��������Ҫ����normalized

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
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


    }
}
