using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputReader : MonoBehaviour
    {
        private const string MoveInputActionName = "Move";
        private const string LookInputActionName = "Look";
        private const string SprintInputActionName = "Sprint";
        
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool IsRunning { get; private set; }

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

            _playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnDestroy()
        {
            _playerInput.onActionTriggered -= OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case MoveInputActionName:
                    Move = context.ReadValue<Vector2>();
                    break;

                case LookInputActionName:
                    Look = context.ReadValue<Vector2>();
                    break;

                case SprintInputActionName:
                    IsRunning = context.ReadValueAsButton();
                    break;
            }
        }
    }
}