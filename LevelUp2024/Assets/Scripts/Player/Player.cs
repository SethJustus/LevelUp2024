using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(IPlayerController))]
[RequireComponent(typeof(WeaponManager))]
public class Player : HealthObject
{
    #region Fields
    private IPlayerController _controller;
    private WeaponManager _weaponManager;
    private Vector2 _movementVector; 
    private bool _dashNextUpdate;
    #endregion
    
    #region Parameters
    [Header("Input Settings")]
    [SerializeField] private InputActionReference MoveAction;
    [SerializeField] private InputActionReference DashAction;
    [SerializeField] private InputActionReference AttackAction;
    #endregion
    
    #region Unity Methods
    public void Start()
    {
        this._controller = GetComponent<IPlayerController>();
        this._weaponManager = GetComponent<WeaponManager>();
    }
    
    void Update()
    {
        _movementVector = MoveAction.action.ReadValue<Vector2>();
        if (DashAction.action.triggered)
        {
            _dashNextUpdate = true;
        }

        if (AttackAction.action.triggered)
        {
            Debug.Log("attack action triggered");
            // Run the attack method on the current weapon
            _weaponManager.EquippedWeapon?.Attack();
        }
    }

    void FixedUpdate()
    {
        _controller.Move(this._movementVector, _dashNextUpdate);
        if (_dashNextUpdate)
        {
            _dashNextUpdate = false;
        }
    }

    #endregion
    
    #region Methods
    // overriding the Die() method from HealthObject base class
    protected override void Die()
    {
        // TODO: Custom death logic goes here
        
        GameManager.Instance.OnPlayerDeath();
    }

    public override void TakeDamage(int damage)
    {
        if (_controller.Status.HasIFrames)
        {
            return;
        }

        base.TakeDamage(damage);
    }
    #endregion
    
    #region Test Methods
    IEnumerator TakeDamageTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            this.TakeDamage(1);
        }
    }
    #endregion
}
