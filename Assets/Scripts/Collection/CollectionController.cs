using Room;
using UnityEngine;

namespace Collection
{
    public class CollectionController : MonoBehaviour
    {
        
        public float speed;
        
        public int posIndex;
        
        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Contains(RoomTag.Player))
            {
                other.GetComponent<Player>().ChangePos(posIndex);
                AudioManager.Instance.PlaySound("shiqu");
                if (posIndex == 2)
                {
                    AudioManager.Instance.PlaySound("effcet_vo_tiyanzhandouliba");
                }
                Destroy(gameObject);
            }
        }

        private void Move()
        {
            transform.Translate(0, 0, speed * Time.fixedDeltaTime, Space.World);
            if (transform.position.z < Map2DRange.BottomRange - 1)
            {
                Destroy(gameObject);
            }
        }

    }
}