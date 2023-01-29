using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Make the controller a state machine and pass itself as a reference 
public class BearmanCtrl : StateMachine<BearmanCtrl>, IDamageable, IAttack
{
    public CharacterEvents.EventsHandler EventsHandler;
    [HideInInspector] public BearmanAnimationHandler AnimationHandler;
    
    [Header("Input")]
    [HideInInspector] public UserInput UserInput;

    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 40;
    private int _health;

    [Header("Airborne state")]
    [HideInInspector] public bool Jumped = false;

    [Header("Punch charge state")]
    [HideInInspector] public float ChargeTime;

    [Header("Ground check")]
    private GroundCheck _groundCheck;
    [HideInInspector] public bool IsGrounded
    {
        get { return _groundCheck.Check(); }
    }

    protected override void Awake()
    {
        base.Awake();
        EventsHandler = new CharacterEvents.EventsHandler();
        AnimationHandler = GetComponent<BearmanAnimationHandler>();
        _groundCheck = transform.Find("GroundCheck").GetComponent<GroundCheck>();

        UserInput = new UserInput();
        UserInput.Player.Enable();
    }

    private void Start() => _health = maxHealth;

    [HideInInspector] public bool TakeDamage(int damagePt)
    {
        SetState(typeof(DamageState));

        _health -= damagePt;
        if (_health <= 0) 
        {
            Kill();
            return true;
        }

        return false;
    }

    [HideInInspector] public void Kill()
    {
        Destroy(this.gameObject);
    }

    public void Attack() => EventsHandler.InvokeAttackEvent();

    public void FinishAttack() => EventsHandler.InvokeFinishAttackEvent();

    public void InstantiateShockwave() => EventsHandler.InvokeShockwave();
}