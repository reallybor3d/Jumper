using UnityEngine;

namespace Jumper
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementScript : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float speed = 8f;
        public float rotationSpeed = 720f;
        public float jumpForce = 10f;

        [Header("Physics Settings")]
        public float gravityMultiplier = 2.5f;

        [Header("Powerups")]
        private bool canDoubleJump = false;
        [HideInInspector] public bool hasDoubleJumpPowerup = false;

        private Rigidbody rb;
        private CapsuleCollider col;

        private Vector3 moveDirection;
        private bool jumpRequested = false;
        private bool isGrounded = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horizontal, 0f, vertical);
            if (moveDirection.sqrMagnitude > 1f)
                moveDirection.Normalize();

            // Rotate to face direction
            if (moveDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpRequested = true;
            }
        }

        void FixedUpdate()
        {
            // Manual ground check
            isGrounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);

            // Move player
            Vector3 move = moveDirection * speed;
            move.y = rb.linearVelocity.y; // preserve vertical velocity
            rb.linearVelocity = move;

            // Handle jump
            if (jumpRequested)
            {
                if (isGrounded)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                    canDoubleJump = hasDoubleJumpPowerup;
                }
                else if (canDoubleJump)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                    canDoubleJump = false;
                }

                jumpRequested = false;
            }

            // Apply extra gravity if falling
            if (!isGrounded && rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
    }
}
