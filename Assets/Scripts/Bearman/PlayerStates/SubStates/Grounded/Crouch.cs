using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class Crouch : PlayerState
    {
        private bool isCrouching;
        private bool pickUpRock;

        public Crouch(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.SetAnimationParameter("isCrouching", true);

            player.SetVelocityX(0);
        }

        public override void Input()
        {
            base.Input();

            isCrouching = GameManager.UserInput.Player.Crouch.IsPressed();
            pickUpRock = GameManager.UserInput.Player.PickUpRock.WasPerformedThisFrame();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (pickUpRock) stateMachine.ChangeState(player.PickUpRockState);
            else if (!isCrouching) stateMachine.ChangeState(player.IdleState);
        }

        public override void Exit()
        {
            base.Exit();

            player.SetAnimationParameter("isCrouching", false);
        }
    }
}
