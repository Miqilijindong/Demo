using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家输入系统，新版的
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

        // 这里是因为手柄操作时，RawMovementInput.x或者.y不一定等于1,导致动画和人物移动不统一。所以需要加入normalized

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // context.started和context.performed其实两者都是差不多的
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
