using UnityEngine;
using IboshEngine.Runtime.Utilities.Singleton;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using IboshEngine.Runtime.Core.EventManagement;
using DG.Tweening;
using IboshEngine.Runtime.Utilities.Debugger;
using GatesJam.CameraManagement;

namespace IboshEngine.Runtime.Core.CameraManagement
{
    /// <summary>
    /// Manages the camera system in the game.
    /// </summary>
    public class CameraManager : IboshSingleton<CameraManager>
    {
        [BoxGroup("Virtual Cameras")]
        [SerializeField]
        private CinemachineCamera topCamera;

        [BoxGroup("Virtual Cameras")]
        [SerializeField]
        private CinemachineCamera topZoomedInCamera;

        [BoxGroup("Virtual Cameras")]
        [SerializeField]
        private CinemachineCamera bottomCamera;

        [BoxGroup("Virtual Cameras")]
        [SerializeField]
        private CinemachineCamera bottomZoomedInCamera;

        [BoxGroup("Camera Controllers")]
        [SerializeField]
        private CameraController topCameraController;

        [BoxGroup("Camera Controllers")]
        [SerializeField]
        private CameraController bottomCameraController;

        private CinemachineCamera _currentCamera;
        private float _movementDelay;

        #region Built-In

        protected override void Awake()
        {
            base.Awake();
            _movementDelay = topCameraController.GetComponent<CinemachineBrain>().DefaultBlend.BlendTime;
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
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnCharacterSelectionUpdated, HandleOnCharacterSelectionUpdated);
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnCharacterSelected, HandleOnCharacterSelected);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnCharacterSelectionUpdated, HandleOnCharacterSelectionUpdated);
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnCharacterSelected, HandleOnCharacterSelected);
        }

        #endregion

        #region Event Handling

        private void HandleOnCharacterSelectionUpdated(int id)
        {
            if (id == topCameraController.CameraID) 
            {
                MoveToTopZoomedIn();
                MoveToBottom();
            }
            else if (id == bottomCameraController.CameraID) 
            {
                MoveToBottomZoomedIn();
                MoveToTop();
            }
        }

        private void HandleOnCharacterSelected(int id)
        {
            if (id == topCameraController.CameraID)
            {
                MoveToTop();
            }
            else if (id == bottomCameraController.CameraID)
            {
                MoveToBottom();
            }
        }

        #endregion

        #region Movement

        public async void MoveToTop()
        {
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnTopCameraStarted);
            await SwitchToCamAsync(topCamera);
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnTopCameraCompleted);
        }

        public async void MoveToBottom()
        {
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnBottomCameraStarted);
            await SwitchToCamAsync(bottomCamera);
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnBottomCameraCompleted);
        }

        public async void MoveToTopZoomedIn()
        {
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnTopZoomedInCameraStarted);
            await SwitchToCamAsync(topZoomedInCamera);
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnTopZoomedInCameraCompleted);
        }

        public async void MoveToBottomZoomedIn()
        {
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnBottomZoomedInCameraStarted);
            await SwitchToCamAsync(bottomZoomedInCamera);
            EventManagerProvider.Camera.Broadcast(CameraEvent.OnBottomZoomedInCameraCompleted);
        }

        #endregion

        #region Virtual Camera Management

        private async UniTask SwitchToCamAsync(CinemachineCamera targetCam)
        {
            SetPriority(targetCam);
            await UniTask.Delay((int)(_movementDelay * 1000));
        }

        private void SetPriority(CinemachineCamera targetCam)
        {
            if (targetCam == topCamera || targetCam == topZoomedInCamera)
            {
                ResetTopPriorities();
            }
            else if (targetCam == bottomCamera || targetCam == bottomZoomedInCamera)
            {
                ResetBottomPriorities();
            }

            targetCam.Priority = 10;
            _currentCamera = targetCam;
            IboshDebugger.LogMessage($"Switched to {targetCam.name}", "Camera", IboshDebugger.DebugColor.Gray, IboshDebugger.DebugColor.Magenta);
        }

        private void ResetTopPriorities()
        {
            topCamera.Priority = 0;
            topZoomedInCamera.Priority = 0;
        }

        private void ResetBottomPriorities()
        {
            bottomCamera.Priority = 0;
            bottomZoomedInCamera.Priority = 0;
        }

        #endregion

        #region Inspector Buttons

        [Button(ButtonSizes.Medium)]
        public void ToTop()
        {
            SetPriority(topCamera);
        }

        [Button(ButtonSizes.Medium)]
        public void ToTopZoomedIn()
        {
            SetPriority(topZoomedInCamera);
        }

        [Button(ButtonSizes.Medium)]
        public void ToBottom()
        {
            SetPriority(bottomCamera);
        }

        [Button(ButtonSizes.Medium)]
        public void ToBottomZoomedIn()
        {
            SetPriority(bottomZoomedInCamera);
        }
        #endregion

        #region Camera Shake

        public void ShakeCamera(float duration, float strength, CameraShakeDirection direction)
        {
            if (_currentCamera == null) return;

            Vector3 shakeStrength = Vector3.zero;

            switch (direction)
            {
                case CameraShakeDirection.Horizontal:
                    shakeStrength = new Vector3(strength, 0, 0);
                    break;
                case CameraShakeDirection.Vertical:
                    shakeStrength = new Vector3(0, 0, strength);
                    break;
                case CameraShakeDirection.Both:
                    shakeStrength = new Vector3(strength, 0, strength);
                    break;
                default:
                    break;
            }

            _currentCamera.transform.DOShakePosition(duration, shakeStrength);
        }

        #endregion
    }

    public enum CameraShakeDirection
    {
        Horizontal,
        Vertical,
        Both
    }
}