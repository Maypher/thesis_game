using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class Idle : Superstates.Ground
    {
        private readonly Data.D_Idle stateData;

        private bool isMoving;
        private bool isCrouching;
        private bool aimRaccoon;
        private bool callBackRaccoon;
        private bool dash;
        private bool punch;
        public Idle(Player entity, StateMachine<Player> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            entity.SetVelocityX(0);

            player.CanJump = true;
            isMoving = false;
            isCrouching = false;
            callBackRaccoon = false;
            punch = false;
        }

        public override void Input()
        {
            base.Input();

            isMoving = GameManager.UserInput.Player.Move.ReadValue<float>() != 0;
            isCrouching = GameManager.UserInput.Player.Crouch.triggered;
            aimRaccoon = GameManager.UserInput.Player.RaccoonAim.triggered;
            callBackRaccoon = GameManager.UserInput.Player.CallBackRaccoon.triggered;
            dash = GameManager.UserInput.Player.Dash.triggered;
            punch = GameManager.UserInput.Player.Punch.triggered;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + stateData.timeToFlex)
            {
                player.SetAnimationParameter("Flex");
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (isMoving) stateMachine.ChangeState(player.WalkState);
            else if (isCrouching) stateMachine.ChangeState(player.CrouchState);
            else if (aimRaccoon) stateMachine.ChangeState(player.GrabRacoonState);
            else if (callBackRaccoon) stateMachine.ChangeState(player.CallBackRaccoonState);
            else if (dash) stateMachine.ChangeState(player.DashState);
            else if (punch) stateMachine.ChangeState(player.PunchState);
        }
    }
}
