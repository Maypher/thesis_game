using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Gunner
{
    public abstract class GunnerState : StateMachine.State<Gunner>
    {
        protected Gunner gunner;

        public GunnerState(Gunner entity, StateMachine<Gunner> stateMachine) : base(entity, stateMachine)
        {
            gunner = entity;
        }
    }
}