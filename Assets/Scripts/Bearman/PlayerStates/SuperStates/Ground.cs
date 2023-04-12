using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Superstates
{
    public abstract class Ground : PlayerState
    {
        private bool wantsToJump;
        
        public Ground(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            wantsToJump = false;
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

            if (wantsToJump && player.CanJump)
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (!player.GroundCheck.Check())
            {
                player.AirMoveState.canCoyoteJump = true;
                stateMachine.ChangeState(player.AirMoveState);
            }
        }

        public override void Input()
        {
            base.Input();

            wantsToJump = player.UserInput.Player.Jump.WasPerformedThisFrame();
        }
    }
}