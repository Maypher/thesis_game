using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States
{
    public class Idle : JilibiliState
    {
        private readonly Data.D_Idle stateData;

        private bool retreat;
        private float timeToAttack;

        public Idle(Jilibili entity, StateMachine<Jilibili> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            jilibili.SetVelocityX(0);

            retreat = false;
            timeToAttack = Random.Range(stateData.minAttackTime, stateData.maxAttackTime);

            jilibili.AwarenessZone.enemyEnteredAttackArea += TryRetreat;
        }

        public override void Exit()
        {
            base.Exit();

            jilibili.AwarenessZone.enemyEnteredAttackArea -= TryRetreat;
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

            if (retreat) stateMachine.ChangeState(jilibili.RetreatState);
            else if (Time.time >= startTime + timeToAttack) stateMachine.ChangeState(jilibili.JumpForwardState);
            else if (!jilibili.FOV.Check()) stateMachine.ChangeState(jilibili.ReturnToBushState);
        }

        private void TryRetreat() => retreat = Random.value <= stateData.retreatProbability;
    }
}
