using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Crocodile.States
{
    public class Bite : CrocodileState, IAttack
    {
        private readonly Data.D_Bite stateData;

        private Vector2 startPosition;
        private bool finishBite;

        private float biteTime;

        public Bite(Crocodile entity, StateMachine<Crocodile> stateMachine, Data.D_Bite stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;

            startPosition = crocodile.transform.position;
        }
   
        public override void Enter()
        {
            base.Enter();

            biteTime = 0;
            finishBite = false;
            crocodile.StartCoroutine(ShakeCoroutine());
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (finishBite && Time.time >= biteTime + stateData.timeAfterBite) stateMachine.ChangeState(crocodile.HideState);
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private IEnumerator ShakeCoroutine()
        {
            float timeElapsed = 0f;

            while (timeElapsed < stateData.shakeDuration)
            {
                while (PauseMenu.IsPaused) yield return null;

                float x = Random.Range(-1f, 1f) * stateData.shakeIntensity;
                float y = Random.Range(-1f, 1f) * stateData.shakeIntensity;
                crocodile.Sprite.position = new Vector2(startPosition.x + x, startPosition.y + y);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            crocodile.Sprite.transform.position = startPosition;
            Attack();
        }
    
        public void Attack()
        {
            Collider2D[] enemies = crocodile.AttackCheck.GetEnemies();

            foreach (Collider2D enemy in enemies)
            {
                stateData.attackDetails.attackPostion = crocodile.AttackCheck.transform.position;
                enemy.GetComponent<IDamageable>()?.TakeDamage(stateData.attackDetails);
            }

            finishBite = true;
            biteTime = Time.time;
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}