using UnityEngine;

public class Wolf_IdleState : IdleState
{
    private Wolf wolf;
    
    public Wolf_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Wolf wolf) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (canSeeTarget) stateMachine.ChangeState(wolf.PlayerDetectedState);
        else if (isIdleTimeOver) stateMachine.ChangeState(wolf.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
