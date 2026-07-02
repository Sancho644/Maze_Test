using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float acceleration = 8f;
        [SerializeField] private float stoppingDistance = 0.2f;

        [Header("Vision")]
        [SerializeField] private float viewDistance = 8f;
        [SerializeField] private float viewAngle = 90f;

        public float MoveSpeed => moveSpeed;
        public float Acceleration => acceleration;
        public float StoppingDistance => stoppingDistance;

        public float ViewDistance => viewDistance;
        public float ViewAngle => viewAngle;
    }
}