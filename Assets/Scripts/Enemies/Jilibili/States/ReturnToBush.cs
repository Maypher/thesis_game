using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States
{
    public class ReturnToBush : JilibiliState
    {
        private readonly Data.D_ReturnToBush stateData;

        private bool finishedHiding;

        public ReturnToBush(Jilibili entity, StateMachine<Jilibili> stateMachine, Data.D_ReturnToBush stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            jilibili.enteredBush += Hide;

            finishedHiding = false;
        }

        public override void Exit()
        {
            base.Exit();

            jilibili.enteredBush -= Hide;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (finishedHiding) stateMachine.ChangeState(jilibili.InsideBushState);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            int directionToBush = (int) Mathf.Sign(jilibili.Bush.transform.position.x - jilibili.transform.position.x);

            if (directionToBush != jilibili.FacingDirection) jilibili.Flip();
            jilibili.SetVelocityX(stateData.moveSpeed);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void Hide()
        {
            //TODO: Play animation

            finishedHiding = true;
        }
    }
}