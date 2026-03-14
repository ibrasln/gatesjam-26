using IboshEngine.Runtime.Utilities.Singleton;

namespace GatesJam.LevelManagement
{
    public class LevelManager : IboshSingleton<LevelManager>
    {
        [ReadOnly] public Level CurrentLevel;
        [SerializeField] private Level[] levels;

        private int _currentLevelIndex;


        #region Level Management

        public void LoadLevel(string levelName)
        {

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