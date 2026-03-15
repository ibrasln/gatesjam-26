using Sirenix.OdinInspector;
using UnityEngine;

namespace GatesJam.LevelManagement.Environment
{
    public class MovablePlatform : MonoBehaviour
    {
        public Transform StartPoint;
        public Transform EndPoint;
        public float Speed = 3.0f;

        [ReadOnly] public bool IsButtonPressed = false;

        private void FixedUpdate()
        {
            Vector2 targetPosition = IsButtonPressed ? EndPoint.position : StartPoint.position;

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                Speed * Time.fixedDeltaTime
            );
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                collision.transform.SetParent(transform);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                collision.transform.SetParent(null);
        }
    }
}