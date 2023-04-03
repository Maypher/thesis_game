using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_MeleeAttackState : MeleeAttackState
{
    private Wolf wolf;

    public Wolf_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        if (isAttackFinished)
        {
            if (enemyInFOV) wolf.stateMachine.ChangeState(wolf.PlayerDetectedState);
            else if (enemyInAttackRange) { } //TODO: Create run away state
            else { } // TODO: Create look for player state
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

