using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Gunner.States
{
    public class Idle : GunnerState
    {
        private readonly Data.D_Idle stateData;

        private float lastFlip;
        private float timeToFlip;

        // Since other states also rotate the gun, when using Mathf.PingPong() the rotation will immediately snap back to where it left off
        // when changing the state making it seem like it teleports. By using this it makes sure the rotation is zeroed out before using PingPong()
        private bool hasNoRotationAtStart;
        private float rotationTimer;

        public Idle(Gunner entity, StateMachine<Gunner> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            lastFlip = Time.time;
            timeToFlip = Random.Range(stateData.minFlipTime, stateData.maxFlipTime);

            rotationTimer = 0;
            hasNoRotationAtStart = false;
        }
       
        public override void Exit()
        {
            base.Exit();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (gunner.FOV.Check()) stateMachine.ChangeState(gunner.ShootState);
        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateData.canFlip && Time.time >= lastFlip + timeToFlip) 
            {
                gunner.Flip();
                lastFlip = Time.time;
            }

            if (hasNoRotationAtStart)
            {
                rotationTimer += Time.fixedDeltaTime;
                gunner.Gun.transform.eulerAngles = new Vector3(0, 0, Mathf.PingPong(rotationTimer * stateData.rotationSpeed, stateData.rotationAngle) - stateData.rotationOffset);

            }
            else
            {
                gunner.Gun.transform.rotation = Quaternion.RotateTowards(gunner.Gun.transform.rotation, Quaternion.identity, .5f);
                hasNoRotationAtStart = gunner.Gun.transform.rotation == Quaternion.identity;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}