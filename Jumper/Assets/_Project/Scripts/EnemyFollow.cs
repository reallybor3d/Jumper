using UnityEngine;
using UnityEngine.AI;

namespace Jumper
{
    public class EnemyFollow : MonoBehaviour
    {

        public NavMeshAgent enemy;
        public Transform player;

        [SerializeField] private float timer = 5;
        private float bulletTime;

        public GameObject enemyBullet;
        public Transform spawnPoint;
        public float enemySpeed;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 target = player.position;
            target.y = transform.position.y;
            enemy.SetDestination(target);

            if (Vector3.Distance(transform.position, player.position) > enemy.stoppingDistance)
            {
                ShootAtPlayer();
            }
        }

        void ShootAtPlayer()
        {
            bulletTime -= Time.deltaTime;

            if (bulletTime > 0) return;

            if (enemyBullet == null || spawnPoint == null) return;

             float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > enemy.stoppingDistance + 2f) return; // only shoot when within range

            bulletTime = timer;

            GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, spawnPoint.rotation);
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

            if (bulletRig != null)
            {
                bulletRig.AddForce(bulletRig.transform.forward * enemySpeed, ForceMode.Impulse);
            }

            Destroy(bulletObj, 5f);
        }
    }
}
