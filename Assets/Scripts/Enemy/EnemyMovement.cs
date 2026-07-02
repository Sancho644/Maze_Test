using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyConfig config;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.speed = config.MoveSpeed;
            _agent.acceleration = config.Acceleration;
            _agent.stoppingDistance = config.StoppingDistance;
        }

        public void MoveTo(Vector3 position)
        {
            _agent.isStopped = false;
            _agent.SetDestination(position);
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public bool ReachedDestination =>
            !_agent.pathPending &&
            _agent.remainingDistance <= _agent.stoppingDistance;

        public Vector3 Position => transform.position;
    }
}