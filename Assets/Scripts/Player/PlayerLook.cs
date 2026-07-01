using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerLook  : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform cameraRoot;

        [Header("Settings")]
        [SerializeField] private float sensitivity = 0.1f;
        [SerializeField] private float maxLookAngle = 80f;

        private PlayerInputReader _input;

        private float _pitch;

        private void Awake()
        {
            _input = GetComponent<PlayerInputReader>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Look();
        }

        private void Look()
        {
            var lookInput = _input.Look * sensitivity;

            transform.Rotate(Vector3.up * lookInput.x);

            _pitch -= lookInput.y;
            _pitch = Mathf.Clamp(_pitch, -maxLookAngle, maxLookAngle);

            cameraRoot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }
    }
}