using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Systems.UISystem.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace GatesJam.UI
{
    public class FailPanel : PopupPanel
    {
        [SerializeField] private Button restartButton;

        #region Event Subscription

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelFailed, HandleOnLevelFailed);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelFailed, HandleOnLevelFailed);
        }

        #endregion

        #region Event Handling

        private void HandleOnLevelFailed()
        {
            Show();
        }

        #endregion

        #region Button Actions

        public void OnRestartButtonClicked()
        {
            EventManagerProvider.UI.Broadcast(UIEvent.OnRestartButtonClicked);
            Hide();
        }

        #endregion
    }
}