using UnityEngine;

namespace Jumper
{
    public class MovementScript : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float speed = 8f;
        public float rotationSpeed = 720f;
        public float jumpSpeed = 10f;

        private float ySpeed = 0f;

        [Header("Physics Settings")]
        public float gravityMultiplier = 2.5f;

        [Header("Powerups")]
        private bool canDoubleJump = false;
        [HideInInspector] public bool hasDoubleJumpPowerup = false;

        [Header("Camera Reference")]
        public Transform cameraTransform;

        private CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        //if you're reading this great job :O you found the secret

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // input to camera movement
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

            Vector3 inputDirection = (camForward * vertical + camRight * horizontal);
            if (inputDirection.sqrMagnitude > 1f)
            {
                inputDirection.Normalize();
            }

            // Apply gravity and jump logic
            if (controller.isGrounded)
            {
                ySpeed = -0.5f;
                canDoubleJump = hasDoubleJumpPowerup;

                if (Input.GetButtonDown("Jump"))
                {
                    ySpeed = jumpSpeed;
                }
            }
            else
            {
                ySpeed += (ySpeed < 0)
                    ? Physics.gravity.y * gravityMultiplier * Time.deltaTime
                    : Physics.gravity.y * Time.deltaTime;

                if (Input.GetButtonDown("Jump") && canDoubleJump)
                {
                    ySpeed = jumpSpeed;
                    canDoubleJump = false;
                }
            }

            // Final movement
            Vector3 velocity = inputDirection * speed;
            velocity.y = ySpeed;
            controller.Move(velocity * Time.deltaTime);

            // Rotate character to face movement direction
            if (inputDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
