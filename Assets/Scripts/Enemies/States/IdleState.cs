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

        canSeeTarget = entity.FOV.Check();

        isIdleTimeOver = false;
        idleTime = SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle) entity.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime) isIdleTimeOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        canSeeTarget = entity.FOV.Check();
    }

    public void SetFlipAfterIdle(bool flip) => flipAfterIdle = flip;

    public float SetRandomIdleTime() => idleTime = Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);
}
