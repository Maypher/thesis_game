using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    private Vector2 workspace;


    [SerializeField] private PlayerData playerData;

    public PlayerIdle playerIdleState { get; private set; }

    public PlayerMove playerMoveState { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        Rb = GetComponent<Rigidbody2D>();

        playerIdleState = new PlayerIdle(this, StateMachine, playerData, "idle");
        playerIdleState = new PlayerIdle(this, StateMachine, playerData, "move");
    }

    private void Start()
    {
        StateMachine.Initialize(playerIdleState);
    }

    private void Update()
    {
        StateMachine.currentState.LogicUpdate();

        CurrentVelocity = Rb.velocity;
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
}
