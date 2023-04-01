using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_PlayerDetectedState : PlayerDetectedState
{
    private Wolf wolf;

    private bool chargeAtPlayer;

    public Wolf_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();

        chargeAtPlayer = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        chargeAtPlayer = Time.time >= startTime + stateData.actionTime;

        if (chargeAtPlayer) stateMachine.ChangeState(wolf.ChargeState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
