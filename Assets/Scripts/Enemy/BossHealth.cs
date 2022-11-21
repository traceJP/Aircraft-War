using Room;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Enemy
{
    public class BossHealth : Singleton<BossHealth>
    {
        
            [SerializeField]
            private float maxHp = 100;
        
            [SerializeField]
            private float minHp;
            
            private float _currentHp;
        
            [SerializeField]
            private Slider slider;

            protected override void Awake()
            {
                base.Awake();
                InitLevel(RoomController.Level);
                _currentHp = slider.value = maxHp;
                slider.maxValue = maxHp;
                slider.minValue = minHp;
            }

            private void InitLevel(Level level)
            {
                switch (level)
                {
                    case Level.Easy:
                        maxHp += RoomController.Instance.createBossCount * 200;
                        break;
                    case Level.Normal:
                        maxHp += 1000;
                        maxHp += RoomController.Instance.createBossCount * 300;
                        break;
                    case Level.Hard:
                        maxHp += 2000;
                        maxHp += RoomController.Instance.createBossCount * 500;
                        break;
                }
                
            }

            private void FixedUpdate()
            {
                ShowHpBySlider();
            }
            
            public void UpdateHp(int value)
            {
                VFXManager.Instance.CreateVFXRange(transform.position);
                if (_currentHp - value <= minHp)
                {
                    RoomController.Instance.hasBoss = false;
                    VFXManager.Instance.CreateVFX(transform.position, "explosion_enemy");
                    AudioManager.Instance.PlaySound("effcet_vo_ruo");
                    AudioManager.Instance.PlaySound("baoz");
                    Score.Instance.UpdateScore(10000);
                    Destroy(gameObject);
                }
                _currentHp += value;
            }
            
            private void ShowHpBySlider()
            {
                slider.value = _currentHp;
            }

    }
}