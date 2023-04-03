using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bearman.States
{

    [CreateAssetMenu(menuName = "States/Character/Airborne")]
    public class Airborne : State<BearmanCtrl>
    {
        // Components
        private Rigidbody2D _rb;
        private AnimationHandler _animationHandler;

        // To allow time for the jump to happen
        // The frame the jump happens the character is technically airborne but the groundCheck is still true due to the radius
        // so it would trigger the transition immediately.
        private float _airTime;
        private bool _isAirborne;
        private float _coyoteTimer;
        private bool _canCoyoteJump;
        private bool _coyoteJumped;
        private bool _releasedJump;
        private float _jumpBufferTimer;

        private float _xDirection;
        private float _maxSpeedReached; // Used to keep the velocity of the previous state while not reaching ludicrous speeds
        private Vector2 _currentVelocity;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _jumpGravity = 1f; // When going up apply less gravity than when falling
        [SerializeField] private float _fallGravity = 2f; // When falling apply bigger gravity for faster fall
        [SerializeField] private float _maxFallSpeed = 10f;

        [Header("Peak of jump")]
        [SerializeField] [Min(0)] private float _jumpHangThreshold = .2f; // When _rb.velocity.y is inside this range reduce the gravity
        [SerializeField] private float _jumpHangGrav = .7f;

        [Header("Air movement")]
        [SerializeField] private float _moveSpeed = 4f;

        [Header("Misc")]
        [SerializeField] private float _coyoteTime = .4f;
        [SerializeField] private float _jumpBufferTime = .2f;

        public override void Init(BearmanCtrl parent)
        {
            base.Init(parent);

            if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
            if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

            _coyoteTimer = 0;
            _jumpBufferTimer = 0;
            _airTime = 0;
            _isAirborne = false;
            _coyoteJumped = false;
            _releasedJump = false;
            _maxSpeedReached = Mathf.Abs(_rb.velocity.x);

            _animationHandler.SetParameter(BearmanCtrl.IsAirborne, true);

            // Apply jump force immediately after entering state if the jump button was pressed in the previous state
            if (controller.Jumped) Jump(_jumpForce);
        }

        public override void CaptureInput()
        {
            _xDirection = controller.UserInput.Player.Move.ReadValue<float>();

            _canCoyoteJump = _coyoteTimer <= _coyoteTime && !controller.Jumped && !_coyoteJumped && controller.UserInput.Player.Jump.WasPerformedThisFrame();
            if (!_releasedJump) _releasedJump = controller.Jumped && controller.UserInput.Player.Jump.WasReleasedThisFrame();

            if (controller.UserInput.Player.Jump.WasPerformedThisFrame()) _jumpBufferTimer = _jumpBufferTime;
            else _jumpBufferTimer -= Time.deltaTime;
        }

        public override void Update()
        {
            #region Variable Update
            _airTime += Time.deltaTime;
            _coyoteTimer += Time.deltaTime;
            _jumpBufferTimer -= Time.deltaTime;
            _isAirborne = !controller.IsGrounded;
            #endregion

            #region Animations
            controller.AnimationHandler.SetParameter(BearmanCtrl.IsMoving, _xDirection != 0);
            #endregion

            #region Coyote Time
            // Give a window of error for jumping
            if (_canCoyoteJump)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                Jump(_jumpForce);
                _coyoteJumped = true;
                controller.Jumped = true;
            }
            #endregion

            #region Jump Buffer

            if (_jumpBufferTimer > 0 && !_isAirborne)
            {
                Jump(_jumpForce);
                _jumpBufferTimer = 0;
            }

            #endregion

            #region Gravity manipulation
            // Gives a window where at the peak of the jump gravity is reduced
            // using !_releasedJump because it shouldn't apply the gravity reduction if the character didn't reach max height
            if (Mathf.Abs(_rb.velocity.y) <= _jumpHangThreshold && !_releasedJump) _rb.gravityScale = _jumpHangGrav;
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
            if (_xDirection != 0) MoveVertically(_moveSpeed * _xDirection);
            #endregion
        }

        public override void FixedUpdate() { }

        public override void ChangeState()
        {
            if (!_isAirborne & _airTime > .3f)
            {
                if (_xDirection == 0) controller.SetState(typeof(Idle));
                else controller.SetState(typeof(Walk));
            }
        }

        public override void Exit()
        {
            _animationHandler.SetParameter(BearmanCtrl.IsAirborne, false);
            controller.Jumped = false;
        }

        private void MoveVertically(float targetSpeed)
        {
            _rb.AddForce(Vector2.right * targetSpeed, ForceMode2D.Force);
            _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -_maxSpeedReached, _maxSpeedReached), _rb.velocity.y);
        }

        private void Jump(float jumpForce) 
        {
            _currentVelocity = _rb.velocity;
            _currentVelocity.y = jumpForce;
            _rb.velocity = _currentVelocity;
        }

        private void SetGravity(float gravMultiplier) => _rb.gravityScale = gravMultiplier;
    }
}