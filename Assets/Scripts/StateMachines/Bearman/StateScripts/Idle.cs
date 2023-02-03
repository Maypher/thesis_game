using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bearman.States
{
    [CreateAssetMenu(menuName = "States/Character/Idle")]
    public class Idle : State<BearmanCtrl>
    {

        // transition variables
        private bool _moving;
        private bool _jump;
        private bool _chargePunch;
        private bool _crouch;
        private bool _aim;
        private bool _ShockwaveAttack;
        private bool _pickUpRock;

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
            _ShockwaveAttack = false;
            _pickUpRock = false;
        }

        public override void CaptureInput()
        {
            _moving = controller.UserInput.Player.Move.IsPressed();
            _jump = controller.UserInput.Player.Jump.IsPressed();
            _chargePunch = controller.UserInput.Player.Punch.WasPerformedThisFrame();
            _crouch = controller.UserInput.Player.Crouch.IsPressed();
            _aim = controller.UserInput.Player.RaccoonAim.IsPressed();
            _ShockwaveAttack = controller.UserInput.Player.ShockwaveAttack.IsPressed();
            _pickUpRock = controller.UserInput.Player.PickUpRock.IsPressed();
        }

        public override void Update()
        {
            if (controller.AnimationHandler.GetCurrentAnimation() != "BearmanFlex")
            {
                if (_idleTime > _timeToFlex)
                {
                    controller.AnimationHandler.SetParameter(BearmanCtrl.Flex);
                    _idleTime = 0;
                }
                else _idleTime += Time.deltaTime;
            }
        }

        public override void FixedUpdate() { }

        public override void ChangeState()
        {
            if (_jump)
            {
                controller.Jumped = true;
                controller.SetState(typeof(Airborne));
            }
            else if (!controller.IsGrounded) controller.SetState(typeof(Airborne));
            else if (_moving) controller.SetState(typeof(Walk));
            else if (_crouch) controller.SetState(typeof(Crouch));
            else if (_ShockwaveAttack) controller.SetState(typeof(ShockwaveAttack));
            else if (_chargePunch) controller.SetState(typeof(Punch));
            else if (_pickUpRock) controller.SetState(typeof(RockHeld));
            else if (_aim) controller.SetState(typeof(RaccoonAim));
        }

        public override void Exit() => _idleTime = 0;
    }
}
