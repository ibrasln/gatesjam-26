using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Utilities.Extensions;

namespace GatesJam.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (xInput == 0) stateMachine.ChangeState(obj.IdleState);
            else
            {
                obj.RB.SetVelocityX(xInput * objData.MoveSpeed);
                obj.Flip(xInput);
            }
        }
    }
}