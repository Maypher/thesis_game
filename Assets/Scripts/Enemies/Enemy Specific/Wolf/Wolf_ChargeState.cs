using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_ChargeState : ChargeState
{
    private Wolf wolf;

    public Wolf_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wolf = wolf;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (canSeeTarget && targetInAttackRange) stateMachine.ChangeState(wolf.MeleeAttackState);
        else if (isChargeTimeOver)
        {
            if (!canSeeTarget) stateMachine.ChangeState(wolf.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
