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
            player.SetAnimationParameter("takeDamage", true);
        }

        public override void Input()
        {
            base.Input();
        }

        public override void Exit()
        {
            base.Exit();

            player.SetAnimationParameter("takeDamage", false);
            player.CanBeDamaged = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();
        }
    }
}