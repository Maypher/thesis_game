using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class PlayerState : State<Player>
    {
        public PlayerState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
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
