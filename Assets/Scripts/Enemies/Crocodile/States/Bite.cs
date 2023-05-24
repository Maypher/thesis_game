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

        private bool killedPlayer;

        public Bite(Crocodile entity, StateMachine<Crocodile> stateMachine, Data.D_Bite stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;

            startPosition = crocodile.transform.position;
        }

        public override void Enter()
        {
            base.Enter();

            finishBite = false;
            killedPlayer = false;


            crocodile.AnimationEvent += FinishBite;
            crocodile.FinishAnimation += GoDown;
            GameManager.Player.PlayerDeath += KillPlayer;

            crocodile.StartCoroutine(ShakeCoroutine());
        }

        public override void Exit()
        {
            base.Exit();

            GameManager.Player.PlayerDeath -= KillPlayer;
            crocodile.AnimationEvent -= FinishBite;
            crocodile.FinishAnimation -= GoDown;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (finishBite && !killedPlayer) stateMachine.ChangeState(crocodile.HideState);
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
            crocodile.SetAnimationParameter("bite");
            crocodile.AudioSource.PlayOneShot(stateData.biteSFX);
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
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }

        private void KillPlayer() => killedPlayer = true;

        private void FinishBite() 
        {
            if (killedPlayer) crocodile.SetAnimationParameter("goodBite");
            else
            {
                crocodile.SetAnimationParameter("failBite");
                crocodile.SetAnimationParameter("underwater", true);
            }
        }

        private void GoDown() => finishBite = true;
    }
}