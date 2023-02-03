using UnityEngine;
using System;
using UnityEngine.InputSystem;


namespace Bearman.States
{
    [CreateAssetMenu(menuName = "States/Character/Walk")]
    public class Walk : State<BearmanCtrl>
    {
        // Components
        private Rigidbody2D _rb;
        private AnimationHandler _animationHandler;

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
        private bool _ShockwaveAttack;
        private bool _pickUpRock;

        private bool _shouldStopBeforeChange; // Used for transitions

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
            _ShockwaveAttack = false;
            _pickUpRock = false;

            _xDirection = 0;
            _movingTime = Mathf.Abs(_rb.velocity.x) / _maxSpeed;
            _decelerationTime = 0;
            _noInputTime = 0;
            _shouldStopBeforeChange = false;
        }

        public override void CaptureInput()
        {
            _xDirection = controller.UserInput.Player.Move.ReadValue<float>();

            // Used for transitions
            _running = controller.UserInput.Player.Run.IsPressed();
            _jump = controller.UserInput.Player.Jump.WasPerformedThisFrame();
            _chargePunch = controller.UserInput.Player.Punch.WasPerformedThisFrame();
            _crouch = controller.UserInput.Player.Crouch.IsPressed();
            _aim = controller.UserInput.Player.RaccoonAim.IsPressed();
            _ShockwaveAttack = controller.UserInput.Player.ShockwaveAttack.IsPressed();
            _pickUpRock = controller.UserInput.Player.PickUpRock.IsPressed();
        }


        public override void Update()
        {
            controller.AnimationHandler.CorrectRotation(_xDirection);
            _animationHandler.SetParameter(AnimationHandler.Parameters.IsMoving, _xDirection != 0);

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

        public override void ChangeState()
        {
            if (controller.IsGrounded)
            {
                if (_jump)
                {
                    controller.Jumped = true;
                    controller.SetState(typeof(Airborne));
                }
                // This states will wait for the character to fully stop before transitioning
                else if (_rb.velocity == Vector2.zero)
                {
                    if (_xDirection == 0) controller.SetState(typeof(Idle));
                }

                else // This states will ignore velocity and stop the player in place
                {
                    _shouldStopBeforeChange = _crouch || _ShockwaveAttack || _chargePunch || _aim;

                    if (_crouch) controller.SetState(typeof(Crouch));
                    if (_ShockwaveAttack) controller.SetState(typeof(ShockwaveAttack));
                    else if (_chargePunch) controller.SetState(typeof(Punch));
                    else if (_pickUpRock) controller.SetState(typeof(RockHeld));
                    else if (_aim) controller.SetState(typeof(RaccoonAim));
                }
            }
            else controller.SetState(typeof(Airborne));
        }

        public override void FixedUpdate() { }

        public override void Exit()
        {
            _animationHandler.SetParameter(AnimationHandler.Parameters.IsMoving, false);
            if (_shouldStopBeforeChange) _rb.velocity = Vector2.zero;
        }

        private void Accelerate()
        {
            _movingTime += Time.deltaTime;
            _decelerationTime = Mathf.Max(0, 1 - _movingTime);

            float endTime = _acceleration[_acceleration.length - 1].time;
            float currentSpeed = _acceleration.Evaluate(Mathf.InverseLerp(0, endTime, _movingTime)) * _maxSpeed * _xDirection;

            if (_running) currentSpeed *= _runMultiplier;

            _rb.velocity = new Vector2(currentSpeed, _rb.velocity.y);
        }

        private void Decelerate()
        {
            _decelerationTime += Time.deltaTime / _timeToFullStop;
            _movingTime = _decelerationTime;

            float currentSpeed = _deceleration.Evaluate(_decelerationTime) * _maxSpeed * Mathf.Sign(_rb.velocity.x);
            _rb.velocity = new Vector2(currentSpeed, _rb.velocity.y);
        }
    }
}
