using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControllerStatus
{
    public bool HasIFrames { get; set; }
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    #region Fields
    private Rigidbody2D _rigidbody;
    private Vector2 _dashDirection;    
    private bool _isDashing;

    #endregion
    
    #region Properties
    public PlayerControllerStatus Status { get; private set; } = new();
    #endregion
    
    #region Parameters
    [Header("Movement Parameters")]
    [SerializeField] private float DashSpeed = 1500f;
    
    [SerializeField] private float DashDuration = 0.25f;
    
    [Header("Stat References")]
    [SerializeField] private Statistic SpeedStatistic;

    [SerializeField] private Statistic AgilityStatistic;

    #endregion
    
    #region Unity Methods
    public void Start()
    {
        this._rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (this._isDashing)
        {
            collision.GetComponent<HealthObject>()?.TakeDamage(12);
        }
    }
    #endregion
    
    #region Methods
    
    public void Move(Vector2 direction, bool dash)
    {
        this.Status.HasIFrames = this._isDashing;
        
        if (dash)
        {
            this.StartDash(direction);
        }

        if (this._isDashing)
        {
            this.Dash();
        }
        else
        {
            this.HorizontalMove(direction);
        }
    }
    
    public void HorizontalMove(Vector2 movementVector)
    {
        if (movementVector != Vector2.zero)
        {
            this.SpeedStatistic.TryGainExperience(1);
        }

        var speedMultiplier = (1 + (SpeedStatistic.Experience / 100f)) * 100;
        
        // Use Time.deltaTime because physics logic is being called from Update
        this._rigidbody.linearVelocity = movementVector * speedMultiplier * Time.deltaTime;
    }
    
    public void StartDash(Vector2 movementVector)
    {
        // Don't dash again if we are currently dashing
        if (this._isDashing)
        {
            return;
        }
        
        this._dashDirection = movementVector;
        this.StartCoroutine(this.StartDashCoroutine());
    }
    
    IEnumerator StartDashCoroutine()
    {
        this._isDashing = true;
        yield return new WaitForSeconds(this.DashDuration);
        this._isDashing = false;
    }
    
    void Dash()
    {
        this._rigidbody.linearVelocity = _dashDirection * DashSpeed * Time.deltaTime;
    }
    #endregion 
}
