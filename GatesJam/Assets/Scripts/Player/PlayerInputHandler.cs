using UnityEngine;
using UnityEngine.InputSystem;

namespace GatesJam.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public int XInput { get; private set; }
        public int YInput { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public bool JumpInput { get; private set; }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            XInput = Mathf.RoundToInt(moveInput.x);
            YInput = Mathf.RoundToInt(moveInput.y);
            MovementInput = moveInput;
            Debug.Log($"Move Input: {MovementInput}");
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpInput = true;
                Debug.Log("Jump Input: " + JumpInput);
            }
            else if (context.canceled)
            {
                JumpInput = false;
                Debug.Log("Jump Input: " + JumpInput);
            }
        }
    }
}