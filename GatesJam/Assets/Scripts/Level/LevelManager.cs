using Cysharp.Threading.Tasks;
using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Utilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GatesJam.LevelManagement
{
    public class LevelManager : IboshSingleton<LevelManager>
    {
        [ReadOnly] public Level CurrentLevel;
        [SerializeField] private Transform levelsParent;
        private Level[] _levels;

        private int _currentLevelIndex;
        private int _succeededCharacterAmount;

        public bool IsAnyCharacterCompletedLevel => _succeededCharacterAmount >= 1;

        #region Built-In

        protected override void Awake()
        {
            base.Awake();
            _levels = levelsParent.GetComponentsInChildren<Level>(true);
        }

        private void Start()
        {
            LoadLevel();
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
            EventManagerProvider.Gameplay.AddListener<int>(GameplayEvent.OnPlayerCompletedLevel, HandleOnPlayerCompletedLevel);
        }

        private void UnsubscribeFromEvents()
        {
            EventManagerProvider.Gameplay.RemoveListener<int>(GameplayEvent.OnPlayerCompletedLevel, HandleOnPlayerCompletedLevel);
        }

        #endregion

        #region Event Handling

        private void HandleOnPlayerCompletedLevel(int playerID)
        {
            IncrementSucceededCharacterAmount();

            if (_succeededCharacterAmount >= 2)
            {
                OnLevelSucceeded();
            }
        }

        #endregion

        #region Level Management

        private void ResetLevel()
        {
            _succeededCharacterAmount = 0;
        }

        public void LoadLevel()
        {
            ResetLevel();

            CurrentLevel?.gameObject.SetActive(false);

            CurrentLevel = _levels[_currentLevelIndex];
            CurrentLevel.gameObject.SetActive(true);
            CurrentLevel.Initialize();

            EventManagerProvider.Level.Broadcast(LevelEvent.OnLevelLoaded, CurrentLevel);
        }

        public async void OnLevelSucceeded()
        {
            Debug.Log("Level Succeeded");
            IncrementLevelIndex();

            await UniTask.Delay(500);
            EventManagerProvider.Level.Broadcast(LevelEvent.OnLevelSucceeded);
            await UniTask.Delay(500);
            LoadLevel();
            await UniTask.Delay(500);
            EventManagerProvider.Level.Broadcast(LevelEvent.OnLevelStarted);
        }

        #endregion

        #region Level Index

        public void IncrementLevelIndex()
        {
            _currentLevelIndex++;
        }

        public void DecrementLevelIndex()
        {
            _currentLevelIndex--;
        }

        public void SetLevelIndex(int index)
        {
            _currentLevelIndex = index;
        }

        #endregion

        #region Succeeded Character Amount

        public void IncrementSucceededCharacterAmount()
        {
            _succeededCharacterAmount++;
        }

        public void DecrementSucceededCharacterAmount()
        {
            _succeededCharacterAmount--;
        }

        #endregion
    }
}