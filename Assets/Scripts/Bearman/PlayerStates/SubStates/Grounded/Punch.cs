using Player.Raccoon;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Substates.Grounded
{
    public class Punch : PlayerState, IAttack
    {
        private readonly Data.D_Punch stateData;

        private bool finishedAnimation;

        public Punch(Player entity, StateMachine<Player> stateMachine, Data.D_Punch stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            player.SetAnimationParameter("punch");

            player.AnimationEvent += Attack;
            player.FinishAnimation += FinishAttack;

            finishedAnimation = false;

            player.SetVelocityX(0);
        }

        public override void Exit()
        {
            base.Exit();

            player.AnimationEvent += Attack;
            player.FinishAnimation += FinishAttack;
        }

        public override void Input()
        {
            base.Input();
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

            if (finishedAnimation) stateMachine.ChangeState(player.IdleState);
        }

        public void Attack()
        {
            Collider2D[] enemies = player.AttackCheck.GetEnemies();

            AttackDetails attackDetails = stateData.attackDetails;

            foreach (Collider2D enemy in enemies)
            {
                attackDetails.attackPostion = player.AttackCheck.transform.position;
                enemy.GetComponent<IDamageable>()?.TakeDamage(attackDetails);
            }
        }

        public void FinishAttack() => finishedAnimation = true;
    }
}