using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States
{
    public class Launch : RaccoonState, IAttack
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

            raccoon.AttackCheck.enemyEnteredAttackArea += Attack;

            ogGrav = raccoon.Rb.gravityScale;
            raccoon.Rb.gravityScale = 0;

            raccoon.Rb.AddForce(stateData.launchForce * raccoon.transform.right, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            base.Exit();

            raccoon.AttackCheck.enemyEnteredAttackArea -= Attack;
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

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}
