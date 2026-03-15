using IboshEngine.Runtime.Core.EventManagement;
using IboshEngine.Runtime.Systems.UISystem.Panels;

namespace GatesJam.UI
{
    public class GameplayPanel : PopupPanel
    {
        #region Event Subscription

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            EventManagerProvider.UI.AddListener(UIEvent.OnGameplayButtonClicked, HandleOnGameplayButtonClicked);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            EventManagerProvider.UI.RemoveListener(UIEvent.OnGameplayButtonClicked, HandleOnGameplayButtonClicked);
        }

        #endregion

        #region Event Handling

        private void HandleOnGameplayButtonClicked()
        {
            Show();
        }

        #endregion
    }
}