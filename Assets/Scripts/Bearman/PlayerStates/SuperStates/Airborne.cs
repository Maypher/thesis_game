using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Superstates
{
    public abstract class Airborne : PlayerState
    {
        private bool wantsToJump;
        private float jumpBufferTimer;

        public Airborne(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }


        public override void Enter()
        {
            base.Enter();

            wantsToJump = false;
            jumpBufferTimer = player.jumpBufferTime;
            player.SetAnimationParameter("isAirborne", true);
        }

        public override void Exit()
        {
            base.Exit();

            player.AirMoveState.alreadyDashed = false;
            player.TimeInAir = 0;

            player.SetAnimationParameter("isAirborne", false);
        }

        public override void Input()
        {
            base.Input();

            if (!wantsToJump) wantsToJump = GameManager.UserInput.Player.Jump.WasPerformedThisFrame();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.TimeInAir += Time.deltaTime;

            if (wantsToJump) jumpBufferTimer -= Time.deltaTime;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            // Since the frame the player is in the air this check is triggered then it would transition immediately back to grounded
            // A small time is given so the player can be airborne before checking
            if (player.GroundCheck.Check() && player.CanLand && player.TimeInAir > 0.2) 
            {

                if (wantsToJump && jumpBufferTimer > 0) stateMachine.ChangeState(player.JumpState);
                else if (GameManager.UserInput.Player.Move.ReadValue<float>() == 0) stateMachine.ChangeState(player.IdleState);
                else stateMachine.ChangeState(player.WalkState);
            }
        }
    }   
}
