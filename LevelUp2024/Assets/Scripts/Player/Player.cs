using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(IPlayerController))]
public class Player : MonoBehaviour
{
    #region Fields
    private IPlayerController _controller;
    #endregion
    
    #region Parameters
    [SerializeField] private InputActionReference MoveAction;

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
    }
    #endregion
}
