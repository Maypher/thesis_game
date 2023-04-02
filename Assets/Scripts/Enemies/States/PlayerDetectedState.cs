using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool seeingTarget, targetWithinAttackRange;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        seeingTarget = entity.FOV.Check();
    }

    public override void DoChecks()
    {
        seeingTarget = entity.FOV.Check();
        targetWithinAttackRange = entity.attackCheck.Check();

        base.DoChecks();
    }
}
