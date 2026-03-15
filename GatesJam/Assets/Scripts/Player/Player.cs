using UnityEngine;
using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Core.EventManagement;
using GatesJam.LevelManagement;

namespace GatesJam.Player
{
    public class Player : MonoBehaviour
    {
        public int ID;
        public PlayerData Data;
        [SerializeField] private Transform groundCheckTransform;

        public PlayerInputHandler InputHandler;
        public Animator Anim { get; private set; }
        public Rigidbody2D RB { get; private set; }
        public int FacingDirection { get; private set; }

        // State Machine
        public StateMachine<Player, PlayerData> StateMachine;
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerSyncState SyncState { get; private set; }
        public PlayerWaitForDesyncState WaitForDesyncState { get; private set; }
        public PlayerUnusableState UnusableState { get; private set; }
        public PlayerMoveToTargetState MoveToTargetState { get; private set; }

        /// <summary>
        /// Target position for MoveToTargetState. Set before changing to MoveToTargetState.
        /// </summary>
        public Vector2? MoveToTarget { get; set; }

        private bool _isCompletedLevel;

        #region Built-In

        private void Awake()
        {
            FacingDirection = 1;
            Anim = GetComponent<Animator>();
            RB = GetComponent<Rigidbody2D>();

            StateMachine = new();
            IdleState = new PlayerIdleState(this, StateMachine, Data, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, Data, "move");
            JumpState = new PlayerJumpState(this, StateMachine, Data, "inAir");
            InAirState = new PlayerInAirState(this, StateMachine, Data, "inAir");
            SyncState = new PlayerSyncState(this, StateMachine, Data, "sync");
            WaitForDesyncState = new PlayerWaitForDesyncState(this, StateMachine, Data, "idle");
            UnusableState = new PlayerUnusableState(this, StateMachine, Data, "idle");
            MoveToTargetState = new PlayerMoveToTargetState(this, StateMachine, Data, "move");

            StateMachine.Initialize(IdleState);
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
            Anim.SetFloat("yVelocity", RB.linearVelocity.y);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Trigger_End"))
            {
                EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnPlayerCompletedLevel, ID);
                StateMachine.ChangeState(UnusableState);
                _isCompletedLevel = true;
            }
            else if (other.CompareTag("Hazard"))
            {
                LevelManager.Instance.OnLevelFailed();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheckTransform.position, Data.GroundCheckRadius);
        }

        #endregion

        #region Event Subscription

        private void SubscribeToEvents()
        {
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnSyncStarted, HandleOnSyncStarted);
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnSyncEnded, HandleOnSyncEnded);
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnDesyncStarted, HandleOnDesyncStarted);
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnDesyncEnded, HandleOnDesyncEnded);
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnPlayerChanged, HandleOnPlayerChanged);
            EventManagerProvider.Level.AddListener<Level>(LevelEvent.OnLevelLoaded, HandleOnLevelLoaded);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelFailed, HandleOnLevelFailed);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelRestarted, HandleOnLevelRestarted);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnSyncStarted, HandleOnSyncStarted);
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnSyncEnded, HandleOnSyncEnded);
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnDesyncStarted, HandleOnDesyncStarted);
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnDesyncEnded, HandleOnDesyncEnded);
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnPlayerChanged, HandleOnPlayerChanged);
            EventManagerProvider.Level.RemoveListener<Level>(LevelEvent.OnLevelLoaded, HandleOnLevelLoaded);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelFailed, HandleOnLevelFailed);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelRestarted, HandleOnLevelRestarted);
        }

        #endregion

        #region Event Handling

        private void HandleOnSyncStarted()
        {
            StateMachine.ChangeState(UnusableState);
        }

        private void HandleOnSyncEnded()
        {
            if (_isCompletedLevel) return;
            StateMachine.ChangeState(IdleState);
        }

        private void HandleOnDesyncStarted()
        {
            StateMachine.ChangeState(UnusableState);
        }

        private void HandleOnDesyncEnded(int playerID)
        {
            if (playerID == ID) StateMachine.ChangeState(IdleState);
            else StateMachine.ChangeState(UnusableState);
        }

        private void HandleOnPlayerChanged(int playerID)
        {
            if (playerID == ID) StateMachine.ChangeState(IdleState);
            else StateMachine.ChangeState(UnusableState);
        }

        private void HandleOnLevelLoaded(Level level)
        {
            Section section = level.GetSectionByID(ID);
            transform.position = section.CharacterSpawnPoint.position;
            MoveTo(section.CharacterStartPoint.position);
            _isCompletedLevel = false;
        }

        private void HandleOnLevelFailed()
        {
            StateMachine.ChangeState(UnusableState);
        }

        private void HandleOnLevelRestarted()
        {
            StateMachine.ChangeState(UnusableState);
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

        /// <summary>
        /// Moves the character to the given world position. Switches to MoveToTargetState and transitions to Idle when arrived.
        /// </summary>
        public void MoveTo(Vector2 targetPosition)
        {
            MoveToTarget = targetPosition;
            StateMachine.ChangeState(MoveToTargetState);
        }
    }
}