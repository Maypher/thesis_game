using UnityEngine;
using System;

[CreateAssetMenu(menuName = "States/Character/Walk")]
public class WalkState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private BearmanAnimationHandler _animationHandler;

    // Variables
    private float _xDirection;
    private bool _running;
    private float _movingTime;
    private float _decelerationTime;
    // Used to allow a change in direction without losing speed. If the player released left before pressing right
    // the deceleration phase would kick in immediately
    private float _noInputTime;

    // Used to trigger other states
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;
    private bool _aim;

    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _runMultiplier = 2f; // When running multiply targetSpeed by this value
    [SerializeField] private float _timeToMaxSpeed = 1f;
    [SerializeField] private float _timeToFullStop = 1f;
    [SerializeField] private float _directionChangeThreshold = .2f; // Used alongside _noInputTime

    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private AnimationCurve _deceleration;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        // If the component has already been cached don't search for it again
        if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _jump = false;
        _chargePunch = false;
        _running = false;
        _xDirection = 0;
        _movingTime = Mathf.Abs(_rb.velocity.x) / _maxSpeed;
        _decelerationTime = 0;
        _noInputTime = 0;
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
        if (controller.IsGrounded)
        {
            if (_jump) 
            {
                controller.jumped = true;
                controller.SetState(typeof(AirborneState)); 
            }
            else if (_xDirection == 0 && _rb.velocity == Vector2.zero) controller.SetState(typeof(IdleState));
            else if (_crouch) controller.SetState(typeof(CrouchState));
            else if (_chargePunch) controller.SetState(typeof(ChargeState));
            else if (_aim) controller.SetState(typeof(RaccoonAimState));
        }
        else controller.SetState(typeof(AirborneState));
    }

    public override void Update()
    {
        controller.AnimationHandler.CorrectRotation(_xDirection);
        _animationHandler.IsMoving(_xDirection != 0);

        if (_xDirection == 0) _noInputTime += Time.deltaTime;
        else _noInputTime = 0;


        if (_xDirection != 0)
        {
            _movingTime += Time.deltaTime / _timeToMaxSpeed;
            _decelerationTime = Mathf.Max(0, 1 - _movingTime); // Used to transition between _moving and slowing down in the animation curve
            Accelerate();
        }
        else if (_noInputTime > _directionChangeThreshold)
        {
            _decelerationTime += Time.deltaTime / _timeToFullStop;
            _movingTime = _decelerationTime;
            Decelerate();
        }

    }

    public override void FixedUpdate() {}

    public override void Exit() 
    {
        _animationHandler.IsMoving(false);
    }

    private void Accelerate()
    {
        float endTime = _acceleration[_acceleration.length - 1].time;
        float currentSpeed = _acceleration.Evaluate(Mathf.InverseLerp(0, endTime, _movingTime)) * _maxSpeed * _xDirection;

        if (_running) currentSpeed *= _runMultiplier;

        _rb.velocity = new Vector2(currentSpeed, _rb.velocity.y);
    }

    private void Decelerate()
    {
        float currentSpeed = _deceleration.Evaluate(_decelerationTime) * _maxSpeed * Mathf.Sign(_rb.velocity.x);
        _rb.velocity = new Vector2(currentSpeed, _rb.velocity.y);
    }
}
