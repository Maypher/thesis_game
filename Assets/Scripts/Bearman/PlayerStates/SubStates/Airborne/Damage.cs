using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne
{
    public class Damage : Superstates.Airborne
    {
        public Damage(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.CanBeDamaged = false;
            player.SetAnimationParameter("damage");
        }

        public override void Exit()
        {
            base.Exit();

            player.CanBeDamaged = true;
        }
    }
}