using GatesJam.Player;
using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Utilities.Singleton;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
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

        private async void HandleOnDesyncStarted()
        {
            await UniTask.Delay(1250);
            PlayerInputHandler.Instance.EnterCharacterSelectionActionMap();
            SelectTop();
        }

        #endregion

        #region Character Selection

        public void SelectTop()
        {
            SelectedPlayer = players[0];
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnCharacterSelectionUpdated, SelectedPlayer.ID);
        }
        
        public void SelectBottom()
        {
            SelectedPlayer = players[1];
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnCharacterSelectionUpdated, SelectedPlayer.ID);
        }

        public void ConfirmSelection()
        {
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnCharacterSelected, SelectedPlayer.ID);
        }

        #endregion

        public void ChangePlayer()
        {
            SelectedPlayer = SelectedPlayer == players[0] ? players[1] : players[0];
            Debug.Log("Changed Player to: " + SelectedPlayer.name);
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnPlayerChanged, SelectedPlayer.ID);
        }
    }
}