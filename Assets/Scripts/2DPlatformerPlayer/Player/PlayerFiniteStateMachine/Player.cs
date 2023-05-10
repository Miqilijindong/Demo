using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPlayer
{
    /// <summary>
    /// Íæ¼ÒÀà
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region State Variables
        public PlayerStateMachine stateMachine { get; private set; }

        /// <summary>
        /// ¾²Ö¹×´Ì¬
        /// </summary>
        public PlayerIdleState idleState { get; private set; }
        /// <summary>
        /// ÒÆ¶¯×´Ì¬
        /// </summary>
        public PlayerMoveState moveState { get; private set; }
        /// <summary>
        /// ÌøÔ¾×´Ì¬
        /// </summary>
        public PlayerJumpState jumpState { get; private set; }
        /// <summary>
        /// ¿ÕÖÐ×´Ì¬
        /// </summary>
        public PlayerInAirState inAirState { get; private set; }
        /// <summary>
        /// µØÉÏ×´Ì¬
        /// </summary>
        public PlayerLandState landState { get; private set; }
        /// <summary>
        /// Ç½ÉÏÏÂ»¬×´Ì¬
        /// </summary>
        public PlayerWallSlideState wallSlideState { get; private set; }
        /// <summary>
        /// ×¥Ç½×´Ì¬
        /// </summary>
        public PlayerWallGrabState wallGrabState { get; private set; }
        /// <summary>
        /// ÅÀÇ½×´Ì¬
        /// </summary>
        public PlayerWallClimbState wallClimbState { get; private set; }
        /// <summary>
        /// ÅÀÇ½ÌøÔ¾×´Ì¬
        /// </summary>
        public PlayerWallJumpState wallJumpState { get; private set; }
        /// <summary>
        /// ×¥Ç½ÌøÔ¾×´Ì¬
        /// </summary>
        public PlayerLedgeClimbState ledgeClimbState { get; private set; }

        [SerializeField]
        private PlayerData playerData;
        #endregion

        #region Components
        public Animator anim { get; private set; }
        public PlayerInputHandler inputHandler { get; private set; }
        public Rigidbody2D rb;
        #endregion

        #region Check Transforms
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private Transform wallCheck;
        [SerializeField]
        private Transform ledgeCheck;
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
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
            rb = GetComponent<Rigidbody2D>();
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
        public Vector2 DetermineCornerPosition()
        {
            RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
            float xDist = xHit.distance;
            workSpace.Set(xDist * faceingDirection, 0f);
            RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
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
