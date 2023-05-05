using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Crocodile
{
    public abstract class CrocodileState : StateMachine.State<Crocodile>
    {
        protected Crocodile crocodile;

        public CrocodileState(Crocodile entity, StateMachine<Crocodile> stateMachine) : base(entity, stateMachine)
        {
            crocodile = entity;
        }
    }
}