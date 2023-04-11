using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Superstates
{
    public abstract class Airborne : PlayerState
    {
        public Airborne(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
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

        public override void Input()
        {
            base.Input();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.TimeInAir += Time.deltaTime;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            // Since the frame the player is in the air this check is triggered then it would transition immediately back to grounded
            // A small time is given so the player can be airborne before checking
            if (player.GroundCheck.Check() && player.CanLand && player.TimeInAir > 0.2) 
            {
                player.TimeInAir = 0;
                player.SetAnimationParameter("isAirborne", false);

                if (player.UserInput.Player.Move.ReadValue<float>() == 0)
                {
                    stateMachine.ChangeState(player.IdleState); 
                }
                else stateMachine.ChangeState(player.WalkState);
            }
        }
    }
}
