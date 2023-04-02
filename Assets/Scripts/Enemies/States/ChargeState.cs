using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool canSeeTarget;
    protected bool targetInAttackRange;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(stateData.chargeSpeed);
        isChargeTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isChargeTimeOver = Time.time >= startTime + stateData.chargeSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        canSeeTarget = entity.FOV.Check();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        targetInAttackRange = entity.attackCheck.Check();
    }
}
