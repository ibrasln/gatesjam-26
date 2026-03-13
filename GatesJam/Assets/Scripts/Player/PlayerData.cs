using UnityEngine;

namespace GatesJam.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        public float MoveSpeed;
        public float JumpForce;
    }
}