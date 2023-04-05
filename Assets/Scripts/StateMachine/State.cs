using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State<T> where T: Entity
    {
        // To keep track of which state machine this state belongs to
        protected StateMachine<T> stateMachine;

        protected StateData stateData;

        // To keep track of which entity this state belongs to
        protected T entity;

        // The time at which this state was activated
        protected float startTime;

        // Used to set animation states directly from the states
        protected string animBoolName;

        public State(T entity, StateMachine<T> stateMachine, string animBoolName, StateData stateData)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            this.stateData = stateData;
        }

        public virtual void Enter()
        {
            startTime = Time.time;
            // entity.Anim.SetBool(animBoolName, true);
        }

        public virtual void Exit()
        {
            // entity.Anim.SetBool(animBoolName, false);
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
        }
    }
}