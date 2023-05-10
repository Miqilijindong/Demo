using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public float variableJumpHeightMultiplier = 0.5f;
    /// <summary>
    /// ÌøÔ¾´ÎÊý
    /// </summary>
    public int amountOfJumps = 1;

    [Header("Wall JUmp State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float wallJumpCoyoteTime = 0.1f;

    [Header("Wall Slider State")]
    public float wallSliderVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

}
