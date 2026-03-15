using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Systems.UISystem.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace GatesJam.UI
{
    public class InGamePanel : IboshPanel
    {
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button restartButton;

        #region Event Subscription

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            restartButton.onClick.AddListener(OnRestartButtonClicked);

            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelStarted, HandleOnLevelStarted);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelSucceeded, HandleOnLevelSucceeded);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelFailed, HandleOnLevelFailed);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelRestarted, HandleOnLevelRestarted);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);

            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelStarted, HandleOnLevelStarted);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelSucceeded, HandleOnLevelSucceeded);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelFailed, HandleOnLevelFailed);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelRestarted, HandleOnLevelRestarted);
        }

        #endregion

        #region Event Handling

        private void HandleOnLevelStarted()
        {
            Show();
        }

        private void HandleOnLevelSucceeded()
        {
            Hide();
        }

        private void HandleOnLevelFailed()
        {
            Hide();
        }

        private void HandleOnLevelRestarted()
        {
            Hide();
        }

        #endregion

        #region Button Actions

        public void OnSettingsButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnSettingsButtonClicked);
        }

        public void OnRestartButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnRestartButtonClicked);
        }

        #endregion
    }
}