using System;
using Room;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var next = new Vector3(Input.GetAxis("Horizontal"), 1, Input.GetAxis("Vertical"));
        transform.Translate(next * (speed * Time.deltaTime), Space.World);
        CheckEdge();
    }

    private void CheckEdge()
    {
        var position = transform.position;
        var posX = Mathf.Clamp(position.x, Map2DRange.LeftRange, Map2DRange.RightRange);
        var posZ = Mathf.Clamp(position.z, Map2DRange.BottomRange, Map2DRange.TopRange);
        position = new Vector3(posX, 1, posZ);
        transform.position = position;
    }
    
}