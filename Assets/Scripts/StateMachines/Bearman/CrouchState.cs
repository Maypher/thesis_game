using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Crouch")]
public class CrouchState : State<BearmanCtrl>
{
    private bool _isCrouching;
    private bool _pickUpRock;

    private CapsuleCollider2D _collider;
    private BearmanAnimator _animator;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);
        if (_collider == null) _collider = controller.GetComponent<CapsuleCollider2D>();
        if (_animator == null) _animator = controller.Animator;

        _isCrouching = true;
        _pickUpRock = false;
    }

    public override void CaptureInput() 
    {
        _isCrouching = Input.GetKey(KeyCode.LeftControl);
        _pickUpRock = Input.GetKeyDown(KeyCode.Mouse0);
    }

    public override void Update()
    {
        _animator.CrouchAnimation(_isCrouching);
    }

    public override void ChangeState()
    {
        if (!_isCrouching) controller.SetState(typeof(IdleState));
        else if (_pickUpRock) controller.SetState(typeof(RockState));
    }


    public override void FixedUpdate() {}
    public override void Exit()
    {
        _collider.size = new Vector2(_collider.size.x, _collider.size.y * 2);
        _animator.CrouchAnimation(false);
    }

}
