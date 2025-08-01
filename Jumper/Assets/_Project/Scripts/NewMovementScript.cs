using UnityEngine;

namespace Jumper
{
    public class NewMovementScript : MonoBehaviour
    {

        private PowerupManager powerupManager;
        private bool canDoubleJump = false;
        private int jumpCount = 0;
        private int maxJumps = 1;

        [Header("Movement")]
        public float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;

        public float groundDrag;

        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        [Header("Keybinds")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        [Header("Slope Handling")]
        public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitingSlope;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        Rigidbody rb;

        public MovementState state;
        public enum MovementState
        {
            walking,
            sprinting,
            air
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            readyToJump = true;

            powerupManager = GetComponent<PowerupManager>();
        }

        private void Update()
        {
            //ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();
            StateHandler();

            //drag handeling
            if (grounded)
                rb.linearDamping = groundDrag;
            else
                rb.linearDamping = 0;

            if (grounded)
                jumpCount = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            //jump
            if (Input.GetKey(jumpKey) && readyToJump && jumpCount < GetMaxJumps())
            {
                readyToJump = false;

                Jump();

                jumpCount++;

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private int GetMaxJumps()
        {
            return (powerupManager != null && powerupManager.hasDoubleJump) ? 2 : 1;
        }

        private void StateHandler()
        {
            //Sprinting
            if (grounded && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }

            //Walking
            else if (grounded)
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }

            //Air
            else
            {
                state = MovementState.air;
            }
        }

        private void MovePlayer()
        {
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            //on slope
            if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

                if (rb.linearVelocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);

            }

            // on ground
            else if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            // in air
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            // grav off when on slope
            rb.useGravity = !OnSlope();
        }

        private void SpeedControl()
        {

            // Limit speed on slope
            if (OnSlope() && !exitingSlope)
            {
                if (rb.linearVelocity.magnitude > moveSpeed)
                    rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
            }

            //Limit speed on ground/air
            else
            {
                Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

                // limit velocity if needed
                if (flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
                }
            }
        }

        private void Jump()
        {

            exitingSlope = true;

            // reset y velocity
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        }

        private void ResetJump()
        {
            readyToJump = true;

            exitingSlope = false;

            canDoubleJump = powerupManager != null && powerupManager.hasDoubleJump;
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        }
    }
}
