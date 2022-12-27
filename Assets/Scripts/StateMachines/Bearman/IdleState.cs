using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Idle")]
public class IdleState : State<BearmanCtrl>
{

    // variables
    [SerializeField] private float _timeToFlex = 10f; // How much time has to pass before playing flex animation
    private float _idleTime; // To time flex animation
    private float _xDirection;
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        _idleTime = 0;
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
    }

    public override void Update()
    {
        if (_idleTime > _timeToFlex) _idleTime = 0;
        else _idleTime += Time.deltaTime;
    }

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        if (_jump) controller.SetState(typeof(JumpState));
        else if (_xDirection != 0) controller.SetState(typeof(WalkingState));
        else if (_crouch) controller.SetState(typeof(CrouchState));
        else if (_chargePunch) controller.SetState(typeof(ChargeState));
    }

    public override void Exit() {}
}
