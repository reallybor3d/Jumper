using TMPro;
using UnityEngine;


namespace Jumper
{
    public class PlayerUI : MonoBehaviour
    {
        public Rigidbody playerRigidbody;   
        public PowerupManager powerupManager;
        public TextMeshProUGUI speedText;    
        public TextMeshProUGUI powerupsText;

        void Update()
        {
            if (playerRigidbody != null && speedText != null)
            {
                float speed = playerRigidbody.linearVelocity.magnitude;
                speedText.text = "Speed: " + speed.ToString("F2");
            }

            if (powerupsText != null)
            {
                UpdatePowerupDisplay();
            }
        }

        void UpdatePowerupDisplay()
        {
            if (powerupManager == null)
            {
                powerupsText.text = "Powerups: None";
                return;
            }

            string activePowerups = "Powerups: ";
            bool hasPowerups = false;

            if (powerupManager.hasDoubleJump)
            {
                activePowerups += "Double Jump ";
                hasPowerups = true;
            }

            if (powerupManager.hasSuperJump)
            {
                activePowerups += "Super Jump ";
                hasPowerups = true;
            }

            if (powerupManager.hasGlider)
            {
                activePowerups += "Glider ";
                hasPowerups = true;
            }

            if (!hasPowerups)
            {
                activePowerups += "None";
            }

            powerupsText.text = activePowerups;
        }
    }
}
