using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State<T> where T: Entity
    {
        // To keep track of which state machine this state belongs to
        protected StateMachine<T> stateMachine;

        // To keep track of which entity this state belongs to
        protected T entity;

        // The time at which this state was activated
        protected float startTime;


        public State(T entity, StateMachine<T> stateMachine)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            startTime = Time.time;
        }

        public virtual void Exit()
        {
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void CheckStateChange()
        {
        }
    }
}