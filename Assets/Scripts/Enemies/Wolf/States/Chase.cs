using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Chase : WolfState
    {
        private readonly Data.D_Chase stateData;

        private bool wall, noLedge, noPlatform;

        private float lastBark, timeToBark;

        // Used in the transition to tired state. Since the wolf shouldn't transition to it when jumping
        // a ground check is required but the position of the ground makes the wolf be halfway out of the ground
        // when landing and transitioning in that frame wouldn't make sense. Thus a timer is added
        // to allow a leeway for the wolf to fully go into the ground.
        private float runningTimer;

        public Chase(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Chase stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            runningTimer = 0;
            wall = false;
            noLedge = false;
            noPlatform = false;

            wolf.SetVelocityX(stateData.moveSpeed);
            wolf.SetAnimationParameter("chase", true);

            timeToBark = Random.Range(stateData.minBarkTime, stateData.maxBarkTime);
            lastBark = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            wolf.SetAnimationParameter("chase", false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= lastBark + timeToBark) 
            { 
                wolf.AudioSource.PlayOneShot(stateData.barkSFX[(int)Mathf.Floor(Random.Range(0, stateData.barkSFX.Length))]);
                lastBark = Time.time;
                timeToBark = Random.Range(stateData.minBarkTime, stateData.maxBarkTime);
            }

            if (wolf.GroundCheck.Check())
            {
                wolf.SetAnimationParameter("chase", true);
                wolf.SetAnimationParameter("jump", false);

                noLedge = !wolf.CheckLedge();
                wall = wolf.CheckWall();

                wolf.SetVelocityX(stateData.moveSpeed);

                if (noLedge) 
                {
                    runningTimer = 0;
                    JumpGap();
                    wolf.SetAnimationParameter("jump", true);
                    wolf.SetAnimationParameter("chase", false);
                }
                else if (wall)
                {
                    runningTimer = 0;
                    JumpWall();
                    wolf.SetAnimationParameter("jump", true);
                    wolf.SetAnimationParameter("chase", false);
                }
                else runningTimer += Time.deltaTime;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (wolf.GroundCheck.Check())
            {
                if (!wolf.FOV.Check() /*|| noPlatform*/) stateMachine.ChangeState(wolf.LookForTargetState);
                else
                {
                    if (Time.time >= startTime + stateData.chaseTime && runningTimer >= 1) stateMachine.ChangeState(wolf.TiredState);
                    else if (wolf.AttackRange.Check()) stateMachine.ChangeState(wolf.AssaultState);
                }
            }
        }

        private void JumpGap()
        {
            // Using a boxCast instead of a raycast because a raycast will only check a single y position so if maxJumpDistance is too big it could potentialy skip
            // a platform. With boxCast, the cast will move through distance, detecting anything it comes in contact with.
            // Size is 0.01f in x because any bigger an it will detect the platform the wolf is currently standing in.
            Collider2D collider = Physics2D.BoxCast(wolf.LedgeCheck.position, new(0.01f, 50), 0, wolf.transform.right, stateData.maxJumpDistanceX, stateData.whatIsGround).collider;

            if (collider)
            {
                // Add .5f * wolf.FacingDirection to add an offset so the wolf doesn't land on the very edge
                Vector2 landPoint = new(collider.ClosestPoint(wolf.transform.position).x + .5f * wolf.FacingDirection, collider.bounds.max.y);

                // If there's ground under the platform the wolf is standing on and the ledge check doesn't detect it make sure the distance is big enough
                if (Mathf.Abs(landPoint.x - wolf.transform.position.x) < 2) return;

                float jumpAngle = Mathf.Deg2Rad * Random.Range(stateData.minJumpAngle, stateData.maxJumpAngle);
                float distance = Mathf.Abs(landPoint.x - wolf.transform.position.x);

                // Logic behind equation: https://www.desmos.com/calculator/f9cbk1m2ag
                Vector2 forceRequired = Mathf.Sqrt(distance * Physics2D.gravity.magnitude / Mathf.Sin(2*jumpAngle)) * new Vector2(Mathf.Cos(jumpAngle), Mathf.Sin(jumpAngle));

                wolf.SetVelocityX(forceRequired.x);
                wolf.SetVelocityY(forceRequired.y);
            }
            noPlatform = true;
        }

        private void JumpWall()
        {
            Collider2D collider = Physics2D.Raycast(wolf.WallCheck.position, wolf.transform.right, wolf.WallCheckDistance, stateData.whatIsGround).collider;

            if (collider) wolf.SetVelocityY(5);
            noPlatform = true;
        }
    }
}
