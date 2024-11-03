using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(IPlayerController))]
public class Player : HealthObject
{
    #region Fields
    private IPlayerController _controller;
    private Vector2 _movementVector;
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
    }
    
    void Update()
    {
        _movementVector = MoveAction.action.ReadValue<Vector2>();
        if (DashAction.action.triggered)
        {
            this._dashnextupdate = true;
        }
    }

    void FixedUpdate()
    {
        _controller.HorizontalMove(_movementVector);
        if (this._dashnextupdate)
        {
            _controller.Dash(_movementVector);
            this._dashnextupdate = false;
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
