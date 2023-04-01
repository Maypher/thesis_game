using UnityEngine;

public class State
{
    // To keep track of which state machine this state belongs to
    protected FiniteStateMachine stateMachine;

    // To keep track of which entity it belongs to
    protected Entity entity;

    // The time at which this state was activated
    protected float startTime;

    // Used to set animation states directly from the states
    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    // Used for collision, input detection, etc.
    public virtual void DoChecks()
    {

    }
}
