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
        private float airHangTimer;
        private bool fallingDown;

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
            
            player.Rb.AddForce(new Vector2(stateData.jumpForce.x * player.FacingDirection, stateData.jumpForce.y), ForceMode2D.Impulse);

            ogGravity = player.Rb.gravityScale;
            player.Rb.gravityScale = 0;
            
            airHangTimer = stateData.airHangTime;
            fallingDown = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Input()
        {
            base.Input();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // Let the player go up for a certain amount of time and then hang there before falling down
            if (Time.time >= startTime + stateData.jumpTime && !fallingDown) 
            { 
                player.SetVelocity(0, Vector2.zero, 0);
                airHangTimer -= Time.deltaTime;
            }
            
            if (airHangTimer <= 0)
            {
                fallingDown = true;
                player.SetVelocity(stateData.fallForce, Vector2.down, player.FacingDirection);
                player.Rb.gravityScale = ogGravity;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}