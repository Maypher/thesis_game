using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States
{
    public class InsideBush : JilibiliState
    {
        private readonly Data.D_InsideBush stateData;

        private bool hiding;
        private float jumpTime;

        public InsideBush(Jilibili entity, StateMachine<Jilibili> stateMachine, Data.D_InsideBush stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            jilibili.SetVelocity(0, Vector2.zero, 0);

            jilibili.transform.position = jilibili.StartPos;

            SetObjectStatus(false);
            
            hiding = true;
            jumpTime = 0;

            jilibili.DetectionZone.enemyEnteredAttackArea += JumpOut;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            // startTime + 0.7 temporary fix until animation is done
            if (!hiding && jilibili.GroundCheck.Check() && Time.time > jumpTime + 0.4) stateMachine.ChangeState(jilibili.IdleState);
        }

        public override void Exit()
        {
            base.Exit();

            jilibili.DetectionZone.enemyEnteredAttackArea -= JumpOut;
        }

        private void JumpOut()
        {
            if (!jilibili.FOV.Check()) jilibili.Flip();

            Transform player = jilibili.DetectionZone.GetEnemies()[0].transform;

            int direction = (int) -Mathf.Sign(player.position.x - jilibili.transform.position.x);

            SetObjectStatus(true);

            //TODO: play jump animation

            jilibili.SetVelocity(stateData.jumpForce, stateData.jumpAngle, -jilibili.FacingDirection);

            jumpTime = Time.time;
            hiding = false;
        }

        private void SetObjectStatus(bool active)
        {
            jilibili.SpriteRenderer.enabled = active;
            jilibili.Rb.isKinematic = !active;
            jilibili.Collider.enabled = active;
        }
    }
}
