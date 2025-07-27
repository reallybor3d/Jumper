using System.Collections;
using Unity.Cinemachine;
using KBCore.Refs;
using UnityEngine;

namespace Platformer {
    public class CameraManager : ValidatedMonoBehaviour {
        [Header("References")]
        [SerializeField, Anywhere] Jumper.InputReader input;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;

        [Header("Settings")] 
        [SerializeField, Range(0.5f, 3f)] float speedMultiplier = 1f;
        [SerializeField] float minVerticalAngle = -60f;
        [SerializeField] float maxVerticalAngle = 60f;

        bool isRMBPressed;
        bool cameraMovementLock;

        float yaw;
        float pitch;

        void OnEnable() {
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

        void OnDisable() {
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        void Start() {
            // Initialize yaw and pitch from current camera rotation
            Vector3 euler = freeLookVCam.transform.eulerAngles;
            yaw = euler.y;
            pitch = euler.x;
        }

        void OnLook(Vector2 cameraMovement, bool isDeviceMouse) {
            if (cameraMovementLock) return;
            if (isDeviceMouse && !isRMBPressed) return;

            float deltaTime = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            // Adjust yaw and pitch
            yaw += cameraMovement.x * speedMultiplier * deltaTime * 100f;
            pitch -= cameraMovement.y * speedMultiplier * deltaTime * 100f;
            pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

            // Apply rotation to the camera
            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0);
            freeLookVCam.transform.rotation = targetRotation;
        }

        void OnEnableMouseControlCamera() {
            isRMBPressed = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(DisableMouseForFrame());
        }

        void OnDisableMouseControlCamera() {
            isRMBPressed = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        IEnumerator DisableMouseForFrame() {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;
        }
    }
}
