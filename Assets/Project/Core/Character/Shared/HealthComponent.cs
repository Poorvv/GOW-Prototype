using UnityEngine;

public class HealthComponent: MonoBehaviour
{
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
        if (_currentHealth <= 0)
        {
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