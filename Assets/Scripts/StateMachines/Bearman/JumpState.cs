using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "States/Character/Jump")]
public class JumpState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private BearmanAnimationHandler _animationHandler;

    // To allow time for the jump to happen
    // The frame the jump happens the character is technically airborne but the groundCheck is still true due to the radius
    // so it would trigger the transition immediately.
    private float _jumpTime;
    private bool _isAirborne;

    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _jumpGravity = 1f; // When going up apply less gravity than when falling

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _jumpTime = 0;
        _isAirborne = false;
        _animationHandler.JumpAnimation(true);

        // Apply jump force immediately after entering state
        _rb.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public override void CaptureInput() {}

    public override void Update()
    {
        _jumpTime += Time.deltaTime;

        _isAirborne = !controller.IsGrounded;

        if (_rb.velocity.y < 0) _rb.gravityScale = controller.fallGravity;
        else _rb.gravityScale = _jumpGravity;
    }

    public override void FixedUpdate() {}
    
    public override void ChangeState()
    {
        if (!_isAirborne && _jumpTime > .3f) controller.SetState(typeof(IdleState));
    }

    public override void Exit() => _animationHandler.JumpAnimation(false);

}
