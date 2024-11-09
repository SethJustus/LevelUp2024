using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(IPlayerController))]
public class Player : HealthObject
{
    #region Fields
    private IPlayerController _controller;
    private Vector2 _movementVector;
    private Vector2 _dashVector;
    private bool _dashnextupdate;
    #endregion
    
    #region Parameters
    [SerializeField] private InputActionReference MoveAction;
    [SerializeField] private InputActionReference DashAction;
    #endregion
    
    #region Unity Methods
    public void Start()
    {
        this._controller = GetComponent<IPlayerController>();
        this.StartCoroutine(this.TakeDamageTick());
    }
    
    void Update()
    {
        _movementVector = MoveAction.action.ReadValue<Vector2>();
        if (DashAction.action.triggered)
        {
            _dashnextupdate = true;
        }
    }

    void FixedUpdate()
    {
        _controller.Move(this._movementVector, _dashnextupdate);
        if (_dashnextupdate)
        {
            _dashnextupdate = false;
        }
    }

    IEnumerator TakeDamageTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            this.TakeDamage(1);
        }
    }

    #endregion
    
    #region Methods
    // overriding the Die() method from HealthObject base class
    protected override void Die()
    {
        // TODO: Custom death logic goes here
    }
    #endregion
}
