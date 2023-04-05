using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Move : Enemies.States.Generics.Move<Wolf>
    {
        public Move(Wolf entity, StateMachine<Wolf> stateMachine, string animBoolName, StateData stateData) : base(entity, stateMachine, animBoolName, stateData)
        {
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}