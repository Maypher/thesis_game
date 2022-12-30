using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "States/Character/Charge")]
public class ChargeState : State<BearmanCtrl>
{
    private BearmanAnimationHandler _animationHandler;

    private bool _charging;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);
        _animationHandler = controller.AnimationHandler;

        controller.chargeTime = 0;
        _animationHandler.ChargeAnimation(true);
    }

    public override void CaptureInput()
    {
        _charging = Input.GetKey(KeyCode.Mouse0);
    }

    public override void Update()
    {
        if (_charging) controller.chargeTime += Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public override void ChangeState()
    {
        if (!_charging)
        {
            if (controller.chargeTime < controller.chargeToHeavyAttack) controller.SetState(typeof(PunchState));
            else controller.SetState(typeof(ChargedPunchState));
        }
    }

    public override void Exit() => _animationHandler.ChargeAnimation(false);
}
