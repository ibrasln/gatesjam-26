using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Systems.UISystem.Panels;

namespace GatesJam.UI
{
    public class FadePanel : IboshPanel
    {
        #region Event Subscription

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            EventManagerProvider.UI.AddListener(UIEvent.OnPlayButtonClicked, HandleOnPlayButtonClicked);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelSucceeded, HandleOnLevelSucceeded);
            EventManagerProvider.Level.AddListener(LevelEvent.OnLevelLoaded, HandleOnLevelLoaded);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            EventManagerProvider.UI.RemoveListener(UIEvent.OnPlayButtonClicked, HandleOnPlayButtonClicked);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelSucceeded, HandleOnLevelSucceeded);
            EventManagerProvider.Level.RemoveListener(LevelEvent.OnLevelLoaded, HandleOnLevelLoaded);
        }

        #endregion

        #region Event Handling

        private void HandleOnPlayButtonClicked()
        {
            Show();
            ShowBlockingOverlay();
        }

        private void HandleOnLevelSucceeded()
        {
            Show();
            ShowBlockingOverlay();
        }

        private void HandleOnLevelLoaded()
        {
            Hide();
            HideBlockingOverlay();
        }

        #endregion
    }
}