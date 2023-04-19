using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class ThrowRaccoon : Superstates.Ground
    {
        private readonly Data.D_ThrowRaccoon stateData;

        private bool finishedAnimation;

        private readonly Transform launchPos;

        public ThrowRaccoon(Player entity, StateMachine<Player> stateMachine, Data.D_ThrowRaccoon stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
            launchPos = player.transform.Find("RaccoonLaunchPos");
        }

        //TODO: Play animation, spawn raccoon, apply force to it (animation already has finish animation event)
        public override void Enter()
        {
            base.Enter();

            finishedAnimation = false;

            player.AnimationEvent += SpawnRaccoon;
            player.FinishAnimation += FinishAnimation;
            
            player.SetAnimationParameter("throwRaccoon");
        }

        public override void Exit()
        {
            base.Exit();

            player.AnimationEvent -= SpawnRaccoon;
            player.FinishAnimation -= FinishAnimation;
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

            if (finishedAnimation) stateMachine.ChangeState(player.IdleState);
        }

        private void FinishAnimation() => finishedAnimation = true;

        private void SpawnRaccoon() => player.Raccoon = (Raccoon.Raccoon)GameObject.Instantiate(stateData.raccoon, launchPos.position, player.transform.rotation);
    }
}