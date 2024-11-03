using System;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    #region Parameters
    public int Health { get; private set; } = 100;

    [SerializeField] private int MaxHealth = 100;
    #endregion
    
    #region Methods
    public void TakeDamage(int damage)
    {
        this.Health -= damage;
        if (this.Health <= 0)
        {
            this.Die();
        }
    }

    public void Heal(int heal)
    {
        this.Health += heal;
    }

    protected virtual void Die()
    {
        throw new NotImplementedException();
    }
    #endregion
}
