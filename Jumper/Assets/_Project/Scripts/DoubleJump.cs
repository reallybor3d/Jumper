using UnityEngine;

namespace Jumper
{
    public class DoubleJumpPowerup : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MovementScript movement = other.GetComponent<MovementScript>();
                if (movement != null)
                {
                    movement.hasDoubleJumpPowerup = true; 
                    Debug.Log("Double jump power-up collected!");
                }

                Destroy(gameObject); 
            }
        }
    }
}
