using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Crocodile.States
{
    public class Idle : CrocodileState
    {
        private readonly Data.D_Idle stateData;

        private bool targetOnTop;
        private float lastGrowlTime;
        private float growlTime;

        public Idle(Crocodile entity, StateMachine<Crocodile> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            targetOnTop = false;

            lastGrowlTime = 0;

            growlTime = Random.Range(stateData.minTimeToSound, stateData.maxTimeToSound);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            targetOnTop = crocodile.AttackCheck.Check();

            if (Time.time >= lastGrowlTime + growlTime&& !crocodile.AudioSource.isPlaying)
            {
                crocodile.AudioSource.PlayOneShot(stateData.growlSFX);
                lastGrowlTime = Time.time;
                growlTime = Random.Range(stateData.minTimeToSound, stateData.maxTimeToSound);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (targetOnTop) stateMachine.ChangeState(crocodile.BiteState);
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}