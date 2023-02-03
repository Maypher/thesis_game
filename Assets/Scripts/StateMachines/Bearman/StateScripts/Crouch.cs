using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bearman.States
{
    [CreateAssetMenu(menuName = "States/Character/Crouch")]
    public class Crouch : State<BearmanCtrl>
    {
        private bool _isCrouching;
        private float _xDirection;

        private Collider2D _collider;
        private AnimationHandler _animationHandler;

        public override void Init(BearmanCtrl parent)
        {
            base.Init(parent);
            if (_collider == null) _collider = controller.GetComponent<CapsuleCollider2D>();
            if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

            _isCrouching = true;

            _animationHandler.SetParameter(AnimationHandler.Parameters.IsCrouching, true);
        }

        public override void CaptureInput()
        {
            _isCrouching = controller.UserInput.Player.Crouch.IsPressed();
            _xDirection = controller.UserInput.Player.Move.ReadValue<float>();
        }

        public override void Update() => _animationHandler.CorrectRotation(_xDirection);

        public override void ChangeState()
        {
            if (!_isCrouching) controller.SetState(typeof(Idle));
        }


        public override void FixedUpdate() { }
        public override void Exit()
        {
            _animationHandler.SetParameter(AnimationHandler.Parameters.IsCrouching, false);
        }

    }
}