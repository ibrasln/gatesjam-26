using Sirenix.OdinInspector;
using UnityEngine;

namespace GatesJam.LevelManagement
{
    public class Section : MonoBehaviour
    {
        [ReadOnly] public int ID;

        public Transform CharacterSpawnPoint;
        public Transform CharacterStartPoint;
        public Transform CharacterEndPoint;
        public Transform CharacterDespawnPoint;
        public Collider2D CameraConfiner;


        #region Initialization

        public void Initialize(int id)
        {
            ID = id;
            Debug.Log($"Section {id} initialized");
        }

        #endregion
    }
}