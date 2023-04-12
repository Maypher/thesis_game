using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne {
    public class AirMove : Superstates.Airborne
    {
        private readonly Data.D_AirMove stateData;

        private float inputDirection;
        private bool wantToDash;

        // Used for being able to jump out of a dash
        public bool canDoubleJump;
        private bool alreadyDoubleJumped;
        private bool wantsToDoubleJump;

        public AirMove(Player entity, StateMachine<Player> stateMachine, Data.D_AirMove stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            alreadyDoubleJumped = false;
            canDoubleJump = false;
        }

        public override void Input()
        {
            base.Input();

            wantToDash = player.UserInput.Player.Dash.WasPerformedThisFrame();
            inputDirection = player.UserInput.Player.Move.ReadValue<float>();
            wantsToDoubleJump = player.UserInput.Player.Jump.WasPressedThisFrame();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            if (player.UserInput.Player.Jump.WasReleasedThisFrame() || player.Rb.velocity.y < 0) player.Rb.gravityScale = stateData.fallingGravity;

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

            if (wantToDash) stateMachine.ChangeState(player.DashState);
            else if (wantsToDoubleJump && canDoubleJump && !alreadyDoubleJumped && Time.time <= startTime + stateData.doubleJumpGap)
            {
                alreadyDoubleJumped = true;
                canDoubleJump = false;
                stateMachine.ChangeState(player.JumpState);
            }
        }
    }
}
