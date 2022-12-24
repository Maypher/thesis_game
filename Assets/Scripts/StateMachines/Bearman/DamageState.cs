using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Damage")]
public class DamageState : State<BearmanCtrl>
{
    private BearmanAnimator _animator;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_animator == null) _animator = controller.Animator;

        _animator.Damaged();
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
