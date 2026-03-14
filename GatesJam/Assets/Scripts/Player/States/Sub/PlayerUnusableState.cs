using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public class PlayerUnusableState : PlayerState
    {
        public PlayerUnusableState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }
    }
}