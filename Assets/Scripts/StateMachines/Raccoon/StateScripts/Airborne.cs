using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raccoon.States
{
    [CreateAssetMenu(menuName = "States/Raccoon/Airborne")]
    public class Airborne : State<RaccoonController>
    {
        private Rigidbody2D _rb;
        private GroundCheck _groundCheck;

        public override void Init(RaccoonController parent)
        {
            base.Init(parent);

            if (_rb == null) _rb = controller.GetComponent<Rigidbody2D>();
            if (_groundCheck == null) _groundCheck = controller.GetComponentInChildren<GroundCheck>();
        }

        public override void CaptureInput() { }

        public override void Update()
        {
            controller.transform.right = _rb.velocity;
        }

        public override void FixedUpdate() { }

        public override void ChangeState()
        {
            if (_groundCheck.Check()) controller.SetState(typeof(ReturnToOwner));
        }

        public override void Exit()
        {
            controller.transform.rotation = Quaternion.identity;
        }

    }
}