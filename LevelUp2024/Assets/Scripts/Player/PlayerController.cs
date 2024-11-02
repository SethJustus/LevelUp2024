using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    #region Fields
    private Rigidbody2D _rigidbody;
    #endregion
    
    #region Parameters

    [SerializeField] private Statistic SpeedStatistic;
    #endregion
    
    #region Unity Methods
    public void Start()
    {
        this._rigidbody = GetComponent<Rigidbody2D>();
    }
    #endregion
    
    
    #region Public Methods
    public void Move(Vector2 movementVector)
    {
        if (movementVector != Vector2.zero)
        {
            this.SpeedStatistic.TryGainExperience(1);
        }

        var speedMultiplier = (1 + (SpeedStatistic.Experience / 100f)) * 100;
        
        // Use Time.deltaTime because physics logic is being called from Update
        this._rigidbody.linearVelocity = movementVector * speedMultiplier * Time.deltaTime;
    }
    #endregion 
}
