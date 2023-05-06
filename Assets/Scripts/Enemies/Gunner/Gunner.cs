using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies.Gunner
{
    public class Gunner : StateMachine.Entity
    {
        public StateMachine.StateMachine<Gunner> StateMachine { get; private set; } = new();
        public FieldOfView FOV { get; private set; }

        #region State Data
        [SerializeField] private States.Data.D_Idle idleData;
        [SerializeField] private States.Data.D_Shoot shootData;
        #endregion

        #region States
        public States.Idle IdleState { get; private set; }
        public States.Shoot ShootState { get; private set; }
        #endregion

        public Transform Gun { get; private set; }

        public override void Awake()
        {
            base.Awake();

            IdleState = new(this, StateMachine, idleData);
            ShootState = new(this, StateMachine, shootData);
        }

        public override void Start()
        {
            base.Start();

            FOV = GetComponentInChildren<FieldOfView>();
            Gun = transform.Find("Gun");

            StateMachine.Initialize(IdleState);
        }

        public override void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.CheckStateChange();

            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}