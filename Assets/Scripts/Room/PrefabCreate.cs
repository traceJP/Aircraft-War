using System;
using System.Collections.Generic;
using Room;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Room
{
    public class PrefabCreate : MonoBehaviour
    {

        public float createEnemyRate;

        public float createCollectionRate;

        public float createBossRate;

        public List<GameObject> enemies;

        public List<GameObject> collections;

        public List<GameObject> bosses;

        private bool _hasBoss;

        private void Start()
        {
            
            InvokeRepeating(nameof(CreateEnemy), 0, createEnemyRate);
            InvokeRepeating(nameof(CreateCollection), 0, createCollectionRate);
            
        }

        private void FixedUpdate()
        {
            
            
        }

        private void CreateEnemy()
        {
            InstantiateAndInit(enemies[Random.Range(0, enemies.Count)]);
        }
        
        private void CreateCollection()
        {
            InstantiateAndInit(collections[Random.Range(0, collections.Count)]);
        }
        
        private void CreateBoss()
        {
            var obj =(bosses[Random.Range(0, bosses.Count)]);
            
            
            
            
        }

        private GameObject InstantiateAndInit(GameObject prefab)
        {
            var position = new Vector3(Random.Range(Map2DRange.LeftRange + 1, Map2DRange.RightRange - 1), 1, 8);
            var obj = Instantiate(prefab);
            obj.transform.position = position;
            return obj;
        }

    }
}