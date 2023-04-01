using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_AttackState : AttackState
{
    private Wolf wolf;

    public Wolf_AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_AttackState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
