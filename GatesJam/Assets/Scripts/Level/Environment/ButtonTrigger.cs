using UnityEngine;

namespace GatesJam.LevelManagement.Environment
{
    public class ButtonTrigger : MonoBehaviour
    {
        public MovablePlatform Platform;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Platform.IsButtonPressed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Platform.IsButtonPressed = false;
            }
        }
    }
}