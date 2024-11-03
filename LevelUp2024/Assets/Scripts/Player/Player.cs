using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(IPlayerController))]
public class Player : HealthObject
{
    #region Fields
    private IPlayerController _controller;
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
        var movementVector = MoveAction.action.ReadValue<Vector2>();
        _controller.Move(movementVector);
        var dashInput = DashAction.action.ReadValue<bool>();
        if (dashInput)
        {
            _controller.Dash(movementVector);
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
