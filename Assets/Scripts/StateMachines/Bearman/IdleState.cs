using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Idle")]
public class IdleState : State<BearmanCtrl>
{

    // transition variables
    private float _xDirection;
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;
    private bool _aim;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        controller.idleTime = 0;
        _jump = false;
        _chargePunch = false;
        _xDirection = 0;
        _crouch = false;
    }

    public override void CaptureInput()
    {
        _xDirection = Input.GetAxisRaw("Horizontal");
        _jump = Input.GetKeyDown(KeyCode.Space);
        _chargePunch = Input.GetKeyDown(KeyCode.Mouse0);
        _crouch = Input.GetKeyDown(KeyCode.LeftControl);
        _aim = Input.GetKeyDown(KeyCode.Mouse1);
    }

    public override void Update()
    {
        if (controller.idleTime > controller.timeToFlex) controller.idleTime = 0;
        else controller.idleTime += Time.deltaTime;
    }

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        if (_jump) controller.SetState(typeof(JumpState));
        else if (_xDirection != 0) controller.SetState(typeof(WalkingState));
        else if (_crouch) controller.SetState(typeof(CrouchState));
        else if (_chargePunch) controller.SetState(typeof(ChargeState));
        else if (_aim) controller.SetState(typeof(RaccoonAimState));
    }

    public override void Exit() => controller.idleTime = 0;
}
