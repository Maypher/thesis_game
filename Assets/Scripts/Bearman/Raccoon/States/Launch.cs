using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States
{
    public class Launch : RaccoonState, IAttack
    {
        private readonly Data.D_Launch stateData;

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

            raccoon.SetVelocity(stateData.launchForce, raccoon.transform.right, 1);
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
            Collider2D[] enemies = raccoon.AttackCheck.GetEnemies();

            AttackDetails attackDetails = stateData.attackDetails;

            foreach (Collider2D enemy in enemies)
            {
                Debug.Log(enemy);

                attackDetails.attackPostion = raccoon.AttackCheck.transform.position;
                enemy.GetComponent<IDamageable>()?.TakeDamage(attackDetails);
            }
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}
