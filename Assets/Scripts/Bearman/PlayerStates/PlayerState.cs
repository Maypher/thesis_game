using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class PlayerState : State<Player>
    {
        public PlayerState(Player entity, StateMachine<Player> stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
        {
        }

        /// <summary>
        /// Used to check Input later used in LogicUpdate() or PhysicsUpdate()
        /// </summary>
        public virtual void Input()
        {
        }
    }
}
