using UnityEngine;

namespace GatesJam.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        public float MoveSpeed;
        public float JumpForce;
        public float InAirMoveSpeed;

        public LayerMask WhatIsGround;
        public float GroundCheckRadius;
    }
}