using Cysharp.Threading.Tasks;
using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Utilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GatesJam.Sync
{
    public class SyncManager : IboshSingleton<SyncManager>
    {
        [ReadOnly] public bool IsSynced;
        private bool _isExecuting;

        #region Built-In

        protected override void Awake()
        {
            base.Awake();
            IsSynced = true;
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

        #region Event Handling

        private void SubscribeToEvents()
        {
            EventManagerProvider.Gameplay.AddListener<Player.Player>(GameplayEvent.OnCharacterSelected, HandleOnCharacterSelected);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener<Player.Player>(GameplayEvent.OnCharacterSelected, HandleOnCharacterSelected);
        }

        #endregion

        #region Event Handling

        private void HandleOnCharacterSelected(Player.Player player)
        {
            Debug.Log("Character Selected: " + player.name);
            EndDesync(player.ID);
        }

        #endregion

        #region Sync & Desync

        public async void Execute()
        {
            if (_isExecuting) return;
            _isExecuting = true;

            if (IsSynced)
            {
                StartDesync();
            }
            else
            {
                await SyncAsync();
            }

            _isExecuting = false;
        }

        public async UniTask SyncAsync()
        {
            IsSynced = true;
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnSyncStarted);
            await UniTask.Delay(1000);
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnSyncEnded);
        }

        public void StartDesync()
        {
            IsSynced = false;
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnDesyncStarted);
        }

        private void EndDesync(int playerID)
        {
            EventManagerProvider.Gameplay.Broadcast(GameplayEvent.OnDesyncEnded, playerID);
        }

        #endregion
    }
}