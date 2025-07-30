using UnityEngine;

namespace Jumper
{
    [RequireComponent(typeof(Camera))]
    public class OrbitCamera : MonoBehaviour
    {
        [Header("Follow Target")]
        [SerializeField] private Transform focus;

        [Header("Camera Offset")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -6f);

        [Header("Follow Smoothing")]
        [SerializeField] private float followSpeed = 10f;

        private Vector3 currentVelocity;

        private void LateUpdate()
        {
            if (focus == null) return;

            // Calculate target position
            Vector3 targetPosition = focus.position + focus.TransformDirection(offset);

            // Smoothly move the camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 1f / followSpeed);

            // Always look at the player
            transform.LookAt(focus);
        }
    }
}
