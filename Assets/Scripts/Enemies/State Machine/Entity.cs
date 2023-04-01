using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all enemy/NPC characters
public abstract class Entity : MonoBehaviour
{
    public int FacingDirection { get; private set; } = 1;

    // Instead of using new Vector2() every time it's needed we just set it here
    private Vector2 velocityWorkspace;

    // Let every entity have its own state machine with its own states
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject GoAttachedTo { get; private set; }

    public FieldOfView FOV { get; private set; }

    public AttackCheck attackCheck { get; private set; }


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
    }

    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    // Set entity velocity based on facing direction and given velocity
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawLine(wallCheck.position, wallCheck.position + entityData.wallCheckDistance * FacingDirection * Vector3.right);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + entityData.ledgeCheckDistance * Vector3.down);
    }
}
