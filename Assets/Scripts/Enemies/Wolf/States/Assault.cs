using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Assault : WolfState
    {
        private readonly Data.D_Assault stateData;

        public Assault(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Assault stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();


            wolf.SetVelocity(stateData.jumpForce.x, stateData.jumpForce, wolf.FacingDirection);
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
            base.PhysicsUpdate();
        }
      
        public override void CheckStateChange()
        {
            base.CheckStateChange();
        }
    }
}
