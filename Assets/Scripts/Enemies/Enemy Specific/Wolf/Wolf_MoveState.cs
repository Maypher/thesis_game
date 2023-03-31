using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_MoveState : MoveState
{
    private Wolf wolf;

    private bool canSeeTarget;

    public Wolf_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();

        canSeeTarget = entity.FOV.Check();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
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

        canSeeTarget = entity.FOV.Check();
    }
}
