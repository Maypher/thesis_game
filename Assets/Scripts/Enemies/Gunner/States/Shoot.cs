using Player;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Enemies.Gunner.States
{
    public class Shoot : GunnerState, IAttack
    {
        private readonly Data.D_Shoot stateData;

        private float lastShot;

        private bool canAttack;

        private readonly GameObject shootFire;

        public Shoot(Gunner entity, StateMachine<Gunner> stateMachine, Data.D_Shoot stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
            shootFire = gunner.transform.Find("Gun").Find("shootFire").gameObject;
        }


        public override void Enter()
        {
            base.Enter();

            lastShot = Time.time;


            canAttack = false;
            gunner.FinishAnimation += StartAttacking;

            gunner.SetAnimationParameter("attack", true);

            gunner.AudioSource.loop =true;
            gunner.AudioSource.PlayOneShot(stateData.laughSFX, .4f);
        }

        public override void Exit()
        {
            base.Exit();
            gunner.FinishAnimation -= StartAttacking;
            gunner.SetAnimationParameter("attack", false);
            gunner.AudioSource.loop = false;
            gunner.StartCoroutine(FadeOutAudio());
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (!gunner.FOV.Check()) stateMachine.ChangeState(gunner.IdleState);
        }

        public override void LogicUpdate()
        {
            if (!canAttack) return;

            base.LogicUpdate();

            Vector3 direction = GameManager.Player.transform.position - gunner.Gun.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            gunner.Gun.transform.rotation = Quaternion.RotateTowards(gunner.Gun.transform.rotation, targetRotation, stateData.rotateSpeed * Time.deltaTime);

            if (Time.time >= lastShot + Random.Range(stateData.minShootInterval, stateData.maxShootInterval)) { Attack(); lastShot = Time.time; }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void Attack()
        {
            GameObject.Instantiate(stateData.bulletTrail, gunner.Gun.transform.position, gunner.Gun.transform.rotation);
            gunner.StartCoroutine(ShowFire());
            gunner.AudioSource.PlayOneShot(stateData.shootSFX, .2f);

            RaycastHit2D enemy = Physics2D.Raycast(gunner.Gun.transform.position, gunner.Gun.transform.right, 20, stateData.whatIsEnemy);

            stateData.attackDetails.attackPostion = gunner.Gun.transform.position;
            enemy.collider?.GetComponent<IDamageable>()?.TakeDamage(stateData.attackDetails);

            float recoilForce = Random.Range(-stateData.gunRecoilAngleRange, stateData.gunRecoilAngleRange);
            gunner.Gun.transform.rotation *= Quaternion.Euler(0, 0, recoilForce);
        }

        public void FinishAttack()
        {
            throw new System.NotImplementedException();
        }

        private void StartAttacking() => canAttack = true;

        private IEnumerator ShowFire()
        {
            shootFire.SetActive(true);
            yield return new WaitForSeconds(stateData.fireTime);
            shootFire.SetActive(false);
        }

        private IEnumerator FadeOutAudio()
        {
            while (gunner.AudioSource.volume > 0)
            {
                float newVolume = gunner.AudioSource.volume - (stateData.laughFadeTime * Time.deltaTime);
                newVolume  = newVolume > 0f ? newVolume : 0;
                gunner.AudioSource.volume = newVolume;
                yield return null;
            }

            gunner.AudioSource.Stop();
            gunner.AudioSource.volume = 1;
        }
    }
}