namespace IboshEngine.Runtime.Core.EventManagement
{
    /// <summary>
    /// Enum defining different types of UI events.
    /// </summary>
    public enum UIEvent
    {
        OnSettingsButtonClicked,
    }

    /// <summary>
    /// Enum defining different types of Data events.
    /// </summary>
    public enum DataEvent
    {
    }

    /// <summary>
    /// Enum defining different types of Camera events.
    /// </summary>
    public enum CameraEvent
    {
        OnNoneCameraStarted,
        OnNoneCameraCompleted,

        OnTopCameraStarted,
        OnTopCameraCompleted,
        OnBottomCameraStarted,
        OnBottomCameraCompleted,
        OnTopZoomedInCameraStarted,
        OnTopZoomedInCameraCompleted,
        OnBottomZoomedInCameraStarted,
        OnBottomZoomedInCameraCompleted,
    }

    /// <summary>
    /// Enum defining different types of Gameplay events.
    /// </summary>
    public enum GameplayEvent
    {
        OnSyncStarted,
        OnSyncEnded,
        OnDesyncStarted,
        OnDesyncEnded,
        OnCharacterSelectionUpdated,
        OnCharacterSelected,
        OnPlayerChanged,
    }

    /// <summary>
    /// Enum defining different types of Level events.
    /// </summary>
    public enum LevelEvent
    {
        OnLevelLoaded,
        OnLevelStarted,
        OnLevelSucceeded,
        OnLevelFailed,
        OnLevelRestarted,
    }
}