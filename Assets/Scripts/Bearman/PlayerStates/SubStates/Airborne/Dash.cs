using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne
{
    public class Dash : Superstates.Airborne, IAttack
    {
        private readonly Data.D_Dash stateData;

        private bool dashing;
        private float lastActive;

        public Dash(Player entity, StateMachine<Player> stateMachine, Data.D_Dash stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }


        public override void Enter()
        {
            base.Enter();

            player.CanLand = false;
            player.CanBeDamaged = false;

            player.DashAttackCheck.enemyEnteredAttackArea += Attack;

            if (Time.time <= lastActive + stateData.cooldownTime) dashing = false;
            else 
            {
                lastActive = Time.time;
                player.SetAnimationParameter("dashing", true);
                player.AudioSource.PlayOneShot(stateData.dashSFX, .2f);
                player.StartCoroutine(DoDash());
            }
        }

        public override void Exit()
        {
            base.Exit();

            player.CanLand = true;
            player.CanBeDamaged = true;
            
            player.AirMoveState.canDoubleJump = true;
            player.SetAnimationParameter("dashing", false);

            player.DashAttackCheck.enemyEnteredAttackArea -= Attack;
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

            if (!dashing) stateMachine.ChangeState(player.AirMoveState);
        }

        private IEnumerator DoDash()
        {
            dashing = true;

            float ogGrav = player.Rb.gravityScale;
            player.Rb.gravityScale = 0;

            player.SetVelocity(stateData.dashForce, Vector2.right, player.FacingDirection);

            yield return new WaitForSeconds(stateData.dashTime);
           
            player.Rb.gravityScale = ogGrav;
            dashing = false;
        }

        public void Attack()
        {
            Collider2D[] enemies = player.DashAttackCheck.GetEnemies();

            AttackDetails attackDetails = stateData.attackDetails;

            foreach (Collider2D enemy in enemies)
            {
                attackDetails.attackPostion = player.DashAttackCheck.transform.position;
                enemy.GetComponent<IDamageable>()?.TakeDamage(attackDetails);
            }
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}