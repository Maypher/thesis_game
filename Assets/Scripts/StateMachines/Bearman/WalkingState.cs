using UnityEngine;
using System;

[CreateAssetMenu(menuName ="States/Character/Walk")]
public class WalkingState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private BearmanAnimationHandler _animationHandler;

    // Variables
    private float _xDirection;
    private bool _running;

    // Used to trigger other states
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;
    private bool _aim;

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
        _aim = Input.GetKeyDown(KeyCode.Mouse1);
    }

    public override void ChangeState()
    {
        if (_groundCheck.Check())
        {
            if (_jump) controller.SetState(typeof(JumpState));
            else if (_rb.velocity.x == 0 && _xDirection == 0) controller.SetState(typeof(IdleState));
            else if (_crouch) controller.SetState(typeof(CrouchState));
            else if (_chargePunch) controller.SetState(typeof(ChargeState));
            else if (_aim) controller.SetState(typeof(RaccoonAimState));
        }
    }
    
    public override void Update()
    {
        controller.AnimationHandler.CorrectRotation(_xDirection);
    }

    public override void FixedUpdate()
    {
        float targetSpeed = _xDirection * controller.maxSpeed;

        // Set target speed to an interpolated value between the current speed and the target speed [a + (b – a) * t]
        targetSpeed = Mathf.Lerp(_rb.velocity.x, targetSpeed, controller.lerpAmount);

        // If shift is pressed increase the speed
        if (_running)
        {
            targetSpeed *= controller.runMultiplier;
        }

        // Get the difference between the target speed and the current speed
        float speedDiff = targetSpeed - _rb.velocity.x;

        // The actual force of movement is the speed difference multiplied by the acceleration constant
        float movement = speedDiff * controller.acceleration;

        _rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public override void Exit() => _animationHandler.WalkingAnimation(false);
}
