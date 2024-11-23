using UnityEngine;

public class FocusObject : MonoBehaviour
{
    #region Properties
    public int Focus { get; private set; } = 0;
    #endregion
    
    #region Unity Methods
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    #endregion
    
    #region Public Methods
    public void BuildFocus(int focus)
    {
        this.Focus += focus;
    }

    public void BreakFocus()
    {
        this.Focus = 0;
    }

    #endregion
}
