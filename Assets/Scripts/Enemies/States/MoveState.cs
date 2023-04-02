using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic move state that can be inherited and customized by any other state
public abstract class MoveState : State
{
    protected D_MoveState stateData;

    protected  bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool canSeeTarget;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(stateData.movementSpeed);
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

    public override void DoChecks()
    {
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        canSeeTarget = entity.FOV.Check();
    }
}
