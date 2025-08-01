using UnityEngine;

namespace Jumper
{
    public class PowerupManager : MonoBehaviour
    {
        [Header("Powerup States")]
        public bool hasDoubleJump = false;
        // You can add more later like:
        // public bool hasSpeedBoost = false;

        public void EnableDoubleJump()
        {
            hasDoubleJump = true;
            Debug.Log("Double Jump Enabled!");
        }

        // Add other powerup methods here
    }
}
