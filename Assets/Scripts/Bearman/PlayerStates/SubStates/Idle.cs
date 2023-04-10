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

        // Some state transitions might want to stop the player, others might want to keep momentum when changing to this state
        public bool fullyStopOnEnter = true;

        public Idle(Player entity, StateMachine<Player> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            if (fullyStopOnEnter) entity.SetVelocityX(0);

            isMoving = false;
        }

        public override void Input()
        {
            base.Input();

            isMoving = player.UserInput.Player.Move.ReadValue<float>() != 0;
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
        }
    }
}
