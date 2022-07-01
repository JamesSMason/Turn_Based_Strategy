using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int maxHealth;

    public event EventHandler OnDead;
    public event EventHandler OnDamaged;

    private void Start()
    {
        maxHealth = health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
        }

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }
    }

    public float GetHealthNormalized()
    {
        return (float) health / maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }
}