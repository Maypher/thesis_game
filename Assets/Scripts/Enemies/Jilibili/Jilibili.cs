using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili {
    public class Jilibili : EnemyEntity
    {
        public StateMachine.StateMachine<Jilibili> StateMachine { get; private set; } = new();

        [Header("Attack details")]
        [SerializeField] private AttackDetails attackDetails;

        public Action enteredBush;
        public Vector2 StartPos { get; private set; }

        #region Components
        /// <summary>
        /// Used for when the player gets close
        /// </summary>
        public AttackCheck AwarenessZone { get; private set; }
        /// <summary>
        /// Used when inside the bush to detect the player
        /// </summary>
        public AttackCheck DetectionZone { get; private set; }
        public GameObject Bush { get; private set; }

        public SpriteRenderer SpriteRenderer { get; private set; }
        public Collider2D Collider { get; private set; }
        #endregion

        #region State Data
        [Header("State Data")]
        [SerializeField] private States.Data.D_InsideBush insideBushData;
        [SerializeField] private States.Data.D_Idle idleData;
        [SerializeField] private States.Data.D_Attack attackData;
        [SerializeField] private States.Data.D_JumpForward jumpForwardData;
        [SerializeField] private States.Data.D_Retreat retreatData;
        [SerializeField] private States.Data.D_ReturnToBush returnToBushData;
        #endregion

        #region States
        public States.InsideBush InsideBushState { get; private set; }
        public States.Idle IdleState { get; private set; }
        public States.Retreat RetreatState { get; private set; }
        public States.Assault AssaultState { get; private set; }
        public States.JumpForward JumpForwardState { get; private set; }
        public States.ReturnToBush ReturnToBushState { get; private set; }
        #endregion

        public override void Awake()
        {
            base.Awake();

            InsideBushState = new(this, StateMachine, insideBushData);
            IdleState = new(this, StateMachine, idleData);
            RetreatState = new(this, StateMachine, retreatData);
            AssaultState = new(this, StateMachine, attackData);
            JumpForwardState = new(this, StateMachine, jumpForwardData);
            ReturnToBushState = new(this, StateMachine, returnToBushData);
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();

            StartPos = transform.position;

            AwarenessZone = transform.Find("JumpAwayArea").GetComponent<AttackCheck>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Collider = GetComponent<Collider2D>();
            Bush = transform.parent.transform.Find("bush").gameObject;
            DetectionZone = Bush.transform.Find("detectionZone").GetComponent<AttackCheck>();

            StateMachine.Initialize(InsideBushState);
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.CheckStateChange();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) 
            {
                attackDetails.attackPostion = collision.GetContact(0).point;
                collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(attackDetails);
            }
        }

        private void OnTriggerStay2D (Collider2D collision)
        {
            if (collision.gameObject == Bush) enteredBush?.Invoke();
        }
    }
}
