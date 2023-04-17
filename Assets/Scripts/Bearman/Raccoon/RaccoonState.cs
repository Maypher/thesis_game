using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon
{
    public class RaccoonState : StateMachine.State<Raccoon>
    {
        protected Raccoon raccoon;
        public RaccoonState(Raccoon entity, StateMachine<Raccoon> stateMachine) : base(entity, stateMachine)
        {
            raccoon = entity;
        }
    }
}