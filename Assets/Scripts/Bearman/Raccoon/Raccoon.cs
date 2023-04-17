using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon
{
    public class Raccoon : StateMachine.Entity
    {
        private static Raccoon instance;

        public StateMachine.StateMachine<Raccoon> StateMachine { get; private set; } = new();

        private Player player = GameManager.Player;

        #region State Data
        [Header("Raccoon State Data")]
        [SerializeField] private States.Data.D_Launch launchData;
        #endregion

        #region States
        public States.Launch LaunchState { get; private set; }
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
        }

        public override void Start()
        {
            base.Start();

            StateMachine.Initialize(LaunchState);
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}
