using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States
{
    public class JumpForward : JilibiliState
    {
        private readonly Data.D_JumpForward stateData;

        private bool attack;

        public JumpForward(Jilibili entity, StateMachine<Jilibili> stateMachine, Data.D_JumpForward stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }


        public override void Enter()
        {
            base.Enter();

            attack = false;

            jilibili.AttackCheck.enemyEnteredAttackArea += ChangeToAttack;

            jilibili.SetVelocity(stateData.jumpForce, stateData.jumpAngle, 1);
        }


        public override void Exit()
        {
            base.Exit();

            jilibili.AttackCheck.enemyEnteredAttackArea -= ChangeToAttack;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (attack) stateMachine.ChangeState(jilibili.AssaultState);
            else if (jilibili.GroundCheck.Check() && Time.time > startTime + 0.3) stateMachine.ChangeState(jilibili.IdleState);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void ChangeToAttack() => attack = true;
    }
}