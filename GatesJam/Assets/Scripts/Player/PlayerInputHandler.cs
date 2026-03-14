using GatesJam.Sync;
using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Utilities.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GatesJam.Player
{
    public class PlayerInputHandler : IboshSingleton<PlayerInputHandler>
    {
        public int XInput { get; private set; }
        public int YInput { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool SyncInput { get; private set; }

        private PlayerInput _playerInput;

        #region Built-In

        protected override void Awake()
        {
            base.Awake();
            _playerInput = GetComponent<PlayerInput>();
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
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnDesyncEnded, HandleOnDesyncEnded);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnDesyncEnded, HandleOnDesyncEnded);
        }
        
        #endregion

        #region Event Handling

        private void HandleOnDesyncEnded()
        {
            EnterPlayerActionMap();
        }

        #endregion

        #region Player Input Actions

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            XInput = Mathf.RoundToInt(moveInput.x);
            YInput = Mathf.RoundToInt(moveInput.y);
            MovementInput = moveInput;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpInput = true;
            }
            else if (context.canceled)
            {
                JumpInput = false;
            }
        }

        public void OnSync(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SyncInput = true;
                SyncManager.Instance.Execute();
            }
            else if (context.canceled)
            {
                SyncInput = false;
            }
        }

        public void OnChangePlayer(InputAction.CallbackContext context)
        {
            if (context.performed) CharacterSelector.Instance.ChangePlayer();
        }

        #endregion

        #region Character Selection Input Actions

        public void OnSelectTop(InputAction.CallbackContext context)
        {
            if (context.performed) CharacterSelector.Instance.SelectTop();
        }

        public void OnSelectBottom(InputAction.CallbackContext context)
        {
            if (context.performed) CharacterSelector.Instance.SelectBottom();
        }

        public void OnConfirmSelection(InputAction.CallbackContext context)
        {
            if (context.performed) CharacterSelector.Instance.ConfirmSelection();
        }

        #endregion

        #region Action Map Management

        public void EnableActionMap(string actionMapName, bool hasCursor)
        {
            _playerInput.SwitchCurrentActionMap(actionMapName);

            Cursor.lockState = hasCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = hasCursor;
        }

        public void EnterPlayerActionMap()
        {
            EnableActionMap("Player", false);
        }

        public void EnterUIActionMap()
        {
            EnableActionMap("UI", true);
        }

        public void EnterCharacterSelectionActionMap()
        {
            EnableActionMap("CharacterSelection", false);
        }

        #endregion
    }
}