using Core;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private PlayerConfig config;

        [Header("Gravity")]
        [SerializeField] private float gravity = -20f;

        private CharacterController _controller;
        private PlayerInputReader _input;
        private SignalBus _signalBus;

        private float _verticalVelocity;
        private bool _enabled = true;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputReader>();
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void Update()
        {
            if (!_enabled) 
                return;
            
            Move();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Move()
        {
            if (_controller.isGrounded && _verticalVelocity < 0)
                _verticalVelocity = -2f;

            var speed = _input.IsRunning ? config.SprintSpeed : config.WalkSpeed;

            var direction =
                transform.forward * _input.Move.y +
                transform.right * _input.Move.x;

            direction.Normalize();

            _verticalVelocity += gravity * Time.deltaTime;

            direction *= speed;
            direction.y = _verticalVelocity;

            _controller.Move(direction * Time.deltaTime);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.State != GameState.Playing)
                _enabled = false;
        }
    }
}