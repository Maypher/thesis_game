using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne
{
    public class GroundPound : Superstates.Airborne
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
        }

        public override void Input()
        {
            base.Input();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.GroundCheck.Check() && !landed) { player.SetAnimationParameter("groundPound"); landed = true; }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void AirHang()
        {
            Debug.Log("AirHang");
            player.SetVelocity(0, Vector2.zero, 0);

            player.AnimationEvent -= AirHang;
            player.AnimationEvent += FallDown;
        }

        private void FallDown()
        {
            player.SetVelocity(stateData.fallForce, Vector2.down, player.FacingDirection);
            player.Rb.gravityScale = ogGravity;

            player.AnimationEvent -= FallDown;
            player.AnimationEvent += SpawnShockwave;
        }

        private void SpawnShockwave()
        {
            GameObject.Instantiate(stateData.shockwave, player.ShockwaveSpawnPos.position, Quaternion.identity);
            player.AnimationEvent -= SpawnShockwave;
        }

        private void EnableLanding() => player.CanLand = true;
    }
}