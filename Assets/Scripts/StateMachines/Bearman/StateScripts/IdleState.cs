using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Idle")]
public class IdleState : State<BearmanCtrl>
{

    // transition variables
    private bool _moving;
    private bool _jump;
    private bool _chargePunch;
    private bool _crouch;
    private bool _aim;
    private bool _shockwave;

    [SerializeField] private float _timeToFlex = 10f; // How much time has to pass before playing flex animation
    private float _idleTime; // To time flex animation

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        _idleTime = 0;
        _jump = false;
        _chargePunch = false;
        _moving = false;
        _crouch = false;
        _shockwave = false;
    }

    public override void CaptureInput()
    {
        _moving = controller.UserInput.Player.Move.IsPressed();
        _jump = controller.UserInput.Player.Jump.IsPressed();
        _chargePunch = controller.UserInput.Player.Punch.IsPressed();
        _crouch = controller.UserInput.Player.Crouch.IsPressed();
        _aim = controller.UserInput.Player.RaccoonAim.IsPressed();
        _shockwave = controller.UserInput.Player.Shockwave.IsPressed();
    }

    public override void Update()
    {
        if (controller.AnimationHandler.GetCurrentAnimation() != "BearmanFlex")
        {
            if (_idleTime > _timeToFlex)
            {
                controller.AnimationHandler.FlexAnimation();
                _idleTime = 0;
            }
            else _idleTime += Time.deltaTime;
        }
    }

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        if (_jump)
        {
            controller.Jumped = true;
            controller.SetState(typeof(AirborneState));
        }
        else if (!controller.IsGrounded) controller.SetState(typeof(AirborneState));
        else if (_moving) controller.SetState(typeof(WalkState));
        else if (_crouch) controller.SetState(typeof(CrouchState));
        else if (_shockwave) controller.SetState(typeof(ShockwaveState));
        else if (_chargePunch) controller.SetState(typeof(PunchState));
        else if (_aim) controller.SetState(typeof(RaccoonAimState));
    }

    public override void Exit() => _idleTime = 0;
}
