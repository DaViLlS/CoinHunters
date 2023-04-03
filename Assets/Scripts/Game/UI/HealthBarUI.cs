using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    public PlayerHealth PlayerHealth { get; set; }

    private void OnDisable()
    {
        PlayerHealth.onHealthChanged -= UpdateBar;
    }

    public void UpdateBar(float maxHealth, float health)
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
