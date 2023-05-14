using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class GrabRaccoon : Superstates.Ground
    {
        private bool finishedAnimation;

        // When the player decides to cancel the throw this is used to rewind the animation instead of
        // creating a completely new state
        public bool putRaccoonBack = false;

        public GrabRaccoon(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (putRaccoonBack) player.SetAnimationParameter("leaveRaccoon");
            else player.SetAnimationParameter("grabRaccoon");
            player.CanJump = false;

            player.SetVelocityX(0);

            finishedAnimation = false;

            // Since this same state is used for putting back the raccoon, the animation is played backwards
            // making the FinishAnimation event trigger immediately on enter and transition to Idle when the animation is still playing.
            // To handle this issue an event was added at the beginning of the animation to work as a finishAnimation when putting back the raccoon
            if (putRaccoonBack) player.AnimationEvent += FinishAnim;
            else player.FinishAnimation += FinishAnim;
        }

        public override void Exit()
        {
            base.Exit();

            putRaccoonBack = false;
            player.AnimationEvent -= FinishAnim;
            player.FinishAnimation -= FinishAnim;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (finishedAnimation)
            {
                if (putRaccoonBack) stateMachine.ChangeState(player.IdleState);
                else stateMachine.ChangeState(player.AimRaccoonState);
            }
        }

        private void FinishAnim() => finishedAnimation = true;
    }
}