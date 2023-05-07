using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne {
    public class AirMove : Superstates.Airborne
    {
        private readonly Data.D_AirMove stateData;

        private float inputDirection;
        private bool airAttack;

        private bool wantToDash;
        public bool alreadyDashed;

        // Used for being able to jump out of a dash
        public bool canDoubleJump;
        private bool alreadyDoubleJumped;
        private bool wantsToJumpMidAir;

        public bool canCoyoteJump;

        public AirMove(Player entity, StateMachine<Player> stateMachine, Data.D_AirMove stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            airAttack = false;
        }

        public override void Exit()
        {
            base.Exit();

            alreadyDoubleJumped = false;
            canDoubleJump = false;
            canCoyoteJump = false;
        }

        public override void Input()
        {
            base.Input();

            wantToDash = GameManager.UserInput.Player.Dash.WasPerformedThisFrame();
            inputDirection = GameManager.UserInput.Player.Move.ReadValue<float>();
            wantsToJumpMidAir = GameManager.UserInput.Player.Jump.WasPressedThisFrame();

            airAttack = GameManager.UserInput.Player.ShockwaveAttack.triggered;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            if (GameManager.UserInput.Player.Jump.WasReleasedThisFrame() || player.Rb.velocity.y < 0) player.Rb.gravityScale = stateData.fallingGravity;

            if (inputDirection != 0)
            {
                float inputSign = Mathf.Sign(inputDirection);

                if (Mathf.Sign(player.FacingDirection) != inputSign) player.Flip();

                player.SetVelocityX(stateData.maxSpeed);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (wantToDash && !alreadyDashed)
            {
                alreadyDashed = true;
                stateMachine.ChangeState(player.DashState);
            }
            else if (wantsToJumpMidAir && canDoubleJump && !alreadyDoubleJumped && Time.time <= startTime + stateData.doubleJumpGap)
            {
                alreadyDoubleJumped = true;
                canDoubleJump = false;
                stateMachine.ChangeState(player.JumpState);
            }
            else if (wantsToJumpMidAir && canCoyoteJump && Time.time <= startTime + stateData.coyoteTime) stateMachine.ChangeState(player.JumpState);
            else if (airAttack) stateMachine.ChangeState(player.GroundpoundState);
        }
    }
}
