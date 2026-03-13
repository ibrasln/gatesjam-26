using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Utilities.Extensions;

namespace GatesJam.Player
{
    public class PlayerInAirState : PlayerState
    {
        protected int xInput;
        
        public PlayerInAirState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            xInput = obj.InputHandler.XInput;

            if (onGround && obj.RB.linearVelocity.y < 0.1f)
            {
                if (xInput == 0) stateMachine.ChangeState(obj.IdleState);
                else stateMachine.ChangeState(obj.MoveState);
            }
            else
            {
                obj.RB.SetVelocityX(xInput * objData.InAirMoveSpeed);
                obj.Flip(xInput);
            }
        }
    }
}