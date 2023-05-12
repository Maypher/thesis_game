using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States
{
    public class Assault : JilibiliState, IAttack
    {
        private readonly Data.D_Attack stateData;

        public Assault(Jilibili entity, StateMachine<Jilibili> stateMachine, Data.D_Attack stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }


        public override void Enter()
        {
            base.Enter();

            // Play animation

            Attack();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            stateMachine.ChangeState(jilibili.IdleState);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void Attack()
        {
            Collider2D[] enemies = jilibili.AttackCheck.GetEnemies();

            AttackDetails attackDetails = stateData.attackDetails;

            foreach (Collider2D enemy in enemies)
            {
                attackDetails.attackPostion = jilibili.AttackCheck.transform.position;
                enemy.GetComponent<IDamageable>()?.TakeDamage(attackDetails);
            }
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}