using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    #region Fields
    private Rigidbody2D _rigidbody;
    private bool _canDash = true;
    #endregion
    
    #region Parameters

    [SerializeField] private Statistic SpeedStatistic;

    [SerializeField] private Statistic AgilityStatistic;
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
    
    public void Dash(Vector2 movementVector)
    {
        if (!this._canDash)
        {
            return;
        }

        Debug.Log("dashing");
        this._rigidbody.AddForce(movementVector * this.AgilityStatistic.Experience, ForceMode2D.Impulse);
        this.StartCoroutine(this.DashCooldown());
    }
    
    private IEnumerator DashCooldown()
    {
        this._canDash = false;
        // TODO: use a calculated field here instead of 1
        yield return new WaitForSeconds(1);
        this._canDash = true;
    }

    #endregion 
}
