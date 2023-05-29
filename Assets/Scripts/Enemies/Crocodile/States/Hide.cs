using StateMachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Enemies.Crocodile.States
{
    public class Hide : CrocodileState
    {
        private readonly Data.D_Hide stateData;

        private Vector2 endPosition;

        private bool finishedRising;

        public Hide(Crocodile entity, StateMachine<Crocodile> stateMachine, Data.D_Hide stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            finishedRising = false;
            endPosition = new(crocodile.StartPosition.x, crocodile.StartPosition.y - 3);

            crocodile.Ground.enabled = false;

            crocodile.StartCoroutine(Lower());

            crocodile.SetAnimationParameter("underwater", true);

        }

        public override void Exit()
        {
            base.Exit();

            crocodile.Ground.enabled = true;
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (finishedRising) stateMachine.ChangeState(crocodile.IdleState);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private IEnumerator Lower()
        {
            float hideTimer = 0;

            while (crocodile.transform.localPosition != (Vector3)endPosition)
            {
                hideTimer += Time.deltaTime;

                float t = Mathf.Clamp01(hideTimer / stateData.hideTime);
                crocodile.transform.localPosition = Vector2.Lerp(crocodile.StartPosition, endPosition, t);
                
                yield return null;
            }

            yield return new WaitForSeconds(stateData.timeUnderwater);
            crocodile.SetAnimationParameter("underwater", false);
            crocodile.StartCoroutine(Rise());
        }

        private IEnumerator Rise()
        {
            float riseTimer = 0;

            while (crocodile.transform.localPosition != (Vector3) crocodile.StartPosition)
            {
                riseTimer += Time.deltaTime;

                float t = Mathf.Clamp01(riseTimer / stateData.riseTime);
                crocodile.transform.localPosition = Vector2.Lerp(endPosition, crocodile.StartPosition, t);

                yield return null;
            }

            finishedRising = true;
        }
    }
}