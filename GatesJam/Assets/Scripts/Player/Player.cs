using UnityEngine;
using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerData Data;
        [SerializeField] private Transform groundCheckTransform;

        public PlayerInputHandler InputHandler;
        public Animator Anim {get; private set; }
        public Rigidbody2D RB {get; private set; }
        public int FacingDirection { get; private set; }

        // State Machine
        public StateMachine<Player, PlayerData> StateMachine;
        public PlayerIdleState IdleState {get; private set; }
        public PlayerMoveState MoveState {get; private set; }
        public PlayerJumpState JumpState {get; private set; }
        public PlayerInAirState InAirState {get; private set; }

        #region Built-In
        private void Awake()
        {
            Anim = GetComponent<Animator>();
            RB = GetComponent<Rigidbody2D>();
            
            StateMachine = new();
            IdleState = new PlayerIdleState(this, StateMachine, Data, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, Data, "move");
            JumpState = new PlayerJumpState(this, StateMachine, Data, "inAir");
            InAirState = new PlayerInAirState(this, StateMachine, Data, "inAir");
        }

        private void Start() 
        {
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            Debug.Log(StateMachine.CurrentState.GetType().Name);
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheckTransform.position, Data.GroundCheckRadius);
        }

        #endregion
   
        #region Checks

        public bool CheckOnGround()
        {
            return Physics2D.OverlapCircle(groundCheckTransform.position, Data.GroundCheckRadius, Data.WhatIsGround);
        }

        #endregion

        public void Flip(int xInput)
        {
            if (xInput == 0 || xInput == FacingDirection) return;
            
            FacingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0f);
        }
    }
}