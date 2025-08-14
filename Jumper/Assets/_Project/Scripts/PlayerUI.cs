using TMPro;
using UnityEngine;


namespace Jumper
{
    public class PlayerUI : MonoBehaviour
    {
        public Rigidbody playerRigidbody;     // Assign your player Rigidbody
        public TextMeshProUGUI speedText;     // Assign the UI Text in Inspector

        void Update()
        {
            float speed = playerRigidbody.linearVelocity.magnitude;
            speedText.text = "Speed: " + speed.ToString("F2");
        }
    }
}
