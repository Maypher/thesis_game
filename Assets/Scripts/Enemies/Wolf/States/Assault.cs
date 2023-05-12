using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Assault : WolfState
    {
        private readonly Data.D_Assault stateData;

        private bool attack;

        public Assault(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Assault stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            attack = false;

            wolf.AttackCheck.enemyEnteredAttackArea += ChangeToAttack;

            wolf.SetVelocity(stateData.jumpForce, stateData.jumpAngle, wolf.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();

            wolf.AttackCheck.enemyEnteredAttackArea -= ChangeToAttack;
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

            if (attack) stateMachine.ChangeState(wolf.AttackState);
            else if (wolf.GroundCheck.Check() && Time.time >= startTime + 0.2f) stateMachine.ChangeState(wolf.LookForTargetState);
        }

        private void ChangeToAttack() => attack = true;
    }
}
