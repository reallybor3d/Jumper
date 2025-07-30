// using UnityEngine;
// using KBCore.Refs;
// using Unity.Cinemachine;
// using System;

// namespace Jumper
// {
//     public class PlayerController : ValidatedMonoBehaviour
//     {
//         [Header("References")]
//         [SerializeField, Self] CharacterController characterController;
//         [SerializeField, Self] Animator animator;
//         [SerializeField, Anywhere] CinemachineCamera freeLookVCam;
//         [SerializeField, Anywhere] InputReader input;

//         [Header("Settings")]
//         [SerializeField] float moveSpeed = 6f;
//         [SerializeField] float rotationSpeed = 15f;
//         [SerializeField] float smoothTime = 0.2f;

//         const float ZeroF = 0f;

//         Transform mainCam;
//         float currentSpeed;
//         float velocity;
//         Vector3 movement;

//         static readonly int Speed = Animator.StringToHash("Speed");

//         void Awake()
//         {
//             // Get the main camera’s transform for movement direction
//             mainCam = Camera.main.transform;

//             // Ensure the Cinemachine camera follows and looks at this object
//             freeLookVCam.Follow = transform;
//             freeLookVCam.LookAt = transform;

//             // Removed: Obsolete OnTargetObjectWarped call
//         }

//         void Update()
//         {
//             HandleMovement();
//             UpdateAnimator();
//         }

//         private void UpdateAnimator()
//         {
//             animator.SetFloat(Speed, currentSpeed);
//         }

//         void HandleMovement()
//         {
//             // Get input movement vector from your InputReader (assuming it's being set somewhere)
//             movement = input.Movement;

//             // Convert movement to camera-relative direction
//             var cameraForward = Vector3.Scale(mainCam.forward, new Vector3(1, 0, 1)).normalized;
//             var cameraRight = mainCam.right;

//             var adjustedDirection = cameraForward * movement.y + cameraRight * movement.x;

//             if (adjustedDirection.magnitude > ZeroF)
//             {
//                 HandleRotation(adjustedDirection);
//                 HandleHorizontalMovement(adjustedDirection);
//                 SmoothSpeed(adjustedDirection.magnitude);
//             }
//             else
//             {
//                 SmoothSpeed(ZeroF);
//             }
//         }

//         void HandleHorizontalMovement(Vector3 adjustedDirection)
//         {
//             var adjustedMovement = adjustedDirection * (moveSpeed * Time.deltaTime);
//             characterController.Move(adjustedMovement);
//         }

//         void HandleRotation(Vector3 adjustedDirection)
//         {
//             var targetRotation = Quaternion.LookRotation(adjustedDirection);
//             transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }

//         void SmoothSpeed(float value)
//         {
//             currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
//         }
//     }
// }
