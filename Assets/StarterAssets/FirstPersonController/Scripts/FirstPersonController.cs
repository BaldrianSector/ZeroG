using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(Rigidbody))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ZeroGravitySyncedController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveForce = 50f;
        public float maxSpeed = 8f;

        [Header("Run")]
        public float runMultiplier = 2f;

        [Header("Rotation")]
        public float lookSensitivity = 1f;
        public float rollSpeed = 3f;

        [Header("References")]
        public GameObject cameraTarget;

        private Rigidbody rb;
        private StarterAssetsInputs input;
#if ENABLE_INPUT_SYSTEM
        private PlayerInput playerInput;
#endif

		private float yaw = 0f;
		private float pitch = 0f;

		private Quaternion targetRotation;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearDamping = 1.5f;
            rb.angularDamping = 4f;

            rb.interpolation = RigidbodyInterpolation.Interpolate;

            input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
            playerInput = GetComponent<PlayerInput>();
#endif
        }

        private void Update()
        {
            HandleLook();
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRoll();
        }
		private void HandleLook()
		{
			Vector2 lookInput = input.look;
			if (lookInput.sqrMagnitude < 0.01f) return;

			float multiplier = IsMouse() ? 1f : Time.deltaTime;

			float yawDelta = lookInput.x * lookSensitivity * multiplier;
			float pitchDelta = lookInput.y * lookSensitivity * multiplier;

			// Calculate local yaw and pitch axes from current rotation
			Vector3 yawAxis = rb.rotation * Vector3.up;
			Vector3 pitchAxis = rb.rotation * Vector3.right;

			// Create yaw and pitch rotations separately
			Quaternion yawRot = Quaternion.AngleAxis(yawDelta, yawAxis);
			Quaternion pitchRot = Quaternion.AngleAxis(pitchDelta, pitchAxis);

			// Apply both rotations to current rotation
			Quaternion newRotation = yawRot * pitchRot * rb.rotation;

			rb.MoveRotation(newRotation);
		}


        private void HandleMovement()
        {
            Vector3 moveInput = new Vector3(input.move.x, 0f, input.move.y);

            if (input.jump) moveInput.y += 1f;
            if (Keyboard.current.leftCtrlKey.isPressed) moveInput.y -= 1f;

            if (moveInput.sqrMagnitude > 1f)
                moveInput.Normalize();

            // Check if running
            float speedMultiplier = Keyboard.current.leftShiftKey.isPressed ? runMultiplier : 1f;

            // Move relative to full player rotation
            Vector3 worldMove = transform.TransformDirection(moveInput);

            if (rb.linearVelocity.magnitude < maxSpeed * speedMultiplier || Vector3.Dot(rb.linearVelocity.normalized, worldMove.normalized) < 0)
            {
                rb.AddForce(worldMove * moveForce * speedMultiplier, ForceMode.Force);
            }
        }

        private void HandleRoll()
        {
            float rollInput = 0f;
            if (Keyboard.current.qKey.isPressed) rollInput = 1f;
            if (Keyboard.current.eKey.isPressed) rollInput = -1f;

            if (rollInput != 0f)
            {
                Vector3 rollTorque = transform.forward * rollInput * rollSpeed;
                rb.AddTorque(rollTorque, ForceMode.Force);
            }
        }

        private bool IsMouse()
        {
#if ENABLE_INPUT_SYSTEM
            return playerInput.currentControlScheme == "KeyboardMouse";
#else
            return false;
#endif
        }
    }
}
