using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class LookForTarget : WolfState
    {
        private readonly Data.D_LookForTarget stateData;

        private int flipCounter;
        private float lastFlip;

        private bool finished;

        public LookForTarget(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_LookForTarget stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            wolf.SetVelocityX(0);

            finished = false;
            flipCounter = stateData.flipTimes;
            lastFlip = Time.time;

            wolf.SetAnimationParameter("lookForPlayer", true);
            wolf.Anim.SetLayerWeight(1, 1);

            wolf.AudioSource.clip = stateData.lookSFX;
            wolf.AudioSource.Play();
            wolf.AudioSource.loop = true;

            wolf.SetAnimationParameter("flipHead");
        }

        public override void Exit()
        {
            base.Exit();

            wolf.SetAnimationParameter("lookForPlayer", false);
            wolf.WolfHead.localEulerAngles = Vector3.zero;

            wolf.Anim.SetLayerWeight(1, 0);

            wolf.AudioSource.Stop();
            wolf.AudioSource.loop = false;

            int directionToPlayer = (int)Mathf.Sign(GameManager.Player.transform.position.x - wolf.transform.position.x);

            if (wolf.FOV.Check() && wolf.FacingDirection != directionToPlayer) wolf.Flip();
            wolf.FOV.Scale.Set(Mathf.Abs(wolf.FOV.Scale.x), wolf.FOV.Scale.y);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(Time.time >= lastFlip + Random.Range(stateData.minFlipDuration, stateData.maxFlipDuration))
            {
                if (flipCounter == 0) finished = true;
                else
                {
                    lastFlip = Time.time;
                    wolf.FOV.Scale.Set(wolf.FOV.Scale.x * -1, wolf.FOV.Scale.y);
                    wolf.SetAnimationParameter("flipHead");
                    flipCounter--;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (finished) stateMachine.ChangeState(wolf.MoveState);
            else if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.TargetDetectedState);
        }
    }
}
