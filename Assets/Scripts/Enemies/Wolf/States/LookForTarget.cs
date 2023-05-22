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
            lastFlip = 0;

            wolf.SetAnimationParameter("lookForPlayer", true);
            wolf.Anim.SetLayerWeight(1, 1);
        }

        public override void Exit()
        {
            base.Exit();

            wolf.FOV.transform.localEulerAngles = Vector3.zero;
            wolf.WolfHead.localEulerAngles = Vector3.zero;
            wolf.SetAnimationParameter("lookForPlayer", false);

            wolf.Anim.SetLayerWeight(1, 0);
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
                    wolf.FOV.transform.Rotate(new(0, 180, 0));
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
            else if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.ChaseState);
        }
    }
}
