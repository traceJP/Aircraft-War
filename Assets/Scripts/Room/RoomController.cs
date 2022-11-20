using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Room
{
    public class RoomController : Singleton<RoomController>
    {

        public float createEnemyRate;

        public float createCollectionRate;

        public float createBossRate;

        public List<GameObject> enemies;

        public List<GameObject> collections;

        public List<GameObject> bosses;

        [HideInInspector]
        public bool hasBoss;

        private bool _isStopAll;

        protected override void Awake()
        {
            base.Awake();
            PlayerHealth.Instance.DeathEvent += () => _isStopAll = true;
        }

        private float _timerEnemy;
        private float _timeCollection;
        private float _timeBoss;
        private void FixedUpdate()
        {
            
            if (_isStopAll)
            {
                return;
            }
            
            _timeCollection += Time.fixedDeltaTime;
            if (_timeCollection > createCollectionRate + 1)
            {
                _timeCollection = 0;
                CreateCollection();
            }

            if (!hasBoss)
            {
                _timerEnemy += Time.fixedDeltaTime;
                _timeBoss += Time.fixedDeltaTime;
                if (_timerEnemy > createEnemyRate)
                {
                    _timerEnemy = 0;
                    CreateEnemy();
                }
                if (_timeBoss > createBossRate)
                {
                    _timeBoss = 0;
                    CreateBoss();
                    hasBoss = true;
                }
            }

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
            var boss = InstantiateAndInit(bosses[Random.Range(0, bosses.Count)]);
            boss.transform.position = new Vector3(0, 1, 8);
        }

        private static GameObject InstantiateAndInit(GameObject prefab)
        {
            var position = new Vector3(Random.Range(Map2DRange.LeftRange + 1, Map2DRange.RightRange - 1), 1, 8);
            var obj = Instantiate(prefab);
            obj.transform.position = position;
            return obj;
        }

    }
}