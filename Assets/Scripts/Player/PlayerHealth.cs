using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerHealth : Singleton<PlayerHealth>
{
    
    [SerializeField]
    private float maxHp = 100;

    [SerializeField]
    private float minHp = 0;
    
    private float _currentHp;

    [SerializeField]
    private Slider slider;

    public event Action DeathEvent;
    

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

    private void Update()
    {
        KaiGua();
    }

    public void UpdateHp(int value)
    {
        VFXManager.Instance.CreateVFXRange(transform.position);
        if (_currentHp - value <= minHp)
        {
            VFXManager.Instance.CreateVFX(transform.position, "explosion_player");
            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
        _currentHp += value;
    }
    
    private void ShowHpBySlider()
    {
        slider.value = _currentHp;
    }
    
    private void KaiGua()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _currentHp = maxHp;
        }
    }

}
