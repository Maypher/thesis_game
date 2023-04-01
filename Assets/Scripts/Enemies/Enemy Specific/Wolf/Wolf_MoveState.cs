using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_MoveState : MoveState
{
    private Wolf wolf;

    public Wolf_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (canSeeTarget) stateMachine.ChangeState(wolf.PlayerDetectedState);
        else if (isDetectingWall || !isDetectingLedge) 
        {
            wolf.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(wolf.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        canSeeTarget = entity.FOV.Check();
    }
}
