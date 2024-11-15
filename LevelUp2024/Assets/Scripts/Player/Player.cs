using System;
using System.Collections;
using UnityEditor;
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
    [SerializeField] private LayerMask Npc_Layer;
    #endregion
    
    #region Unity Methods
    public void Start()
    {
        this._controller = GetComponent<IPlayerController>();
        this._weaponManager = GetComponent<WeaponManager>();
    }
    
    void Update()
    {
        Collider2D[] NearbyeNpcs = Physics2D.OverlapCircleAll(this.transform.position, 3f, Npc_Layer);
        if(NearbyeNpcs.Length >= 1){
            // init setup for algo
            Collider2D NearestNpc = NearbyeNpcs[0];
            double distance = Math.Sqrt(Math.Pow(this.transform.position[0] - NearestNpc.transform.position[0], 2) + Math.Pow(this.transform.position[1] - NearestNpc.transform.position[1], 2));
            for(int i = 1; i < NearbyeNpcs.Length; i++){
                // euclidian distance checks for the nearest npc
                if(distance < Math.Pow(this.transform.position[0] - NearbyeNpcs[i].transform.position[0], 2) + Math.Pow(this.transform.position[1] - NearbyeNpcs[i].transform.position[1], 2))
                {
                    NearestNpc = NearbyeNpcs[i];
                    distance = Math.Pow(this.transform.position[0] - NearbyeNpcs[i].transform.position[0], 2) + Math.Pow(this.transform.position[1] - NearbyeNpcs[i].transform.position[1], 2);
                }
            }
            DialogueManager.GetInstance().EnterDialogue(NearestNpc.GetComponent<Npc_behaviour>().InkJSON);
        }
        _movementVector = MoveAction.action.ReadValue<Vector2>();
        //print(_movementVector);
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
