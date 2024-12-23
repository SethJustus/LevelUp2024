using System;
using System.Collections;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
[RequireComponent(typeof(IPlayerController))]
[RequireComponent(typeof(WeaponManager))]
public class Player : HealthObject
{
    #region Fields
    private IPlayerController _controller;
    private WeaponManager _weaponManager;
    private Vector2 _movementVector; 
    private bool _dashNextUpdate;
    private FocusObject _focusObject;
    private Collider2D LastNearestNpc;
    public bool InDialogue;
    public Animator animator;
    public bool attacking = false;
    #endregion
    
    #region Properties
    public int Focus { get; private set; } = 0;
    #endregion
    
    #region Parameters
    [Header("Input Settings")]
    [SerializeField] private InputActionReference MoveAction;
    [SerializeField] private InputActionReference DashAction;
    [SerializeField] private InputActionReference AttackAction;
    [SerializeField] private InputActionReference InteractAction;
    [SerializeField] private InputActionReference PreviousAction;
    [SerializeField] private InputActionReference NextAction;
    [Header("NPC Settings")]
    [SerializeField] private LayerMask Npc_Layer;
    #endregion

    #region Parameters
    [Header("Sound Clips")]
    [SerializeField] private AudioClip PlayerAttackAudio;
    #endregion

    private AudioSource audioSource;

    


    #region Unity Methods
    public void Start()
    {
        this._controller = GetComponent<IPlayerController>();
        this._weaponManager = GetComponent<WeaponManager>();
        this._focusObject = GetComponentInChildren<FocusObject>();
        this.InDialogue = false;
        this.audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if(InDialogue){
            return;
        }
        this.HandleInput();
        
        this.CheckForNPCs();
    }

    void FixedUpdate()
    {
        if(attacking){
            return;
        }
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
        {            return;
                 }
         
                 this._focusObject.BreakFocus();
         
                 base.TakeDamage(damage);
             }


    private void HandleInput()
    {
        if(attacking){
            
            audioSource.PlayOneShot(PlayerAttackAudio);
            return;
        }
        // animating input
        _movementVector = MoveAction.action.ReadValue<Vector2>();
        float hmov = _movementVector[0];
        float vmov = _movementVector[1];
        if(hmov != 0){
            Vector2 flip = this.transform.localScale;
            flip.x = Math.Abs(flip.x) * Math.Abs(hmov) / hmov;
            this.transform.localScale = flip;
            
            
        }
        // if(flip != new Vector2(this.transform.localScale.x, this.transform.localScale.y)){
        //     
        // }
        animator.SetFloat("vspeed", vmov);
        animator.SetFloat("hspeed", Math.Abs(hmov));

        

        if (DashAction.action.triggered)
        {
            _dashNextUpdate = true;
        }

        if (AttackAction.action.triggered)
        {
            // Run the attack method on the current weapon
            _weaponManager.EquippedWeapon?.Attack();
        }

        if (NextAction.action.triggered)
        {
            _weaponManager.EquipNext();
        }
        
        if (PreviousAction.action.triggered)
        {
            _weaponManager.EquipPrevious();
        }
    }

    void CheckForNPCs()
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
            NearestNpc.GetComponent<Npc_behaviour>().Indicator = true;
            LastNearestNpc = NearestNpc;
            if (InteractAction.action.triggered){
                InDialogue = true;
                LastNearestNpc.GetComponent<Npc_behaviour>().Indicator = false;
                DialogueManager Dmanager = DialogueManager.GetInstance();
                Dmanager.EnterDialogue(NearestNpc.GetComponent<Npc_behaviour>().InkJSON);
                Dmanager.Player = this;
            }
        } else if (LastNearestNpc){
            LastNearestNpc.GetComponent<Npc_behaviour>().Indicator = false;
            LastNearestNpc = null;
        }
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
