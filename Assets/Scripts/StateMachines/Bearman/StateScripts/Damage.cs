using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bearman.States
{
    [CreateAssetMenu(menuName = "States/Character/Damage")]
    public class Damage : State<BearmanCtrl>
    {
        private AnimationHandler _animationHandler;

        public override void Init(BearmanCtrl parent)
        {
            base.Init(parent);

            if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

            _animationHandler.SetParameter(BearmanCtrl.ReciveDamage);
        }

        public override void CaptureInput() { }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void ChangeState()
        {
            controller.SetState(typeof(Idle));
        }

        public override void Exit() { }
    }
}
