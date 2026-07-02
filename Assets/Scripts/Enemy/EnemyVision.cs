using Core;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private EnemyConfig config;
        [SerializeField] private Transform eyes;
        [SerializeField] private LayerMask obstacleMask;

        public Transform Player => _player;
        
        private SignalBus _signalBus;
        
        private Transform _player;
        private bool _enabled = true;

        private void Awake()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
        
        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
        
        [Inject]
        public void Construct(Player.Player player, SignalBus signalBus)
        {
            _player = player.transform;
            _signalBus = signalBus;
        }

        public bool CanSeePlayer()
        {
            if (!_enabled)
                return false;
            
            Vector3 direction = _player.position - eyes.position;

            float distance = direction.magnitude;

            if (distance > config.ViewDistance)
                return false;

            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > config.ViewAngle * 0.5f)
                return false;

            if (Physics.Raycast(
                    eyes.position,
                    direction.normalized,
                    distance,
                    obstacleMask))
            {
                return false;
            }

            return true;
        }
        
        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.State != GameState.Playing)
                _enabled = false;
        }
    }
}