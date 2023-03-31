using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Entity
{
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    public Wolf_IdleState IdleState { get; private set; }
    public Wolf_MoveState MoveState { get; private set; }


    public override void Start()
    {
        base.Start();

        MoveState = new Wolf_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new Wolf_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(MoveState);
    }
}