using DG.Tweening;
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
                transform.DOMoveY(transform.position.y - .3f, .2f);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Platform.IsButtonPressed = false;
                transform.DOMoveY(transform.position.y + .3f, 0.2f);
            }
        }
    }
}