using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Superstates
{
    public abstract class Ground : PlayerState
    {
        protected Player player;

        public Ground(Player entity, StateMachine<Player> stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
        {
            this.player = entity;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            // TODO: All transitions to airborne states
        }
    }
}