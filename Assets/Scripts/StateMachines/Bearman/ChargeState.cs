using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "States/Character/Charge")]
public class ChargeState : State<BearmanCtrl>
{
    private bool _charging;
    public float ChargeTime { get; private set; }
    public float ChargeToHeavyAttack { get; private set; }

    [SerializeField] private float _chargeToHeavyAttack = 1;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        ChargeTime = 0;
        ChargeToHeavyAttack = _chargeToHeavyAttack;
    }

    public override void CaptureInput()
    {
        _charging = Input.GetKey(KeyCode.Mouse0);
    }

    public override void Update()
    {
        controller.EventsHandler.InvokeChargeEvent(_charging);
        if (_charging) ChargeTime += Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public override void ChangeState()
    {
        if (!_charging)
        {
            if (ChargeTime < ChargeToHeavyAttack) controller.SetState(typeof(PunchState));
            else controller.SetState(typeof(ChargedPunchState));
        }
    }

    public override void Exit()
    {
        // throw new System.NotImplementedException();
    }
}
