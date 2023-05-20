using StateMachine;
using System.Collections;
using UnityEngine;


namespace Player.Raccoon.States
{
    public class ReturnToPlayer : RaccoonState
    {
        private readonly Data.D_ReturnToPlayer stateData;

        private readonly Collider2D playerCollider = GameManager.Player.GetComponent<Collider2D>();

        private int directionToPlayer;

        private GameObject treeBranch;
        private Vector2 treeInitialPos;

        private bool treeMovementFinished;

        public bool DestroyRaccoon { get; private set; }

        public ReturnToPlayer(Raccoon entity, StateMachine<Raccoon> stateMachine, Data.D_ReturnToPlayer stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            directionToPlayer = (int) Mathf.Sign((GameManager.Player.transform.localPosition - raccoon.transform.position).x);
            raccoon.transform.position = GetOutsideCameraViewPosition(directionToPlayer, stateData.screenSpawnInset);

            raccoon.SetVelocity(0, Vector2.zero, 0);
            raccoon.Sprite.enabled = false;

            treeBranch = GameObject.Instantiate(stateData.treeBranch, GetOutsideCameraViewPosition(directionToPlayer, stateData.treeScreenSpawnInset), Quaternion.Euler(new(0, 0, 180)));
            treeInitialPos = treeBranch.transform.position;

            treeMovementFinished = false;

            raccoon.StartCoroutine(MoveTreeCoroutine(treeInitialPos, treeInitialPos + stateData.maxTreeMovement, stateData.goDownduration, stateData.goDownEasing));
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            directionToPlayer = (int) Mathf.Sign((GameManager.Player.transform.localPosition - raccoon.transform.position).x);

            if (directionToPlayer != raccoon.FacingDirection) raccoon.Flip();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (treeMovementFinished) raccoon.transform.position = Vector3.MoveTowards(raccoon.transform.position, new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.max.y), stateData.maxDistanceDelta);
        }

        // Used after being enabled to place the raccoon outside the camera view and place it on the ground
        private Vector2 GetOutsideCameraViewPosition(float directionToPlayer, Vector2 inset)
        {
            float spawnX = Camera.main.ViewportToWorldPoint(directionToPlayer == -1 ? Vector3.right : Vector3.zero).x + inset.x * directionToPlayer;
            float spawnY = Camera.main.ViewportToWorldPoint(Vector3.one).y + inset.y;

            return new(spawnX, spawnY);
        }

        private IEnumerator MoveTreeCoroutine(Vector2 initialPos, Vector2 endPos, float duration, EasingType easing)
        {
            float elapsedTime = 0f;

            while (elapsedTime <= duration)
            {
                // Calculate the normalized time value based on the elapsed time and duration
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                // Calculate the eased position using the easeInOutElastic function
                var easedValue = easing switch
                {
                    EasingType.EaseInBounce => EaseInBounce(normalizedTime),
                    EasingType.EaseOutBounce => EaseOutBounce(normalizedTime),
                    EasingType.EaseInBack => EaseInBack(normalizedTime),
                    _ => 1f,
                };

                // Update the object's position
                Vector2 easedPosition = initialPos + (endPos - initialPos) * easedValue;
                treeBranch.transform.position = easedPosition;

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // Movement completed, reset variables or perform any required actions
            treeBranch.transform.position = endPos;

            if (treeMovementFinished) { GameObject.Destroy(treeBranch); DestroyRaccoon = true; }
            else
            {
                treeMovementFinished = true;
                raccoon.Sprite.enabled = true;
                raccoon.transform.position = treeBranch.transform.Find("RaccoonJumpPos").transform.position;

                raccoon.StartCoroutine(MoveTreeCoroutine(endPos, treeInitialPos, stateData.goUpDuration, stateData.goUpEasing));
            }
        }

        public float EaseInBounce(float x)
        {
            return 1 - EaseOutBounce(1 - x);
        }

        private float EaseOutBounce(float x)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (x < 1f / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2f / d1)
            {
                x -= 1.5f / d1;
                return n1 * x * x + 0.75f;
            }
            else if (x < 2.5f / d1)
            {
                x -= 2.25f / d1;
                return n1 * x * x + 0.9375f;
            }
            else
            {
                x -= 2.625f / d1;
                return n1 * x * x + 0.984375f;
            }
        }

        private float EaseInBack(float x) 
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1;

            return c3* x * x* x - c1* x * x;
        }
    }
    
    public enum EasingType
    {
        EaseInBounce,
        EaseOutBounce,
        EaseInBack
    }
}
