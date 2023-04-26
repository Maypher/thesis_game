using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Assault : WolfState, IAttack
    {
        private readonly Data.D_Assault stateData;

        public Assault(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Assault stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            wolf.AttackCheck.enemyEnteredAttackArea += Attack;

            wolf.SetVelocity(stateData.jumpForce.x, stateData.jumpForce, wolf.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();
            wolf.AttackCheck.enemyEnteredAttackArea -= Attack;
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
        }

        public void Attack()
        {
            Debug.Log("Attacked");
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}
