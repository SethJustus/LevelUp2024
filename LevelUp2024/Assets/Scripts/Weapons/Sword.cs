using System.Collections;
using Mono.Cecil.Cil;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    #region Fields
    private bool _isAttacking;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    #endregion
    
    #region Properties
    public bool IsEquipped { get; set; }
    #endregion
    
    #region Parameters
    [SerializeField] GameObject Player;

    [SerializeField] private float SwingSpeed = 500;

    [SerializeField] private int SwordDamage = 25;
    #endregion
    
    #region Unity Methods

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        this.gameObject.SetActive(false);
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
        Debug.Log("Swinging the sword");
        if (this._isAttacking)
        {
            return;
        }

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        this._isAttacking = true;
        this.gameObject.SetActive(true);
        
        while (Mathf.Abs(transform.eulerAngles.z - 360) > 15f)
        {
            Debug.Log(Mathf.Abs(transform.eulerAngles.z - 360));
            transform.RotateAround(Player.transform.position, new Vector3(0,0,1), SwingSpeed * Time.deltaTime);
            yield return null;
        }
        
        // Reset the position and rotation
        this.transform.localRotation = _startRotation;
        this.transform.localPosition = _startPosition;
        
        this._isAttacking = false;
        this.gameObject.SetActive(false);
    }

    #endregion


}
