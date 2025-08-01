using UnityEngine;

namespace Jumper
{
    public class DoubleJump : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            PowerupManager manager = other.GetComponent<PowerupManager>();
            if (manager != null)
            {
                manager.EnableDoubleJump();
                Destroy(gameObject);
            }
        }
    }
}
