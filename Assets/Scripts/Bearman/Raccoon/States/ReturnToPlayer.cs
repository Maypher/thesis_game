using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States
{
    public class ReturnToPlayer : RaccoonState
    {
        private readonly Data.D_ReturnToPlayer stateData;

        private float directionToPlayer;

        public ReturnToPlayer(Raccoon entity, StateMachine<Raccoon> stateMachine, Data.D_ReturnToPlayer stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            directionToPlayer = GetDirectionToPlayer();
            MoveOutsideCameraView();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            directionToPlayer = raccoon.DirectionToTarget(GameManager.Player.gameObject);

            if (directionToPlayer != raccoon.FacingDirection) raccoon.Flip();

            raccoon.SetVelocityX(stateData.moveSpeed);
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (raccoon.FOV.Check()) stateMachine.ChangeState(raccoon.JumpToPlayerState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private int GetDirectionToPlayer() => (int) Mathf.Sign((GameManager.Player.transform.localPosition - raccoon.transform.position).x);

        // Used after being enabled to place the raccoon outside the camera view and place it on the ground
        private void MoveOutsideCameraView()
        {
            float spawnX = Camera.main.ViewportToWorldPoint(directionToPlayer == -1 ? Vector3.right : Vector3.zero).x - 7 * directionToPlayer;

            // Spawn outside camera view and up to check where ground is
            raccoon.transform.position = new Vector2(spawnX, 10);

            RaycastHit2D groundPos = Physics2D.Raycast(raccoon.transform.position, Vector2.down, stateData.whatIsGround);

            if (groundPos) raccoon.transform.position = groundPos.point + new Vector2(0, .1f); // Offset to avoid intersecting with ground
        }
    }
}