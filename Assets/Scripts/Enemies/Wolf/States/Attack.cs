using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{

    public class Attack : WolfState, IAttack
    {
        private readonly Data.D_Attack stateData;

        public Attack(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Attack stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            // Play animation
            (this as IAttack).Attack();
        }

        public void FinishAttack()
        {
            
        }

        void IAttack.Attack()
        {
            Collider2D[] enemies = wolf.AttackCheck.GetEnemies();

            stateData.details.attackPostion = wolf.AttackCheck.transform.position;

            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<IDamageable>()?.TakeDamage(stateData.details);
            }
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            stateMachine.ChangeState(wolf.TargetDetectedState);
        }
    }
}