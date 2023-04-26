using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf
{
    public abstract class WolfState : State<Wolf>
    {
        protected Wolf wolf;
        public WolfState(Wolf entity, StateMachine<Wolf> stateMachine) : base(entity, stateMachine)
        {
            this.wolf = entity;
        }
    }
}
