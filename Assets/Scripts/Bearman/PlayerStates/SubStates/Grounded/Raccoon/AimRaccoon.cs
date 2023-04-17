using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class AimRaccoon : Superstates.Ground
    {
        private readonly Data.D_AimRaccoon stateData;

        private GameObject launchPos;
        private SpriteRenderer arrow;

        private bool aiming;
        private bool throwRaccoon;
        private float rotationTimer;
       

        public AimRaccoon(Player entity, StateMachine<Player> stateMachine, Data.D_AimRaccoon stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
            launchPos = player.transform.Find("RaccoonLaunchPos").gameObject;
            arrow = launchPos.GetComponent<SpriteRenderer>();
        }


        public override void Enter()
        {
            base.Enter();

            arrow.flipX = player.FacingDirection == -1;

            launchPos.transform.eulerAngles = Vector3.zero;
            launchPos.SetActive(true);

            player.SetVelocityX(0);
            player.CanJump = false;
            
            rotationTimer = 0;

            player.SetAnimationParameter("aiming", true);
        }

        public override void Exit()
        {
            base.Exit();

            launchPos.SetActive(false);
            player.SetAnimationParameter("aiming", false);
            player.CanJump = true;
        }

        public override void Input()
        {
            base.Input();

            aiming = player.UserInput.Player.RaccoonAim.IsPressed();
            throwRaccoon = player.UserInput.Player.Throw.triggered;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            rotationTimer += Time.fixedDeltaTime;

            launchPos.transform.eulerAngles = new Vector3(0, 0, Mathf.PingPong(rotationTimer * stateData.rotationSpeed, stateData.rotationAngle) - stateData.rotationOffset);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
       
        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (!aiming)
            {
                player.GrabRacoonState.putRaccoonBack = true;
                stateMachine.ChangeState(player.GrabRacoonState);
            }
            else if (throwRaccoon) stateMachine.ChangeState(player.ThrowRaccoonState);
        }
    }
}
