using Cysharp.Threading.Tasks;
using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Utilities.Singleton;
using Sirenix.OdinInspector;

namespace GatesJam.Sync
{
    public class SyncManager : IboshSingleton<SyncManager>
    {
        [ReadOnly] public bool IsSynced;
        [ReadOnly] public bool IsExecuting;

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
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnCharacterSelected, HandleOnCharacterSelected);
            EventManagerProvider.Gameplay.AddListener(GameplayEvent.OnPlayerCompletedLevel, HandleOnPlayerCompletedLevel);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnCharacterSelected, HandleOnCharacterSelected);
            EventManagerProvider.Gameplay.RemoveListener(GameplayEvent.OnPlayerCompletedLevel, HandleOnPlayerCompletedLevel);
        }

        #endregion

        #region Event Handling

        private void HandleOnCharacterSelected(int id)
        {
            EndDesync(id);
        }

        private async void HandleOnPlayerCompletedLevel()
        {
            await SyncAsync();
        }

        #endregion

        #region Sync & Desync

        public async void Execute()
        {
            if (IsExecuting) return;
            IsExecuting = true;

            if (IsSynced)
            {
                StartDesync();
            }
            else
            {
                await SyncAsync();
            }

            IsExecuting = false;
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