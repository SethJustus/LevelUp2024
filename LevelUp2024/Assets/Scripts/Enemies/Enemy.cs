using UnityEngine;

public class Enemy : HealthObject
{
    #region Methods
    protected override void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
