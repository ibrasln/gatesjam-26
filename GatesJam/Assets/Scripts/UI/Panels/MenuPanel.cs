using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Systems.UISystem.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace GatesJam.UI
{
    public class MenuPanel : IboshPanel
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button gameplayButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        #region Event Subscription

        private void Start()
        {
            Show();
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            playButton.onClick.AddListener(OnPlayButtonClicked);
            gameplayButton.onClick.AddListener(OnGameplayButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);

            EventManagerProvider.Level.AddListener(LevelEvent.OnGameCompleted, HandleOnGameCompleted);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
            gameplayButton.onClick.RemoveListener(OnGameplayButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);

            EventManagerProvider.Level.RemoveListener(LevelEvent.OnGameCompleted, HandleOnGameCompleted);
        }

        #endregion

        #region Event Handling

        private void HandleOnGameCompleted()
        {
            Show();
        }

        #endregion

        #region Button Actions

        public void OnPlayButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnPlayButtonClicked);
            Hide();
        }

        public void OnGameplayButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnGameplayButtonClicked);
        }

        public void OnSettingsButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnSettingsButtonClicked);
        }

        public void OnQuitButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnQuitButtonClicked);
            Application.Quit();
        }

        #endregion
    }
}