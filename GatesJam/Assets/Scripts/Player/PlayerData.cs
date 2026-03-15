using UnityEngine;

namespace GatesJam.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        public float MoveSpeed;
        public float JumpForce;
        public float InAirMoveSpeed;

        public float MoveToTargetSpeed = 5f;
        public float MoveToTargetArrivalThreshold = 0.05f;

        public LayerMask WhatIsGround;
        public float GroundCheckRadius;
    }
}