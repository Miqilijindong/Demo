using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPlayer
{
    /// <summary>
    /// ÕÊº“¿‡
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region State Variables
        public PlayerStateMachine stateMachine { get; private set; }

        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerInAirState inAirState { get; private set; }
        public PlayerLandState landState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerWallGrabState wallGrabState { get; private set; }
        public PlayerWallClimbState wallClimbState { get; private set; }

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
        #endregion

        #region Other Functions
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
