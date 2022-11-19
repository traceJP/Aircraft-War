using System;
using Room;
using UI;
using UnityEngine;

namespace Bullet
{
    
    public enum BulletType : short {
        Player,
        Enemy,
    }
    
    public class BulletController : MonoBehaviour
    {

        public float speed;

        public BulletType bulletType;

        private Rigidbody _rb;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (bulletType == BulletType.Enemy)
            {
                speed = -speed;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (transform.localEulerAngles.y == 0)
            {
                transform.Translate(0, 0, speed * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                _rb.AddRelativeForce(-transform.forward * (speed * Time.fixedDeltaTime * 100));
            }

            if (transform.position.z > Map2DRange.TopRange + 1 || transform.position.z < Map2DRange.BottomRange - 1)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (bulletType)
            {
                case BulletType.Player:
                    PlayerBulletTrigger(other.gameObject);
                    break;
                case BulletType.Enemy:
                    EnemyBulletTrigger(other.gameObject);
                    break;
                default:
                    Debug.LogWarning("子弹碰撞错误，未检测到子弹的类型");
                    break;
            }
        }

        private void PlayerBulletTrigger(GameObject other)
        {
            if (other.tag.Contains(RoomTag.Enemy))
            {
                Score.Instance.UpdateScore(100);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

            if (other.tag.Contains(RoomTag.Boss))
            {
                
                // TODO: boss扣血
                
                Destroy(gameObject);
            }
                
            if (other.tag.Contains(RoomTag.Bullet) && other.GetComponent<BulletController>().bulletType == BulletType.Enemy)
            {
                Score.Instance.UpdateScore(10);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }

        private void EnemyBulletTrigger(GameObject other)
        {
            if (other.tag.Contains(RoomTag.Player))
            {
                // 减血量
                PlayerHealth.Instance.UpdateHp(-10);
                Destroy(gameObject);
            }
        }
        
    }
}