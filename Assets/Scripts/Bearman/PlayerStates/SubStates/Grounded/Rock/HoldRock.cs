using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class HoldRock : Superstates.Ground
    {
        private readonly Data.D_HoldRock stateData;

        private float lastMove;

        private bool wantsToMove;

        private bool throwRock;

        public HoldRock(Player entity, StateMachine<Player> stateMachine, Data.D_HoldRock stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            lastMove = Time.time;
            throwRock = false;

            player.SetAnimationParameter("holdingRock", true);

            player.FinishAnimation += Stop;
        }

        public override void Exit()
        {
            base.Exit();

            player.SetAnimationParameter("isMoving", false);
            if (!player.Rock) player.SetAnimationParameter("holdingRock", false);

            player.FinishAnimation -= Stop;
        }

        public override void Input()
        {
            base.Input();

            // Only move in the direction the player was facing when the rock was picked up
            wantsToMove = GameManager.UserInput.Player.Move.ReadValue<float>() == player.FacingDirection;

            throwRock = GameManager.UserInput.Player.Throw.triggered;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (wantsToMove && Time.time >= lastMove + stateData.moveInterval) Move();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (throwRock) stateMachine.ChangeState(player.ThrowRockState);
            else if (!player.Rock) stateMachine.ChangeState(player.IdleState);
        }

        private void Move()
        {
            player.SetAnimationParameter("isMoving", true);
            player.SetVelocityX(stateData.moveSpeed);
        }

        private void Stop()
        {
            player.SetAnimationParameter("isMoving", false);
            player.SetVelocityX(0);
            lastMove = Time.time;
        }
    }
}