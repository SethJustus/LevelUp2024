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
        collision.collider.GetComponent<HealthObject>()?.TakeDamage(this.Damage);
        
        // this.Despawn();
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
