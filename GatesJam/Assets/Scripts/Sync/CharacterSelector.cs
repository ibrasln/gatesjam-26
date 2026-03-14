using GatesJam.Player;
using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Utilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GatesJam.Sync
{
    public class CharacterSelector : IboshSingleton<CharacterSelector>
    {
        [ReadOnly] public Player.Player SelectedPlayer;
        [SerializeField] private Player.Player[] players;

        #region Built-In

        protected override void Awake()
        {
            base.Awake();
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
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnDesyncStarted, HandleOnDesyncStarted);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnDesyncStarted, HandleOnDesyncStarted);
        }

        #endregion

        #region Event Handling

        private void HandleOnDesyncStarted()
        {
            Debug.Log("Desync Started");
            PlayerInputHandler.Instance.EnterCharacterSelectionActionMap();
        }

        #endregion

        #region Character Selection

        public void SelectTop()
        {
            SelectedPlayer = players[0];
            Debug.Log("Selected Top Player");
        }
        
        public void SelectBottom()
        {
            SelectedPlayer = players[1];
            Debug.Log("Selected Bottom Player");
        }

        public void ConfirmSelection()
        {
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnCharacterSelected, SelectedPlayer);
        }

        #endregion
    }
}