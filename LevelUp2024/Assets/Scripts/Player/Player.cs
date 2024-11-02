using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Parameters
    [SerializeField]  InputActionReference MoveAction;
    #endregion
    
    #region Unity Methods
    void Update()
    {
        var movementVector = MoveAction.action.ReadValue<Vector2>();
        Debug.Log(movementVector);
    }
    #endregion
}
