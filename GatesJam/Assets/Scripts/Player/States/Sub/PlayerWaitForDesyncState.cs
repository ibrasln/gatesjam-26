using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public class PlayerWaitForDesyncState : PlayerGroundedState
    {
        public PlayerWaitForDesyncState(Player player, StateMachine<Player, PlayerData> stateMachine, PlayerData data, string animBoolName) : base(player, stateMachine, data, animBoolName)
        {
        }
    }
}