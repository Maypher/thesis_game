using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_StunState : StunState
{
    private Wolf wolf;

    public Wolf_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isStunTimeOver)
        {
            if (entity.attackCheck.Check()) entity.stateMachine.ChangeState(wolf.MeleeAttackState);
            else if (entity.FOV.Check()) entity.stateMachine.ChangeState(wolf.ChargeState);
           // else entity.stateMachine.ChangeState(wolf.LookForTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
