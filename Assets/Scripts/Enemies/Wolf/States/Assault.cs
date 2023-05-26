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

        private float lastJump = Time.time;

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
            wolf.SetAnimationParameter("jumping", true);

            lastJump = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            wolf.AttackCheck.enemyEnteredAttackArea -= ChangeToAttack;
            wolf.SetAnimationParameter("jumping", false);
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
            else if (wolf.GroundCheck.Check()) 
            {
                if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.ChaseState);
                else stateMachine.ChangeState(wolf.LookForTargetState); 
            }
        }

        private void ChangeToAttack() => attack = true;
    }
}
