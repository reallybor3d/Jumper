using UnityEngine;

namespace Jumper
{
    public class PowerupManager : MonoBehaviour
    {
        [Header("Powerup States")]
        public bool hasDoubleJump = false;
        public bool hasSuperJump = false;
        public bool hasGlider = false;

        public void EnableDoubleJump()
        {
            hasDoubleJump = true;
            Debug.Log("Double Jump Enabled!");
        }

        public void EnableSuperJump()
        {
            hasSuperJump = true;
            Debug.Log("Super Jump Enabled!");
        }

        public void EnableGlider()
        {
            hasGlider = true;
            Debug.Log("Glider Enabled!");
        }
    }
}
