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
    [SerializeField] private D_AttackState attackStateData;

    public Wolf_IdleState IdleState { get; private set; }
    public Wolf_MoveState MoveState { get; private set; }

    public Wolf_PlayerDetectedState PlayerDetectedState { get; private set; }

    public Wolf_ChargeState ChargeState { get; private set; }

    public Wolf_AttackState AttackState { get; private set; }


    public override void Start()
    {
        base.Start();

        MoveState = new Wolf_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new Wolf_IdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new Wolf_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        ChargeState = new Wolf_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        AttackState = new Wolf_AttackState(this, stateMachine, "attack", attackStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public override void Update()
    {
        base.Update();
    }
}