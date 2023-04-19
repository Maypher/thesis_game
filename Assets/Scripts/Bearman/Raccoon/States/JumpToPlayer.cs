using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States
{
    public class JumpToPlayer : RaccoonState
    {
        private readonly Data.D_JumpToPlayer stateData;

        public JumpToPlayer(Raccoon entity, StateMachine<Raccoon> stateMachine, Data.D_JumpToPlayer stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            raccoon.PlayerCheck.enemyEnteredAttackArea += Despawn;

            raccoon.SetVelocityX(stateData.jumpForce.x);
            raccoon.SetVelocityY(stateData.jumpForce.y);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        private void Despawn()
        {
            GameManager.Player.Raccoon = null;
            GameObject.Destroy(raccoon.gameObject);
        }
    }
}
