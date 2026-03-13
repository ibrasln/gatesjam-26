using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public class PlayerGroundedState : PlayerState
    {
        protected int xInput;
        protected int yInput;
        protected bool jumpInput;
        
        public PlayerGroundedState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            xInput = obj.InputHandler.XInput;
            yInput = obj.InputHandler.YInput;
            jumpInput = obj.InputHandler.JumpInput;
            
            if (jumpInput) stateMachine.ChangeState(obj.JumpState);
        }
    }
}   