using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
   #region Fields
   private IHealthBar _healthBar;
   private Guid _currentHealProcessId;
   #endregion
    
    #region Parameters
    
    public int Health { get; private set; } = 100;
    
    [Header("Health Settings")]
    
    [SerializeField] private int MaxHealth = 100;

    [SerializeField] private bool AutoHeal = false;

    [SerializeField] private float AutoHealDelaySecs = 3f;
    
    [SerializeField] private float AutoHealRate = 0.1f;
    #endregion
    
    #region Unity Methods

    void Awake()
    {
        // Can't serialize interface in unity, so I am getting it manually here
        this._healthBar = GetComponentInChildren<IHealthBar>();
    }

    #endregion
    
    #region Methods
    public virtual void TakeDamage(int damage)
    {
        this.Health -= damage;
        
        this._healthBar.UpdateUI(this.Health, this.MaxHealth);

        if (this.AutoHeal)
        {
            this.StartCoroutine(this.HealUp());
        }
        
        if (this.Health <= 0)
        {
            this.Die();
        }
    }

    public virtual void Heal(int heal)
    {
        this.Health += heal;
        this._healthBar.UpdateUI(this.Health, this.MaxHealth);
    }

    protected virtual void Die()
    {
        throw new NotImplementedException();
    }

    IEnumerator HealUp()
    { 
        var processId = Guid.NewGuid();
        this._currentHealProcessId = processId;
        yield return new WaitForSeconds(AutoHealDelaySecs);
        
        while (this.Health < this.MaxHealth && this._currentHealProcessId == processId)
        {
           this.Heal(1);
           yield return new WaitForSeconds(this.AutoHealRate);
        }
    }

    #endregion
}
