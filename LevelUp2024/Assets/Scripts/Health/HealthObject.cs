using System;
using System.Linq;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    #region Parameters
    public int Health { get; private set; } = 100;

    [SerializeField] private int MaxHealth = 100;

    private IHealthBar _healthBar;
    #endregion
    
    #region Unity Methods

    void Awake()
    {
        // Can't serialize interface in unity, so I am getting it manually here
        this._healthBar = GetComponentInChildren<IHealthBar>();
    }

    #endregion
    
    #region Methods
    public void TakeDamage(int damage)
    {
        this.Health -= damage;
        
        this._healthBar.UpdateUI(this.Health, this.MaxHealth);
        if (this.Health <= 0)
        {
            this.Die();
        }
    }

    public void Heal(int heal)
    {
        this.Health += heal;
        this._healthBar.UpdateUI(this.Health, this.MaxHealth);
    }

    protected virtual void Die()
    {
        throw new NotImplementedException();
    }
    #endregion
}
