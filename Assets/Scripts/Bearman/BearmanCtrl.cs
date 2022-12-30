using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Make the controller a state machine and pass itself as a reference 
public class BearmanCtrl : StateMachine<BearmanCtrl>, IDamageable, IAttack
{
    public CharacterEvents.EventsHandler EventsHandler;
    public BearmanAnimationHandler AnimationHandler;

    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 40;
    private int health;

    [Header("Walking state")]
    public float maxSpeed = 5f;
    public float runMultiplier = 2f; // When running multiply targetSpeed by this value
    public float acceleration = 5f;
    [Range(0, 1)] public float lerpAmount = 0f; // Used to determine the rate of change from current speed to max speed

    [Header("Idle state")]
    public float timeToFlex = 10f; // How much time has to pass before playing flex animation
    [HideInInspector] public float idleTime; // To time flex animation

    [Header("Jump state")]
    public float jumpForce = 10f;
    public float gravity = 1f; // When going up apply less gravity than when falling
    public float fallGravity = 2f; // When falling apply bigger gravity for faster fall

    [Header("Punch charge state")]
    public float chargeToHeavyAttack = 1;
    [HideInInspector] public float chargeTime;

    [Header("Punch state")]
    public int damage = 10;

    [Header("Heavy punch state")]
    public int smallDamage = 20;
    public int mediumDamage = 30;
    public int heavyDamage = 50;

    public float smallChargeTime;
    public float mediumChargeTime = 2;
    public float heavyChargeTime = 2.5f;

    [Header("Punch and heavy punch states")]
    public float punchRadius = .5f;
    public Transform punchLocation;

    [Header("Rock state")]
    public float speed;
    public float stepInterval;

    [Header("Aim & throw raccoon state")]
    public float throwForce = 5f;
    public float raccoonMass = 2f;
    public Transform launchPosition;

    protected override void Awake()
    {
        base.Awake();
        EventsHandler = new CharacterEvents.EventsHandler();
        AnimationHandler = GetComponent<BearmanAnimationHandler>();
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
}

// Movimiento normal de piedra/no girar (mas lento)
// Moverse en el aire
// 