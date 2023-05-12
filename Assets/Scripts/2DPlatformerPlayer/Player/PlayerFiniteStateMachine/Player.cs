using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPlayer
{
    /// <summary>
    /// 玩家类
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region State Variables
        public PlayerStateMachine stateMachine { get; private set; }

        /// <summary>
        /// 静止状态
        /// </summary>
        public PlayerIdleState idleState { get; private set; }
        /// <summary>
        /// 移动状态
        /// </summary>
        public PlayerMoveState moveState { get; private set; }
        /// <summary>
        /// 跳跃状态
        /// </summary>
        public PlayerJumpState jumpState { get; private set; }
        /// <summary>
        /// 空中状态
        /// </summary>
        public PlayerInAirState inAirState { get; private set; }
        /// <summary>
        /// 地上状态
        /// </summary>
        public PlayerLandState landState { get; private set; }
        /// <summary>
        /// 墙上下滑状态
        /// </summary>
        public PlayerWallSlideState wallSlideState { get; private set; }
        /// <summary>
        /// 抓墙状态
        /// </summary>
        public PlayerWallGrabState wallGrabState { get; private set; }
        /// <summary>
        /// 爬墙状态
        /// </summary>
        public PlayerWallClimbState wallClimbState { get; private set; }
        /// <summary>
        /// 爬墙跳跃状态
        /// </summary>
        public PlayerWallJumpState wallJumpState { get; private set; }
        /// <summary>
        /// 抓墙角状态
        /// </summary>
        public PlayerLedgeClimbState ledgeClimbState { get; private set; }
        /// <summary>
        /// 冲刺状态
        /// </summary>
        public PlayerDashState dashState { get; private set; }
        /// <summary>
        /// 蹲下静止状态
        /// </summary>
        public PlayerCrouchIdleState crouchIdleState { get; private set; }
        /// <summary>
        /// 蹲下移动状态
        /// </summary>
        public PlayerCrouchMoveState crouchMoveState { get; private set; }

        [SerializeField]
        private PlayerData playerData;
        #endregion

        #region Components
        public Animator anim { get; private set; }
        public PlayerInputHandler inputHandler { get; private set; }
        public Rigidbody2D rb;
        public Transform dashDirectionIndicator { get; private set; }
        public BoxCollider2D movementCollider { get; private set; }

        #endregion

        #region Check Transforms
         
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private Transform wallCheck;
        [SerializeField]
        private Transform ledgeCheck;
        [SerializeField]
        private Transform ceilingCheck;

        #endregion

        #region Other Variables
        public Vector2 currentVelocity { get; private set; }
        public int faceingDirection { get; private set; }

        private Vector2 workSpace;
        #endregion

        #region Unity Callback Functions
        private void Awake()
        {
            stateMachine = new PlayerStateMachine();

            idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
            moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
            jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
            inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
            landState = new PlayerLandState(this, stateMachine, playerData, "land");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
            wallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
            wallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
            ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
            dashState = new PlayerDashState(this, stateMachine, playerData, "inAir");
            crouchIdleState = new PlayerCrouchIdleState(this, stateMachine, playerData, "crouchIdle");
            crouchMoveState = new PlayerCrouchMoveState(this, stateMachine, playerData, "crouchMove");
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
            rb = GetComponent<Rigidbody2D>();
            dashDirectionIndicator = transform.Find("DashDirectionIndicator");
            movementCollider = GetComponent<BoxCollider2D>();

            faceingDirection = 1;

            stateMachine.Initialize(idleState);
        }

        private void Update()
        {
            currentVelocity = rb.velocity;
            stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        #region Set Functions
        public void SetVelocityZero()
        {
            rb.velocity = Vector2.zero;
            currentVelocity = Vector2.zero;
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
            rb.velocity = workSpace;
            currentVelocity = workSpace;
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workSpace = direction * velocity;
            rb.velocity = workSpace;
            currentVelocity = workSpace;
        }

        public void SetVelocityX(float velocity)
        {
            workSpace.Set(velocity, currentVelocity.y);
            rb.velocity = workSpace;
            currentVelocity = workSpace;
        }

        public void SetVelocityY(float velocity)
        {
            workSpace.Set(currentVelocity.x, velocity);
            rb.velocity = workSpace;
            currentVelocity = workSpace;
        }
        #endregion

        #region Check Functions

        public bool CheckForCeiling()
        {
            return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        }

        public void CheckIfShouldFlip(int inputX)
        {
            if (inputX != 0 && inputX != faceingDirection)
            {
                Flip();
            }
        }

        public bool CheckIfGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        }

        public bool CheckIfTouchingWall()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        }

        public bool CheckIfTouchingLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.right * faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        }

        public bool CheckIfTouchingWallBack()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * -faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        }
        #endregion

        #region Other Functions
        public void SetColliderHeight(float height)
        {
            Vector2 center = movementCollider.offset;
            workSpace.Set(movementCollider.size.x, height);

            center.y += (height - movementCollider.size.y) / 2;
            movementCollider.size = workSpace;
            movementCollider.offset = center;
        }

        public Vector2 DetermineCornerPosition()
        {
            RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
            float xDist = xHit.distance;

            workSpace.Set((xDist + 0.015f) * faceingDirection, 0f);
            //workSpace.Set(xDist * faceingDirection, 0f);// 这里不知道为什么有问题，会导致墙角抓墙位置有问题
            RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
            //RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);// 这里不知道为什么有问题，会导致墙角抓墙位置有问题
            float yDist = yHit.distance;

            workSpace.Set(wallCheck.position.x + (xDist * faceingDirection), ledgeCheck.position.y - yDist);

            return workSpace;
        }

        public Vector2 DetermineCornerPositionTest()
        {
            RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
            float xDist = xHit.distance;
            workSpace.Set((xDist + 0.015f) * faceingDirection, 0f);
            RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
            float yDist = yHit.distance;

            workSpace.Set(wallCheck.position.x + (xDist * faceingDirection), ledgeCheck.position.y - yDist);
            return workSpace;
        }

        private void Flip()
        {
            faceingDirection *= -1;
            transform.Rotate(0, 180f, 0);
        }

        private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
        #endregion
    }
}
