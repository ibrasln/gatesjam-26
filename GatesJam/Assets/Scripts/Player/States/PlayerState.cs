using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public abstract class PlayerState : State<Player, PlayerData>
    {
        protected PlayerState(Player player, StateMachine<Player, PlayerData> stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }
    }
}