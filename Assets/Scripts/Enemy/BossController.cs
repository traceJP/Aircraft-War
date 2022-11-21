using System.Collections.Generic;
using Room;
using UnityEngine;

namespace Enemy
{
    public class BossController : MonoBehaviour
    {
        
        public float speed;
        
        public float shootRate;

        public List<GameObject> bulletPrefabs;

        private bool _allowShoot;
        
        
        private float _timer;
        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_allowShoot && _timer > shootRate)
            {
                _timer = 0;
                Shoot();
            }
            
            Move();
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Contains(RoomTag.Player))
            {
                PlayerHealth.Instance.UpdateHp(-2);
                BossHealth.Instance.UpdateHp(-10);
                AudioManager.Instance.PlaySound("xiaobaozai");
            }
        }

        private void Move()
        {
            if (transform.position.z > 3)
            {
                transform.Translate(0, 0, -0.5f * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                _allowShoot = true;
                if (transform.position.x < Map2DRange.LeftRange + 1.3 || transform.position.x > Map2DRange.RightRange - 1.3)
                {
                    speed *= -1;
                }
                transform.Translate(speed * Time.fixedDeltaTime, 0, 0, Space.World);
            }
        }
        
        private void Shoot()
        {
            for (var i = 0; i < bulletPrefabs.Count; i++)
            {
                Instantiate(bulletPrefabs[i])
                    .transform.position = transform.GetChild(i).position;
            }
        }
        
    }
}