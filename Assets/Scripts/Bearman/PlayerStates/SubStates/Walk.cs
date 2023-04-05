using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Substates.Grounded
{
    public class Walk : Superstates.Ground
    {
        private Data.D_Walk stateData;
        
        private bool usingKeyboard;
        private float inputDirection;
        private float walkTime;
        private bool decelerating;
        private float decelerationTime;

        public Walk(Player entity, StateMachine<Player> stateMachine, string animBoolName, Data.D_Walk stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            walkTime = 0;
            decelerating = false;
            decelerationTime = 0;
            
            player.UserInput.Player.Move.performed += Move_performed;
        }


        public override void Exit()
        {
            base.Exit();
            player.UserInput.Player.Move.performed -= Move_performed;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            inputDirection = player.UserInput.Player.Move.ReadValue<float>();

            if (inputDirection != 0)
            {
                walkTime = Mathf.Clamp01(walkTime + Time.deltaTime);
                decelerating = false;
            }
            else if (!decelerating)
            {
                decelerationTime = (1 - walkTime) * stateData.timeToFullStop;
                walkTime = 0;
                decelerating = true;
            }
            else if (decelerating) decelerationTime = Mathf.Clamp(decelerationTime + Time.deltaTime, 0, stateData.timeToFullStop);

            if (Mathf.Sign(player.FacingDirection) != Mathf.Sign(inputDirection) && inputDirection != 0) player.Flip();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (inputDirection != 0)
            {
                // When using the keyboard apply speed incrementally over a fixed time
                if (usingKeyboard)
                {
                    player.SetVelocityX(stateData.acceleration.Evaluate(walkTime / stateData.timeToMaxSpeed) * stateData.maxSpeed);
                }
                // If using a joystick then let the regular ease take control of the speed
                else
                {
                    player.SetVelocityX(stateData.acceleration.Evaluate(Mathf.Abs(inputDirection)) * stateData.maxSpeed);
                }
            }
            else
            {
                player.SetVelocityX(stateData.deceleration.Evaluate(decelerationTime / stateData.timeToFullStop) * stateData.maxSpeed);
            }
        }

        // Since the active control scheme can only be accessed from context then this function's task is only to determine
        // if keyboard or game-pad in use
        private void Move_performed(InputAction.CallbackContext ctx)
        {
             usingKeyboard = ctx.action.activeControl.device.name == "Keyboard";
        }
    }
}