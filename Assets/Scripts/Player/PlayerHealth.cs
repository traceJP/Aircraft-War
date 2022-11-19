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

    [SerializeField]
    private float currentHp;

    [SerializeField]
    private Slider slider;

    public event Action DeathEvent;
    

    protected override void Awake()
    {
        base.Awake();
        currentHp = slider.value = maxHp;
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
        if (currentHp - value <= minHp)
        {
            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
        currentHp += value;
    }
    
    private void ShowHpBySlider()
    {
        slider.value = currentHp;
    }
    
    private void KaiGua()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHp = maxHp;
        }
    }

}
