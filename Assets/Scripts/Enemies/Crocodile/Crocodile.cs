using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies.Crocodile
{
    public class Crocodile : StateMachine.Entity
    {
        public StateMachine.StateMachine<Crocodile> StateMachine { get; private set; } = new();

        #region state data
        [SerializeField] private States.Data.D_Idle idleData;
        [SerializeField] private States.Data.D_Bite biteData;
        [SerializeField] private States.Data.D_Hide hideData;
        #endregion

        #region states
        public States.Idle IdleState { get; private set; }
        public States.Bite BiteState { get; private set; }
        public States.Hide HideState { get; private set; }
        #endregion

        public AttackCheck AttackCheck { get; private set; }
        public Vector2 StartPosition { get; private set; }
        public Transform Sprite { get; private set; }
        public Collider2D Ground { get; private set; }

        public override void Awake()
        {
            IdleState = new(this, StateMachine, idleData);
            BiteState = new(this, StateMachine, biteData);
            HideState = new(this, StateMachine, hideData);
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();

            AttackCheck = GetComponentInChildren<AttackCheck>();
            StartPosition = transform.position;
            Sprite = transform.Find("Sprite");
            Ground = transform.Find("Ground").GetComponent<Collider2D>();

            StartPosition = transform.position;

            StateMachine.Initialize(IdleState);
        }

        public override void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.CheckStateChange();
        }
    }
}