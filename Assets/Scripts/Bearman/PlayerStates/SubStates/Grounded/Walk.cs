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
        private float walkTime;
        private bool decelerating;
        private float decelerationTime;

        private float inputDirection;
        private bool isCrouching;
        private bool aimRaccoon;
        private bool callBackRaccoon;
        private bool dash;
        private bool punch;

        public Walk(Player entity, StateMachine<Player> stateMachine, Data.D_Walk stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            if (Mathf.Abs(player.Rb.velocity.x) > stateData.maxSpeed) walkTime = 1;
            else walkTime = 0;

            decelerating = false;
            decelerationTime = 0;
            callBackRaccoon = false;
            isCrouching = false;
            punch = false;
            
            player.CanJump = true;
            player.SetAnimationParameter("isMoving", true);
            GameManager.UserInput.Player.Move.performed += Move_performed;

            player.AnimationEvent += PlayFootstep;
        }


        public override void Exit()
        {
            base.Exit();

            player.SetAnimationParameter("isMoving", false);
            GameManager.UserInput.Player.Move.performed -= Move_performed;
            player.AnimationEvent -= PlayFootstep;
        }

        public override void Input()
        {
            base.Input();
            inputDirection = GameManager.UserInput.Player.Move.ReadValue<float>();

            isCrouching = GameManager.UserInput.Player.Crouch.triggered;
            aimRaccoon = GameManager.UserInput.Player.RaccoonAim.triggered;
            callBackRaccoon = GameManager.UserInput.Player.CallBackRaccoon.triggered;
            dash = GameManager.UserInput.Player.Dash.triggered;
            punch = GameManager.UserInput.Player.Punch.triggered;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (inputDirection != 0)
            {
                walkTime = Mathf.Clamp01(walkTime + Time.deltaTime);
                decelerating = false;
            }
            else if (!decelerating)
            {
                decelerationTime = (1 - walkTime) * stateData.timeToFullStop;
                walkTime =  Mathf.Min(0, walkTime - Time.deltaTime);
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

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            // Change to idle if there's no input and the player has stopped moving
            if (isCrouching) stateMachine.ChangeState(player.CrouchState);
            else if (callBackRaccoon) stateMachine.ChangeState(player.CallBackRaccoonState);
            else if (aimRaccoon && !player.Raccoon) stateMachine.ChangeState(player.GrabRacoonState);
            else if (dash) stateMachine.ChangeState(player.DashState);
            else if (punch) stateMachine.ChangeState(player.PunchState);
            else if (entity.Rb.velocity.x == 0 && inputDirection == 0) stateMachine.ChangeState(player.IdleState);
        }

        private void PlayFootstep()
        {
            int pos = (int) Mathf.Floor(Random.Range(0, stateData.footsteps.Length));

            player.AudioSource.PlayOneShot(stateData.footsteps[pos]);
        }
    }
}