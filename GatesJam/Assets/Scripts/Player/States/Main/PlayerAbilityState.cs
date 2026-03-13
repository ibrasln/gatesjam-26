using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool isAbilityDone;

        public PlayerAbilityState(Player player, StateMachine<Player, PlayerData> stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            isAbilityDone = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isAbilityDone)
            {
                if (onGround) stateMachine.ChangeState(obj.IdleState);
                else stateMachine.ChangeState(obj.InAirState);
            }
        }
    }
}