using GatesJam.LevelManagement;
using IboshEngine.Runtime.Core.EventManagement;
using Unity.Cinemachine;
using UnityEngine;

namespace GatesJam.CameraManagement
{
    public class ConfinerSetter : MonoBehaviour
    {
        [SerializeField] private int id;
        private CinemachineConfiner2D _confiner;
        private Collider2D _confinerCollider;

        #region Built-In

        private void Awake()
        {
            _confiner = GetComponent<CinemachineConfiner2D>();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        #region Event Subscription

        private void SubscribeToEvents()
        {
            EventManagerProvider.Level.AddListener<Level>(LevelEvent.OnLevelLoaded, HandleOnLevelLoaded);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Level.RemoveListener<Level>(LevelEvent.OnLevelLoaded, HandleOnLevelLoaded);
        }

        #endregion

        #region Event Handling

        private void HandleOnLevelLoaded(Level level)
        {
            _confinerCollider = level.GetSectionByID(id).CameraConfiner;
            _confiner.BoundingShape2D = _confinerCollider;
            _confiner.InvalidateBoundingShapeCache();
        }

        #endregion
    }
}