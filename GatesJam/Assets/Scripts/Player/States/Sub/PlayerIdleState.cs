using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Utilities.Extensions;

namespace GatesJam.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            obj.RB.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (xInput != 0) stateMachine.ChangeState(obj.MoveState);
        }
    }
}