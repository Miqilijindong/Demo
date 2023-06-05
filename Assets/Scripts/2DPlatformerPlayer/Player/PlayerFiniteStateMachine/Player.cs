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
        /// ×¥Ç½½Ç×´Ì¬
        /// </summary>
        public PlayerLedgeClimbState ledgeClimbState { get; private set; }
        /// <summary>
        /// ³å´Ì×´Ì¬
        /// </summary>
        public PlayerDashState dashState { get; private set; }
        /// <summary>
        /// ¶×ÏÂ¾²Ö¹×´Ì¬
        /// </summary>
        public PlayerCrouchIdleState crouchIdleState { get; private set; }
        /// <summary>
        /// ¶×ÏÂÒÆ¶¯×´Ì¬
        /// </summary>
        public PlayerCrouchMoveState crouchMoveState { get; private set; }
        /// <summary>
        /// Ê×Òª¹¥»÷
        /// </summary>
        public PlayerAttackState primaryAttackState { get; private set; }
        /// <summary>
        /// µÚ¶þÖÖ¹¥»÷---Ä¿Ç°»¹Î´ÊµÏÖ
        /// </summary>
        public PlayerAttackState secondaryAttackState { get; private set; }

        [SerializeField]
        private PlayerData playerData;
        #endregion

        #region Components

        public Core core { get; private set; }
        public Animator anim { get; private set; }
        public PlayerInputHandler inputHandler { get; private set; }
        public Rigidbody2D rb;
        public Transform dashDirectionIndicator { get; private set; }
        public BoxCollider2D movementCollider { get; private set; }
        public PlayerInventory inventory { get; private set; }

        #endregion

        #region Other Variables
        //public Vector2 currentVelocity { get; private set; }
        //public int faceingDirection { get; private set; }

        private Vector2 workSpace;
        #endregion

        #region Unity Callback Functions
        private void Awake()
        {
            core = GetComponentInChildren<Core>();

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
            primaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
            secondaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
            rb = GetComponent<Rigidbody2D>();
            dashDirectionIndicator = transform.Find("DashDirectionIndicator");
            movementCollider = GetComponent<BoxCollider2D>();
            inventory = GetComponent<PlayerInventory>();

            //faceingDirection = 1;

            primaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
            //secondaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);

            stateMachine.Initialize(idleState);
        }

        private void Update()
        {
            //currentVelocity = rb.velocity;
            core.LogicUpdate();
            stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        #region Other Functions
        
        /// <summary>
        /// ÐÞ¸ÄÅö×²Ìå»ý¸ß¶È
        /// </summary>
        /// <param name="height"></param>
        public void SetColliderHeight(float height)
        {
            Vector2 center = movementCollider.offset;
            workSpace.Set(movementCollider.size.x, height);

            center.y += (height - movementCollider.size.y) / 2;
            movementCollider.size = workSpace;
            movementCollider.offset = center;
        }

        /*public Vector2 DetermineCornerPositionTest()
        {
            RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * core.movement.faceingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
            float xDist = xHit.distance;
            workSpace.Set((xDist + 0.015f) * core.movement.faceingDirection, 0f);
            RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
            float yDist = yHit.distance;

            workSpace.Set(wallCheck.position.x + (xDist * core.movement.faceingDirection), ledgeCheck.position.y - yDist);
            return workSpace;
        }*/

        private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
        #endregion
    }
}
