using IboshEngine.Runtime.Core.EventManagement;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;

namespace GatesJam.CameraManagement
{
    public class CameraController : MonoBehaviour
    {
        public int CameraID;

        [SerializeField] private Volume volume;

        private AnalogGlitchVolume _analogGlitchVolume;
        private DigitalGlitchVolume _digitalGlitchVolume;
    
        private Camera _camera;
        private bool _isGlitching;

        #region Built-In

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            volume.profile.TryGet<AnalogGlitchVolume>(out _analogGlitchVolume);
            volume.profile.TryGet<DigitalGlitchVolume>(out _digitalGlitchVolume);
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void Start()
        {
            DisableGlitch();
        }

        #endregion

        #region Event Subscription

        private void SubscribeToEvents()
        {
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnSyncStarted, HandleOnSyncStarted);
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnDesyncEnded, HandleOnDesyncEnded);
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnPlayerChanged, HandleOnPlayerChanged);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnSyncStarted, HandleOnSyncStarted);
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnDesyncEnded, HandleOnDesyncEnded);
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnPlayerChanged, HandleOnPlayerChanged);
        }

        #endregion

        #region Event Handling

        private void HandleOnSyncStarted()
        {
            DisableGlitch();
        }

        private void HandleOnDesyncEnded(int id)
        {
            if (id == CameraID) DisableGlitch();
            else EnableGlitch();
        }

        private void HandleOnPlayerChanged(int id)
        {
            if (id == CameraID) DisableGlitch();
            else EnableGlitch();
        }

        #endregion

        #region Glitch Management

        public void EnableGlitch()
        {
            _isGlitching = true;
            _analogGlitchVolume.active = true;
            _digitalGlitchVolume.active = true;
        }

        public void DisableGlitch()
        {
            _isGlitching = false;
            _analogGlitchVolume.active = false;
            _digitalGlitchVolume.active = false;
        }

        #endregion
    }
}