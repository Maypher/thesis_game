using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Entity
{
    [Header ("Data files")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;


    public Wolf_IdleState IdleState { get; private set; }
    public Wolf_MoveState MoveState { get; private set; }

    public Wolf_PlayerDetectedState PlayerDetectedState { get; private set; }

    public Wolf_ChargeState ChargeState { get; private set; }

    public Wolf_MeleeAttackState MeleeAttackState { get; private set; }

    public Wolf_StunState StunState { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        MoveState = new Wolf_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new Wolf_IdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new Wolf_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        ChargeState = new Wolf_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        MeleeAttackState = new Wolf_MeleeAttackState(this, stateMachine, "attack", meleeAttackPosition, meleeAttackStateData, this);
        StunState = new Wolf_StunState(this, stateMachine, "stun", stunStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(AttackDetails attackDetails)
    {
        base.TakeDamage(attackDetails);

        if (isStunned && stateMachine.CurrentState != StunState) stateMachine.ChangeState(StunState);
    }
}