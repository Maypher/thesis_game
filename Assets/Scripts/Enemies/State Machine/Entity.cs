using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Base class for all enemy/NPC characters
public abstract class Entity : MonoBehaviour
{
    public int FacingDirection { get; private set; } = 1;

    // Instead of using new Vector2() every time it's needed we just set it here
    private Vector2 velocityWorkspace;


    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    protected bool isStunned;

    public int lastDamageDirection { get; private set; }

    // Let every entity have its own state machine with its own states
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject GoAttachedTo { get; private set; }

    public FieldOfView FOV { get; private set; }

    public AttackCheck attackCheck { get; private set; }

    public GroundCheck groundCheck { get; private set; }

    // Used to trigger attacks from within animations. Must be set in each individual 
    public AttackState currentAttack;



    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    # endregion

    // Works as regular start. Runs when initializing object
    public virtual void Start()
    {
        GoAttachedTo = transform.gameObject;
        Rb = GoAttachedTo.GetComponent<Rigidbody2D>();
        Anim = GoAttachedTo.GetComponent<Animator>();
        FOV = GoAttachedTo.GetComponentInChildren<FieldOfView>();
        attackCheck = GoAttachedTo.transform.Find("AttackRange").GetComponent<AttackCheck>();

        stateMachine = new FiniteStateMachine();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime) 
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    // Set entity velocity based on facing direction and given velocity
    public virtual void SetVelocityX(float velocity)
    {
        velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
        Rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        velocityWorkspace.Set(Rb.velocity.x, velocity);
        Rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);

        Rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, GoAttachedTo.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        GoAttachedTo.transform.Rotate(0, 180, 0);
    }

    public void TriggerAttack()
    {

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawLine(wallCheck.position, wallCheck.position + entityData.wallCheckDistance * FacingDirection * Vector3.right);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + entityData.ledgeCheckDistance * Vector3.down);
    }

    public virtual void DamageHop(float velocity) => SetVelocityY(velocity);

    public virtual void ResetStunResistance() 
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void TakeDamage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;
        currentStunResistance -= attackDetails.stunDamageAmount;

        isStunned = currentStunResistance <= 0;

        currentHealth -= attackDetails.damage;

        // Used for knocking back in the right direction when getting hit
        lastDamageDirection = attackDetails.attackPostion.x > transform.position.x ? -1 : 1;

        DamageHop(entityData.damageHopSpeed);
    }

    public virtual void Kill()
    {
        throw new NotImplementedException();
    }
}
