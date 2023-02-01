using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Damage")]
public class DamageState : State<BearmanCtrl>
{
    private BearmanAnimationHandler _animationHandler;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _animationHandler.SetParameter(BearmanAnimationHandler.Parameters.TakeDamage);
    }

    public override void CaptureInput() {}

    public override void Update() {}

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        controller.SetState(typeof(IdleState));
    }

    public override void Exit() {}
}
