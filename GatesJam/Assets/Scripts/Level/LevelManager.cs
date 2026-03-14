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

        #region Built-In

        protected override void Awake()
        {
            base.Awake();
            _levels = levelsParent.GetComponentsInChildren<Level>(true);
        }

        private async void Start()
        {
            await UniTask.Delay(2000);
            LoadLevel();
        }

        #endregion

        #region Level Management

        public void LoadLevel()
        {
            CurrentLevel?.gameObject.SetActive(false);

            CurrentLevel = _levels[_currentLevelIndex];
            CurrentLevel.gameObject.SetActive(true);
            CurrentLevel.Initialize();

            EventManagerProvider.Level.Broadcast(LevelEvent.OnLevelLoaded, CurrentLevel);
        }

        public async void OnLevelSucceeded()
        {
            IncrementLevelIndex();

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
    }
}