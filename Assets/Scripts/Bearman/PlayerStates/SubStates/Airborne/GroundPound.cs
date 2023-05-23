using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne
{
    public class GroundPound : Superstates.Airborne, IAttack
    {
        private readonly Data.D_GroundPound stateData;

        private float ogGravity;

        private bool landed;

        public GroundPound(Player entity, StateMachine<Player> stateMachine, Data.D_GroundPound stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();
        }

        public override void Enter()
        {
            base.Enter();

            player.SetAnimationParameter("groundPound");

            player.AnimationEvent += AirHang;
            player.FinishAnimation += EnableLanding;

            player.SetVelocityX(0);
            player.Rb.AddForce(new Vector2(stateData.jumpForce.x * player.FacingDirection, stateData.jumpForce.y), ForceMode2D.Impulse);

            ogGravity = player.Rb.gravityScale;
            player.Rb.gravityScale = 0;
            player.CanLand = false;
            landed = false;

        }

        public override void Exit()
        {
            base.Exit();

            player.FinishAnimation -= EnableLanding;
            player.GroundPoundCheck.enemyEnteredAttackArea -= Attack;
            player.Rb.gravityScale = ogGravity;

            player.CanBeDamaged = true;
        }

        public override void Input()
        {
            base.Input();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.GroundCheck.Check() && !landed) 
            {
                player.SetAnimationParameter("groundPound");
                player.GroundPoundParticles.Play();
                player.AudioSource.PlayOneShot(stateData.groundpoundLandSFX);
                landed = true;
                CameraShakeManager.instance.CameraShakeFromProfile(stateData.screenShakeProfile, player.CameraImpulseSource);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void AirHang()
        {
            player.SetVelocity(0, Vector2.zero, 0);

            player.AnimationEvent -= AirHang;
            player.AnimationEvent += FallDown;
        }

        private void FallDown()
        {
            player.SetVelocity(stateData.fallForce, Vector2.down, player.FacingDirection);
            player.Rb.gravityScale = ogGravity;

            player.AnimationEvent -= FallDown;
            player.CanBeDamaged = false;
            player.GroundPoundCheck.enemyEnteredAttackArea += Attack;
        }

        private void EnableLanding() => player.CanLand = true;

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

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}