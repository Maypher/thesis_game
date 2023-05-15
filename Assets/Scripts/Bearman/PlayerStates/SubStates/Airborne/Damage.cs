using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Airborne
{
    public class Damage : Superstates.Airborne
    {
        private readonly Data.D_Damage stateData;

        public Damage(Player entity, StateMachine<Player> stateMachine, Data.D_Damage stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            player.CanBeDamaged = false;
            player.SetAnimationParameter("takeDamage", true);
            
            player.StartCoroutine(InvinsibilityFrames());
        }

        public override void Input()
        {
            base.Input();
        }

        public override void Exit()
        {
            base.Exit();

            player.SetAnimationParameter("takeDamage", false);
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

        private IEnumerator InvinsibilityFrames()
        {
            float timeBetweenFlashes = stateData.invincibilityTime / (stateData.flashTimes * 2);
            
            // Set the layer of the player to a different layer so it doesn't get detected by enemies
            player.gameObject.layer = LayerMask.NameToLayer("InvinsibleToEnemies");

            Physics2D.IgnoreLayerCollision(player.gameObject.layer, stateData.enemyLayer, true);

            for (int i = 0; i < stateData.flashTimes; i++)
            {
                player.SpriteRenderer.color = new Color(1, 0, 0, .5f);
                yield return new WaitForSeconds(timeBetweenFlashes);
                player.SpriteRenderer.color = Color.white;
                yield return new WaitForSeconds(timeBetweenFlashes);
            }

            Physics2D.IgnoreLayerCollision(player.gameObject.layer, stateData.enemyLayer, false);
            player.CanBeDamaged = true;
            // Set the player back to its base layer
            player.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}