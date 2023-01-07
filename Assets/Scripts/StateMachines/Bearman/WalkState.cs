using UnityEngine;
using System;

[CreateAssetMenu(menuName = "States/Character/Walk")]
public class WalkState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private BearmanAnimationHandler _animationHandler;

    // Variables
    private float _xDirection;
    private bool _running;
    private float _movingTime;
    private float _decelerationTime;

    // Used to trigger other states
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;
    private bool _aim;

    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _runMultiplier = 2f; // When running multiply targetSpeed by this value

    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private AnimationCurve _deceleration;

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
        _movingTime = 0;
        _decelerationTime = 0;
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
            else if (_xDirection == 0 && _rb.velocity == Vector2.zero) controller.SetState(typeof(IdleState));
            else if (_crouch) controller.SetState(typeof(CrouchState));
            else if (_chargePunch) controller.SetState(typeof(ChargeState));
            else if (_aim) controller.SetState(typeof(RaccoonAimState));
        }
    }

    public override void Update()
    {
        controller.AnimationHandler.CorrectRotation(_xDirection);
        _animationHandler.WalkingAnimation(_xDirection != 0);


        if (_xDirection != 0)
        {
            _movingTime += Time.deltaTime;
            _decelerationTime = 0;
        }
        else
        {
            _decelerationTime += Time.deltaTime;
            _movingTime = 0;
        }

    }

    public override void FixedUpdate()
    {
        if (_xDirection != 0) Accelerate();
        else Decelerate(); // If going back to idle state apply deceleration otherwise transition immediately
    }

    public override void Exit() 
    {
        _animationHandler.WalkingAnimation(false);
    }

    private void Accelerate()
    {
        float endTime = _acceleration[_acceleration.length - 1].time;
        float currentSpeed = _acceleration.Evaluate(Mathf.InverseLerp(0, endTime, _movingTime)) * _maxSpeed * _xDirection;

        if (_running) currentSpeed *= _runMultiplier;

        _rb.velocity = Vector2.right * currentSpeed;
    }

    private void Decelerate()
    {
        float currentSpeed = _deceleration.Evaluate(_decelerationTime) * _maxSpeed * Mathf.Sign(_rb.velocity.x);
        _rb.velocity = Vector2.right * currentSpeed;
    }
}
