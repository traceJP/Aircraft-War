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
                _currentHp = slider.value = maxHp;
                slider.maxValue = maxHp;
                slider.minValue = minHp;
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