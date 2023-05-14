using StateMachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Player.Raccoon.States
{
    public class ReturnToPlayer : RaccoonState
    {
        private readonly Data.D_ReturnToPlayer stateData;

        private Collider2D playerCollider = GameManager.Player.GetComponent<Collider2D>();

        public ReturnToPlayer(Raccoon entity, StateMachine<Raccoon> stateMachine, Data.D_ReturnToPlayer stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            float directionToPlayer = (int) Mathf.Sign((GameManager.Player.transform.localPosition - raccoon.transform.position).x);
            MoveOutsideCameraView(directionToPlayer);

            raccoon.SetVelocity(0, Vector2.zero, 0);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            raccoon.transform.position = Vector3.MoveTowards(raccoon.transform.position, new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.max.y), stateData.maxDistanceDelta);
        }

        // Used after being enabled to place the raccoon outside the camera view and place it on the ground
        private void MoveOutsideCameraView(float directionToPlayer)
        {
            float spawnX = Camera.main.ViewportToWorldPoint(directionToPlayer == -1 ? Vector3.right : Vector3.zero).x + stateData.screenSpawnInset * directionToPlayer;
            float spawnY = Camera.main.ViewportToWorldPoint(Vector3.one).y;

            raccoon.transform.position = new Vector2(spawnX, spawnY);
        }
    }
}