using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IVisitable
{
    [SerializeField] private float playerHealthOnStart;
    private float _playerHealth;

    public System.Action<float, float> onHealthChanged;
    public System.Action onDie;

    public string PlayerName { get; set; }

    private void Awake()
    {
        _playerHealth = playerHealthOnStart;
    }

    public void GetVisitordamage(float damage)
    {
        _playerHealth -= damage;
        onHealthChanged?.Invoke(playerHealthOnStart, _playerHealth);
        if (_playerHealth <= 0)
        {
            onDie?.Invoke();
        }
    }
}
