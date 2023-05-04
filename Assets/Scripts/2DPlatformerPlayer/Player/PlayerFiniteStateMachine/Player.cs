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

        [SerializeField]
        private PlayerData playerData;
        #endregion

        #region Components
        public Animator anim { get; private set; }
        public PlayerInputHandler inputHandler { get; private set; }
        public Rigidbody2D rb;
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
            stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            currentVelocity = rb.velocity;
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
        #endregion

        #region Check Functions
        public void CheckIfShouldFlip(int inputX)
        {
            if (inputX != 0 && inputX != faceingDirection)
            {
                Flip();
            }
        }
        #endregion

        #region Other Functions
        private void Flip()
        {
            faceingDirection *= -1;
            transform.Rotate(0, 180f, 0);
        }
        #endregion
    }
}
