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


        
        private void Start()
        {
            InvokeRepeating(nameof(Shoot), 0, shootRate);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (transform.position.z > 3)
            {
                transform.Translate(0, 0, -0.5f * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                
                // TODO：这里准备好了可以开始发射子弹了
                
                if (transform.position.x < Map2DRange.LeftRange + 1.3 || transform.position.x > Map2DRange.RightRange - 1.3)
                {
                    speed *= -1;
                }
                transform.Translate(speed * Time.fixedDeltaTime, 0, 0, Space.World);
            }
        }
        
        
        
        private void Shoot()
        {
            var index = 0;
            foreach (Transform pos in transform)
            {
                var bullet = Instantiate(bulletPrefabs[index++]);
                bullet.transform.position = pos.position;
            }
        }
        
        
        
        
        
        
    }
}