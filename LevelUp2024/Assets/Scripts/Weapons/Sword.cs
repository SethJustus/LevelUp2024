using System.Collections;
using Mono.Cecil.Cil;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    #region Fields
    private bool _isAttacking;
    private Collider2D _hitBox;
    #endregion
    
    #region Properties
    public bool IsEquipped { get; set; }
    #endregion
    
    #region Parameters

    [SerializeField] private Player Player;
    [SerializeField] private int SwordDamage = 25;
    
    [SerializeField] private float SwordHitboxUptimeSecs = 0.5f;
    #endregion
    
    #region Unity Methods

    void Start()
    {
        this._hitBox = this.GetComponent<Collider2D>();
        this._hitBox.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        
        other.GetComponent<HealthObject>()?.TakeDamage(SwordDamage);
    }

    #endregion
    
    #region Methods
    public void Attack()
    {
        if (this._isAttacking)
        {
            return;
        }

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        this._isAttacking = true;
        this._hitBox.enabled = true;
        Player.attacking = true;
        Player.animator.SetBool("Attack", true);
        Player.animator.SetFloat("vspeed", 0);
        Player.animator.SetFloat("hspeed", 0);
        yield return new WaitForSeconds(SwordHitboxUptimeSecs);
        Player.attacking = false;
         Player.animator.SetBool("Attack", false);
        this._hitBox.enabled = false;
        this._isAttacking = false;
    }

    #endregion


}
