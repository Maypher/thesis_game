using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "States/Character/Airborne")]
public class AirborneState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private BearmanAnimationHandler _animationHandler;

    // To allow time for the jump to happen
    // The frame the jump happens the character is technically airborne but the groundCheck is still true due to the radius
    // so it would trigger the transition immediately.
    private float _airTime;
    private bool _isAirborne;
    private float _coyoteTimer;
    private bool _canCoyoteJump;
    private bool _coyoteJumped;
    private bool _releasedJump;
    private bool _appliedMaxHeightForce;

    private float _xDirection;
    private float _maxSpeedReached; // Used to keep the velocity of the previous state while not reaching ludicrous speeds

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _jumpGravity = 1f; // When going up apply less gravity than when falling
    [SerializeField] private float _fallGravity = 2f; // When falling apply bigger gravity for faster fall
    [SerializeField] private float _maxFallSpeed = 10f;

    [Header("Peak of jump")]
    [SerializeField] [Min(0)] private float _jumpHangThreshold = .2f; // When _rb.velocity.y is inside this range reduce the gravity
    [SerializeField] private float _jumpHangGrav = .7f;
    [SerializeField] private float _jumpMaxHeightSpeed = 5f; // When at the peak of the jump apply a small speed boost

    [Header("Air movement")]
    [SerializeField] private float _maxMoveSpeed = 5f;
    [SerializeField] [Range(0, 1)] private float _lerpAmount = 1;

    [Header("Misc")]
    [SerializeField] private float _coyoteTime = .4f;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _coyoteTimer = 0;
        _airTime = 0;
        _isAirborne = false;
        _coyoteJumped = false;
        _releasedJump = false;
        _appliedMaxHeightForce = false;
        _maxSpeedReached = Mathf.Abs(_rb.velocity.x);

        _animationHandler.JumpAnimation(true);

        // Apply jump force immediately after entering state if the jump button was pressed in the previous state
        if (controller.jumped) Jump(_jumpForce);
    }

    public override void CaptureInput()
    {
       _xDirection = Input.GetAxisRaw("Horizontal");
        _canCoyoteJump = _coyoteTimer <= _coyoteTime && !controller.jumped && !_coyoteJumped && Input.GetKeyDown(KeyCode.Space);
        if (!_releasedJump) _releasedJump = controller.jumped && Input.GetKeyUp(KeyCode.Space);
    }

    public override void Update()
    {
        #region Variable Update
        _airTime += Time.deltaTime;
        _coyoteTimer += Time.deltaTime;
        _isAirborne = !controller.IsGrounded;
        #endregion

        #region Animations
        controller.AnimationHandler.IsMoving(_xDirection != 0);
        #endregion

        #region Coyote Time
        // Give a window of error for jumping
        if (_canCoyoteJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            Jump(_jumpForce);
            _coyoteJumped = true;
            controller.jumped = true;
        }
        #endregion

        #region Gravity manipulation
        // Gives a window where at the peak of the jump gravity is reduced
        // using !_releasedJump because it shouldn't apply the gravity reduction if the character didn't reach max height
        if (Mathf.Abs(_rb.velocity.y) <= _jumpHangThreshold && !_releasedJump) 
        {
            _rb.gravityScale = _jumpHangGrav;
            if ((_xDirection == 1) == controller.AnimationHandler.FacingRight && !_appliedMaxHeightForce && Mathf.Abs(_rb.velocity.x) > 1)
            {
                float targetSpeed = _rb.velocity.x + _jumpMaxHeightSpeed * _xDirection;

                if (Mathf.Abs(targetSpeed) > _maxSpeedReached) _maxSpeedReached = Mathf.Abs(targetSpeed);

                MoveVertically(targetSpeed, 1); // Apply a small speed boost at the peak of the jump
                _appliedMaxHeightForce = true;

            }
        }
        // If falling down apply greater gravity for faster fall
        else if (_rb.velocity.y < 0 || _releasedJump)
        {
            SetGravity(_fallGravity);
            // Clamp the fall velocity so when falling large distances it doesn't reach ludicrous speeds
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_maxFallSpeed));
        }
        else SetGravity(_jumpGravity);
        #endregion

        #region Movement
        if (_xDirection != 0)
        {
            ApplyForce(_maxMoveSpeed * _xDirection);
        }
        #endregion
    }

    public override void FixedUpdate() { }

    public override void ChangeState()
    {
        if (!_isAirborne & _airTime > .3f)
        {
            if (_xDirection == 0) controller.SetState(typeof(IdleState));
            else controller.SetState(typeof(WalkState));
        }
    }

    public override void Exit() => _animationHandler.JumpAnimation(false);

    // Interpolates between the current X velocity and the target velocity
    private void MoveVertically(float targetSpeed, float lerpAmount)
    {
        _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, targetSpeed, lerpAmount), _rb.velocity.y);
    }

    private void ApplyForce(float force) 
    {
        _rb.AddForce(Vector2.right * force * _lerpAmount, ForceMode2D.Impulse);
        _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -_maxSpeedReached, _maxSpeedReached), _rb.velocity.y);
    }

    private void Jump(float jumpForce) => _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

    private void SetGravity(float gravMultiplier) => _rb.gravityScale = gravMultiplier;
}
