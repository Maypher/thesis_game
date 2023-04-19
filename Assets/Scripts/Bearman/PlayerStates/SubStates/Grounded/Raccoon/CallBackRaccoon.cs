using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Grounded
{
    public class CallBackRaccoon : PlayerState
    {
        public CallBackRaccoon(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (player.Raccoon == null) 
            { 
                stateMachine.ChangeState(player.IdleState);
                return;
            }

            player.Raccoon.gameObject.SetActive(true);
            player.Raccoon.StateMachine.ChangeState(player.Raccoon.ReturnToPlayerState);
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            stateMachine.ChangeState(player.IdleState);
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}