using UnityEngine;

namespace GatesJam.LevelManagement
{
    public class Level : MonoBehaviour
    {
        private Section[] _sections;

        #region Built-In

        private void Awake()
        {
            _sections = GetComponentsInChildren<Section>();
        }

        #endregion

        #region Initialization

        public void Initialize()
        {
            Debug.Log("Level initialized");

            for (int i = 0; i < _sections.Length; i++)
            {
                Section section = _sections[i];
                section.Initialize(i + 1);
            }
        }

        #endregion

        #region Section Management

        public Section GetSectionByID(int id)
        {
            foreach (var section in _sections)
            {
                if (section.ID == id)
                {
                    return section;
                }
            }

            return null;
        }

        #endregion
    }
}
