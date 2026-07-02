using System;
using Core;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerLook : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Transform cameraRoot;

        [Header("Settings")] 
        [SerializeField] private PlayerConfig config;

        private PlayerInputReader _input;
        private SignalBus _signalBus;

        private float _pitch;
        private bool _enabled = true;

        private void Awake()
        {
            _input = GetComponent<PlayerInputReader>();
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void Update()
        {
            if (!_enabled)
                return;

            Look();
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

        private void Look()
        {
            var lookInput = _input.Look * config.MouseSensitivity;

            transform.Rotate(Vector3.up * lookInput.x);

            _pitch -= lookInput.y;
            _pitch = Mathf.Clamp(_pitch, config.MinPitch, config.MaxPitch);

            cameraRoot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.State != GameState.Playing)
                _enabled = false;
        }
    }
}