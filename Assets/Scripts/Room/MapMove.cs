using System;
using UnityEngine;

namespace Room
{
    public class MapMove : MonoBehaviour
    {

        public float speed;

        private MeshRenderer _mash;

        private void Awake()
        {
            _mash = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            _mash.material.mainTextureOffset = new Vector2(0, Time.time * Time.fixedDeltaTime * speed);
        }
    }
}