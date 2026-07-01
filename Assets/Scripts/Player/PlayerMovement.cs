using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 7f;

        [Header("Gravity")]
        [SerializeField] private float gravity = -20f;

        private CharacterController _controller;
        private PlayerInputReader _input;

        private float _verticalVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputReader>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_controller.isGrounded && _verticalVelocity < 0)
                _verticalVelocity = -2f;

            var speed = _input.IsRunning ? runSpeed : walkSpeed;

            var direction =
                transform.forward * _input.Move.y +
                transform.right * _input.Move.x;

            direction.Normalize();

            _verticalVelocity += gravity * Time.deltaTime;

            direction *= speed;
            direction.y = _verticalVelocity;

            _controller.Move(direction * Time.deltaTime);
        }
    }
}