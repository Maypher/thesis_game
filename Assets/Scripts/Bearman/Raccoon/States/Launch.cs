using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States
{
    public class Launch : RaccoonState
    {
        private Data.D_Launch stateData;

        private float ogGrav;

        public Launch(Raccoon entity, StateMachine<Raccoon> stateMachine, Data.D_Launch stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            ogGrav = raccoon.Rb.gravityScale;
            raccoon.Rb.gravityScale = 0;

            raccoon.Rb.AddForce(raccoon.transform.right * stateData.launchForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            base.Exit();

            raccoon.Rb.gravityScale = ogGrav;
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
