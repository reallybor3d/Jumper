using UnityEngine;

namespace Jumper
{
    public class BulletScript : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}
