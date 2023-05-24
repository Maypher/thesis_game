using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon
{
    // Not really an enemy but it requires everything an enemy does so fight me (-(-_-(-_(-_(-_-)_-)-_-)_-)_-)-)
    public class Raccoon : Enemies.EnemyEntity
    {
        private static Raccoon instance;

        public StateMachine.StateMachine<Raccoon> StateMachine { get; private set; } = new();

        #region Components
        public AttackCheck PlayerCheck { get; private set; }
        public SpriteRenderer Sprite { get; private set; }
        
        [field: SerializeField] [field: Header("SFX")] public AudioSource AudioSource { get; private set; }
        #endregion

        #region State Data
        [Header("Raccoon State Data")]
        [SerializeField] private States.Data.D_Launch launchData;
        [SerializeField] private States.Data.D_ReturnToPlayer returnToPlayerData;
        [SerializeField] private States.Data.D_JumpToPlayer jumpToPlayerData;
        #endregion

        #region States
        public States.Launch LaunchState { get; private set; }
        public States.ReturnToPlayer ReturnToPlayerState { get; private set; }
        public States.JumpToPlayer JumpToPlayerState { get; private set; }
        #endregion

        public override void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            LaunchState = new(this, StateMachine, launchData);
            ReturnToPlayerState = new(this, StateMachine, returnToPlayerData);
            JumpToPlayerState = new(this, StateMachine, jumpToPlayerData);
        }

        public override void Start()
        {
            base.Start();

            PlayerCheck = transform.Find("PlayerCheck").GetComponent<AttackCheck>();
            Sprite = GetComponent<SpriteRenderer>();

            StateMachine.Initialize(LaunchState);
        }

        public override void Update()
        {
            base.Update();

            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.CheckStateChange();
        }

        public override void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnBecameInvisible()
        {
            // Only disable the raccoon if it goes out of view while being launched because there are cases where
            // the raccoon might go out of view because of the player's movement
            if (StateMachine.CurrentState == LaunchState) gameObject.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (StateMachine.CurrentState == ReturnToPlayerState && other.CompareTag("Player"))
            {
                if (ReturnToPlayerState.DestroyRaccoon) Destroy(gameObject);
                else
                {
                    Sprite.enabled = false;
                }
            }
        }
    }
}
