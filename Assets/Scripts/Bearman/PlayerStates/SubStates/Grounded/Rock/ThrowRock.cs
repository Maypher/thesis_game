using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Substates.Grounded
{
    public class ThrowRock : Superstates.Ground
    {
        private Data.D_ThrowRock stateData;

        public ThrowRock(Player entity, StateMachine<Player> stateMachine, Data.D_ThrowRock stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            Rigidbody2D rb = player.Rock.GetComponent<Rigidbody2D>();

            player.Rock.transform.parent = null;

            rb.simulated = true;
            rb.velocity = stateData.throwForce * player.transform.right;
        }

        public override void Exit()
        {
            base.Exit();
            player.SetAnimationParameter("holdingRock", false);
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            stateMachine.ChangeState(player.IdleState);
        }
    }
}
