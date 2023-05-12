using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States
{
    public class Retreat : JilibiliState
    {
        private readonly Data.D_Retreat stateData;

        public Retreat(Jilibili entity, StateMachine<Jilibili> stateMachine, Data.D_Retreat stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            jilibili.SetVelocity(stateData.jumpForce, stateData.jumpAngle, -jilibili.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();
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

            if (jilibili.GroundCheck.Check() && Time.time > startTime + .1f) stateMachine.ChangeState(jilibili.IdleState);
        }

    }
}