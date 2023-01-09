using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make the controller a state machine and pass itself as a reference 
public class BearmanCtrl : StateMachine<BearmanCtrl>, IDamageable, IAttack
{
    public CharacterEvents.EventsHandler EventsHandler;
    [HideInInspector] public BearmanAnimationHandler AnimationHandler;

    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 40;
    private int health;

    [Header("Jump state")]
    public float fallGravity = 2f; // When falling apply bigger gravity for faster fall

    [Header("Punch charge state")]
    [HideInInspector] public float chargeTime;

    [Header("Punch state")]
    public int damage = 10;

    [Header("Punch and heavy punch states")]
    public float punchRadius = .5f;
    public Transform punchLocation;

    [Header("Aim & throw raccoon state")]
    public float throwForce = 5f;
    public float raccoonMass = 2f;
    public Transform launchPosition;

    [Header("Ground check")]
    private GroundCheck _groundCheck;
    [HideInInspector] public bool IsGrounded;

    protected override void Awake()
    {
        base.Awake();
        EventsHandler = new CharacterEvents.EventsHandler();
        AnimationHandler = GetComponent<BearmanAnimationHandler>();
        _groundCheck = transform.Find("GroundCheck").GetComponent<GroundCheck>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    [HideInInspector] public bool TakeDamage(int damagePt)
    {
        SetState(typeof(DamageState));

        health -= damagePt;
        if (health <= 0) 
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

    public void ThrowRaccoon() => EventsHandler.InvokeThrowRaccoon();

    override protected void Update()
    {
        base.Update();
        IsGrounded = _groundCheck.Check();
    }
}