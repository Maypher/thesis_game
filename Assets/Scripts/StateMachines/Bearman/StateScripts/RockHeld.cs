using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bearman.States
{
    [CreateAssetMenu(menuName = "States/Character/RockHeld")]
    public class RockHeld : State<BearmanCtrl>
    {
        private Rigidbody2D _rb;
        private AnimationHandler _animationHandler;
        private Transform _rockSpawnPos;

        [Header("Movement")]
        [SerializeField] private AnimationCurve _acceleration;
        [SerializeField] private AnimationCurve _deceleration;
        [SerializeField] private float _maxSpeed = 5;
        [SerializeField] private float _timeToMaxSpeed = 1;
        [SerializeField] private float _timeToFullStop = 1;

        [Header("Shield")]
        [SerializeField] private GameObject _rockPrefab;

        [Header("Thrown")]
        [SerializeField] private Vector2 _throwForce;
        [SerializeField] private float _torque = -20;

        private GameObject _rock;

        private float _xDirection;
        private float _walkDirection;
        private bool _throw;
        private float _walkTime;
        private float _slowDownTime;
        private bool _holdingRock;

        public override void Init(BearmanCtrl parent)
        {
            base.Init(parent);

            if (_rb == null) _rb = controller.GetComponent<Rigidbody2D>();
            if (_animationHandler == null) _animationHandler = controller.AnimationHandler;
            if (_rockSpawnPos == null) _rockSpawnPos = controller.transform.Find("RockSpawnPos");

            _throw = false;
            _walkTime = 0;
            _slowDownTime = 1; // Start at the end (no speed)
            _walkDirection = controller.AnimationHandler.FacingDirection;
            _holdingRock = false;

            controller.AnimationEvent += InstantiateRock;

            _animationHandler.SetParameter(BearmanCtrl.PickUpRock);
        }

        public override void CaptureInput()
        {
            _xDirection = controller.UserInput.Player.Move.ReadValue<float>();
            _throw = controller.UserInput.Player.Throw.WasPerformedThisFrame();
        }

        public override void Update()
        {
            _animationHandler.SetParameter(BearmanCtrl.IsMoving, _rb.velocity.x != 0);

            // Only move the character in the direction it was facing when state was entered
            if (_xDirection == _walkDirection && _holdingRock) Accelerate();
            else Decelerate();
        }

        public override void FixedUpdate()
        {
        }

        public override void ChangeState()
        {
            if (_throw) controller.SetState(typeof(Idle));
        }

        public override void Exit()
        {
            _animationHandler.SetParameter(BearmanCtrl.HoldingRock, false);

            controller.AnimationEvent -= InstantiateRock;

            if (_throw) ThrowRock(new Vector2(_throwForce.x * _walkDirection, _throwForce.y), _torque);
            else ThrowRock(Vector2.zero, 0);
        }

        private void Accelerate()
        {
            _walkTime += Time.deltaTime / _timeToMaxSpeed;
            _slowDownTime = _walkTime;

            _rb.velocity = new Vector2(_acceleration.Evaluate(_walkTime) * _maxSpeed * _walkDirection, _rb.velocity.y);
        }

        private void Decelerate()
        {
            _slowDownTime += Time.deltaTime / _timeToFullStop;
            _walkTime = _slowDownTime;

            _rb.velocity = new Vector2(_deceleration.Evaluate(_slowDownTime) * _maxSpeed * _walkDirection, _rb.velocity.y);
        }

        private void ThrowRock(Vector2 throwForce, float torque)
        {
            _rock.transform.SetParent(null, true);

            Rock.RockController script = _rock.GetComponent<Rock.RockController>();
            script.IgnoreCollisionWith(controller.GetComponent<Collider2D>());
            script.ThrowForce = throwForce;
            script.Torque = torque;
            script.Thrown = true;
        }

        private void InstantiateRock()
        {
            _rock = Instantiate(_rockPrefab, _rockSpawnPos.position, _rockSpawnPos.rotation, _rockSpawnPos);
            _animationHandler.SetParameter(BearmanCtrl.HoldingRock, true);
            _holdingRock = true;
        }
    }
}