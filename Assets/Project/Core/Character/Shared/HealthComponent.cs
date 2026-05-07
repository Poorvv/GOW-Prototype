using System;
using UnityEngine;

public class HealthComponent: MonoBehaviour
{
    public Action<AttackInfo> OnDamageTaken;
    public Action OnDeath;
    [SerializeField] private int maxHealth = 100;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(AttackInfo attackInfo)
    {
        _currentHealth -= attackInfo.Damage;
        Debug.Log($"{gameObject.name} took {attackInfo.Damage} damage");
        OnDamageTaken?.Invoke(attackInfo);
        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Die();
        }
    }

    private void Die()
    {
        // Handle death logic here
        Destroy(gameObject);
        Debug.Log($"{gameObject.name} has died.");
    }
}