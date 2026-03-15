using UnityEngine;
using IboshEngine.Runtime.Systems.StateMachine;
using IboshEngine.Runtime.Utilities.Extensions;

namespace GatesJam.Player
{
    public class PlayerMoveToTargetState : PlayerState
    {
        public PlayerMoveToTargetState(Player obj, StateMachine<Player, PlayerData> stateMachine, PlayerData objData, string animBoolName)
            : base(obj, stateMachine, objData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (!obj.MoveToTarget.HasValue)
            {
                stateMachine.ChangeState(obj.IdleState);
                return;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!obj.MoveToTarget.HasValue)
            {
                stateMachine.ChangeState(obj.IdleState);
                return;
            }

            Vector2 currentPosition = obj.RB.position;
            Vector2 targetPosition = obj.MoveToTarget.Value;
            float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

            float arrivalThreshold = objData.MoveToTargetArrivalThreshold > 0f ? objData.MoveToTargetArrivalThreshold : 0.05f;
            if (distanceToTarget <= arrivalThreshold)
            {
                obj.RB.SetVelocityZero();
                obj.MoveToTarget = null;
                stateMachine.ChangeState(obj.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!obj.MoveToTarget.HasValue)
                return;

            Vector2 currentPosition = obj.RB.position;
            Vector2 targetPosition = obj.MoveToTarget.Value;
            Vector2 direction = (targetPosition - currentPosition).normalized;

            float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
            float moveSpeed = objData.MoveToTargetSpeed > 0f ? objData.MoveToTargetSpeed : objData.MoveSpeed;
            float speed = Mathf.Min(moveSpeed, distanceToTarget / Time.fixedDeltaTime);
            Vector2 velocity = direction * speed;

            obj.RB.SetVelocity(velocity);

            int facingDirection = direction.x > 0f ? 1 : (direction.x < 0f ? -1 : 0);
            if (facingDirection != 0)
                obj.Flip(facingDirection);
        }
    }
}
