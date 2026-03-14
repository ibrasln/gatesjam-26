using IboshEngine.Runtime.Systems.StateMachine;

namespace GatesJam.Player
{
    public class PlayerSyncState : PlayerAbilityState
    {
        public PlayerSyncState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }
}