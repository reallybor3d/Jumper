using UnityEngine;

namespace Jumper
{
    public class Glider : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            PowerupManager manager = other.GetComponent<PowerupManager>();
            if (manager != null)
            {
                manager.EnableGlider();
                Destroy(gameObject);
            }
        }
    }
}
