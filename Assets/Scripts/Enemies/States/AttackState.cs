using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAttackFinished;
    protected bool enemyInFOV;
    protected bool enemyInAttackRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        enemyInFOV = entity.FOV.Check();
        enemyInAttackRange = entity.attackCheck.Check();
    }

    public override void Enter()
    {
        base.Enter();

        entity.currentAttack = this;
        isAttackFinished = false;
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

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() => isAttackFinished = true;
}
