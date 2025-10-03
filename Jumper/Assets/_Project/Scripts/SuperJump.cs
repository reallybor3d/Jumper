using UnityEngine;

namespace Jumper
{
    public class SuperJump : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            PowerupManager manager = other.GetComponent<PowerupManager>();
            if (manager != null)
            {
                manager.EnableSuperJump();
                Destroy(gameObject);
            }
        }
    }
}
