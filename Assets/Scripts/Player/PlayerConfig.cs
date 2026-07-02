using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float sprintSpeed = 8f;

        [Header("Camera")]
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;

        public float WalkSpeed => walkSpeed;
        public float SprintSpeed => sprintSpeed;

        public float MouseSensitivity => mouseSensitivity;
        public float MinPitch => minPitch;
        public float MaxPitch => maxPitch;
    }
}