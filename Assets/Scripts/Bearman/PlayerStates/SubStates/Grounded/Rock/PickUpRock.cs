using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class PickUpRock : Superstates.Ground
    {
        private readonly Data.D_PickUpRock stateData;

        private bool animationFinished;

        private Transform rockSpawnLocation;

        public PickUpRock(Player entity, StateMachine<Player> stateMachine, Data.D_PickUpRock stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;

            rockSpawnLocation = player.transform.Find("RockSpawnPos");
        }


        public override void Enter()
        {
            base.Enter();

            player.CanJump = false;
            player.SetVelocityX(0);

            animationFinished = false;
            player.SetAnimationParameter("pickUpRock");

            player.AnimationEvent += AnimationFinished;
        }

        public override void Exit()
        {
            base.Exit();
            player.AnimationEvent -= AnimationFinished;
        }

        public override void Input()
        {
            base.Input();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (animationFinished)
            {
                SpawnRock();
                stateMachine.ChangeState(player.HoldRockState);
            }
        }

        private void AnimationFinished() => animationFinished = true;

        private void SpawnRock() 
        { 
            player.Rock = MonoBehaviour.Instantiate(stateData.rockPrefab, rockSpawnLocation);
            player.AudioSource.PlayOneShot(stateData.pickUpRockSFX, .3f);
        }
    }
}
