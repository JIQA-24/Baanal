using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Color low;
    public Color high;

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        fill.color = Color.Lerp(low, high, slider.normalizedValue);
    }

    public void DeactivateHealthBar()
    {
        slider.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
