using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne
{
    public class Jump : PlayerState
    {
        private Data.D_Jump stateData;

        public Jump(Player entity, StateMachine<Player> stateMachine, Data.D_Jump stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            player.SetAnimationParameter("jump");

            player.SetVelocityY(stateData.jumpForce);
            player.Rb.gravityScale = stateData.jumpGravity;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            stateMachine.ChangeState(player.AirMoveState);
        }
    }
}