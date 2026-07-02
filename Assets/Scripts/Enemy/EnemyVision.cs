using Core;
using Core.Signals;
using UnityEngine;
using Zenject;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

        private void OnEnable()
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
            
            var direction = _player.position - eyes.position;

            var distance = direction.magnitude;

            if (distance > config.ViewDistance)
                return false;

            var angle = Vector3.Angle(transform.forward, direction);

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
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (eyes == null || config == null)
                return;

            Handles.color = new Color(1f, 1f, 0f, 0.15f);

            Handles.DrawSolidArc(
                eyes.position,
                Vector3.up,
                Quaternion.Euler(0, -config.ViewAngle / 2, 0) * transform.forward,
                config.ViewAngle,
                config.ViewDistance);

            Handles.color = Color.yellow;

            Handles.DrawWireArc(
                eyes.position,
                Vector3.up,
                Quaternion.Euler(0, -config.ViewAngle / 2, 0) * transform.forward,
                config.ViewAngle,
                config.ViewDistance);
        }
#endif
    }
}