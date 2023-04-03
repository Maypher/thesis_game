using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Generic idle state that can be inherited and customized by any other state
public abstract class IdleState : State
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;

    protected float idleTime;
    protected bool isIdleTimeOver;

    protected bool canSeeTarget;



    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle) entity.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isIdleTimeOver = Time.time >= startTime + idleTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        canSeeTarget = entity.FOV.Check();
    }

    public void SetFlipAfterIdle(bool flip) => flipAfterIdle = flip;

    public float SetRandomIdleTime() => idleTime = Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);
}