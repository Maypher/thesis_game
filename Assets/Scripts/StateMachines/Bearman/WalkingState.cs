using UnityEngine;
using System;

[CreateAssetMenu(menuName ="States/Character/Walk")]
public class WalkingState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private BearmanAnimationHandler _animationHandler;

    [Header("Movement variables")]
    [SerializeField] private float maxSpeed = 5f;
    // When running multiply targetSpeed by this value
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private float acceleration = 5f;
    [Range(0, 1)] [SerializeField] private float lerpAmount = 0f; // Used to determine the rate of change from current speed to max speed

    // Variables
    private float _xDirection;
    private bool _running;

    // Used to trigger other states
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        // If the component has already been cached don't search for it again
        if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
        if (_groundCheck == null) _groundCheck = parent.GetComponentInChildren<GroundCheck>();
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _jump = false;
        _chargePunch = false;
        _running = false;
        _xDirection = 0;

        _animationHandler.WalkingAnimation(true);
    }

    public override void CaptureInput()
    {
        _xDirection = Input.GetAxisRaw("Horizontal");
        _running = Input.GetKey(KeyCode.LeftShift);
        _jump = Input.GetKeyDown(KeyCode.Space);
        _chargePunch = Input.GetKeyDown(KeyCode.Mouse0);
        _crouch = Input.GetKeyDown(KeyCode.LeftControl);
    }

    public override void ChangeState()
    {
        if (_groundCheck.Check())
        {
            if (_jump) controller.SetState(typeof(JumpState));
            else if (_rb.velocity.x == 0 && _xDirection == 0) controller.SetState(typeof(IdleState));
            else if (_crouch) controller.SetState(typeof(CrouchState));
            else if (_chargePunch) controller.SetState(typeof(ChargeState));
        }
    }
    
    public override void Update()
    {
        controller.AnimationHandler.CorrectRotation(_xDirection);
    }

    public override void FixedUpdate()
    {
        float targetSpeed = _xDirection * maxSpeed;

        // Set target speed to an interpolated value between the current speed and the target speed [a + (b – a) * t]
        targetSpeed = Mathf.Lerp(_rb.velocity.x, targetSpeed, lerpAmount);

        // If shift is pressed increase the speed
        if (_running)
        {
            targetSpeed *= runMultiplier;
        }

        // Get the difference between the target speed and the current speed
        float speedDiff = targetSpeed - _rb.velocity.x;

        // The actual force of movement is the speed difference multiplied by the acceleration constant
        float movement = speedDiff * acceleration;

        _rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public override void Exit() => _animationHandler.WalkingAnimation(false);
}
