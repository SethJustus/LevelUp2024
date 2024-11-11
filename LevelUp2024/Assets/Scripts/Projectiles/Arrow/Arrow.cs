using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    #region Fields
    private Vector2 _direction;
    private HealthObject _parent;
    private Rigidbody2D _rigidbody;
    #endregion
    
    #region Parameters
    [SerializeField] private float Speed;
    [SerializeField] private int Damage;
    [SerializeField] private float DespawnTimeSeconds = 2;
    #endregion
    
    #region Unity Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DealDamage(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        DealDamage(trigger);
    }

    #endregion
    
    #region Methods
    public void Initialize(Vector2 direction, HealthObject parent)
    {
        this._direction = direction;
        //this._direction = transform.InverseTransformDirection(direction);
        this._parent = parent;
        this._rigidbody = this.GetComponent<Rigidbody2D>();
        this._rigidbody.AddRelativeForce(_direction * Speed, ForceMode2D.Impulse);
        this.StartCoroutine(DespawnCoroutine());
    }

    void DealDamage(Collider2D collider)
    {
        var healthObject = collider.GetComponent<HealthObject>();
        
        if (healthObject == null || healthObject == this._parent)
        {
            return;
        }

        healthObject.TakeDamage(this.Damage);
    }
    
    void Despawn()
    {
        // TODO: spawn particles
        Destroy(this.gameObject);
    }

    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(this.DespawnTimeSeconds);
        this.Despawn();
    }
    

    #endregion
    
    
}
