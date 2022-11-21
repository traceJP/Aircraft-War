using Room;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {

        public float speed;
        
        public float shootRate;

        public GameObject bulletPrefab;


        private void Start()
        {
            InvokeRepeating(nameof(Shoot), 0, shootRate);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Contains(RoomTag.Player))
            {
                PlayerHealth.Instance.UpdateHp(-20);
                AudioManager.Instance.PlaySound("xiaobaozai");
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(0, 0, speed * Time.fixedDeltaTime, Space.World);
            if (transform.position.z < Map2DRange.BottomRange - 1)
            {
                Destroy(gameObject);
            }
        }
        
        private void Shoot()
        {
            foreach (Transform pos in transform)
            {
                Instantiate(bulletPrefab)
                    .transform.position = pos.position;
            }
        }

    }
}