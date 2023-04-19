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
        private bool pickUpRock;
        private bool aimRaccoon;
        private bool callBackRaccoon;
        private bool dash;
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
            pickUpRock = false;
            callBackRaccoon = false;
        }

        public override void Input()
        {
            base.Input();

            isMoving = player.UserInput.Player.Move.ReadValue<float>() != 0;
            pickUpRock = player.UserInput.Player.PickUpRock.triggered;
            aimRaccoon = player.UserInput.Player.RaccoonAim.triggered;
            callBackRaccoon = player.UserInput.Player.CallBackRaccoon.triggered;
            dash = player.UserInput.Player.Dash.triggered;
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
            else if (pickUpRock) stateMachine.ChangeState(player.PickUpRockState);
            else if (aimRaccoon) stateMachine.ChangeState(player.GrabRacoonState);
            else if (callBackRaccoon) stateMachine.ChangeState(player.CallBackRaccoonState);
            else if (dash) stateMachine.ChangeState(player.DashState);
        }
    }
}
