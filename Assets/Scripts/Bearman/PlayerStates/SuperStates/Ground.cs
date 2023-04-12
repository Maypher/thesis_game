using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Superstates
{
    public abstract class Ground : PlayerState
    {

        public Ground(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (player.UserInput.Player.Jump.WasPressedThisFrame() && player.CanJump) stateMachine.ChangeState(player.JumpState);
            else if (!player.GroundCheck.Check()) stateMachine.ChangeState(player.AirMoveState);
        }

        private void Jump() => stateMachine.ChangeState(player.JumpState);
    }
}