using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne {
    public class AirMove : Superstates.Airborne
    {
        private readonly Data.D_AirMove stateData;

        private float inputDirection;

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
        }

        public override void Input()
        {
            base.Input();

            inputDirection = player.UserInput.Player.Move.ReadValue<float>();
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
        }
    }
}
