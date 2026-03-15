using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Utilities.Extensions;

namespace GatesJam.Player
{
    public class PlayerUnusableState : PlayerState
    {
        public PlayerUnusableState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName) : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            obj.RB.SetVelocityZero();
        }
    }
}