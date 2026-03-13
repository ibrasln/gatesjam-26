using IboshEngine.Runtime.Systems.StateMachine;
using UnityEngine;

namespace GatesJam.Player
{
    public abstract class PlayerState : State<Player, PlayerData>
    {
        protected bool onGround;
        
        private readonly int _animationHash;
        
        public PlayerState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName = "") : base(obj, stateMachine, objData, animBoolName)
        {
            _animationHash = Animator.StringToHash(animBoolName);
        }

        public override void Enter()
        {
            base.Enter();
            obj.Anim.SetBool(_animationHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            obj.Anim.SetBool(_animationHash, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
            onGround = obj.CheckOnGround();
        }
    }
}