using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Utilities.Extensions;

namespace GatesJam.Player
{
    public class PlayerWaitForDesyncState : PlayerGroundedState
    {
        public PlayerWaitForDesyncState(Player player, StateMachine<Player, PlayerData> stateMachine, PlayerData data, string animBoolName) : base(player, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            obj.RB.SetVelocityZero();
        }
    }
}