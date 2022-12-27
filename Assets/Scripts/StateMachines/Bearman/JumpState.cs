using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "States/Character/Jump")]
public class JumpState : State<BearmanCtrl>
{
    // Components
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private BearmanAnimationHandler _animationHandler;

    // Variables
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = 1f; // When going up apply less gravity than when falling
    [SerializeField] private float fallGravity = 2f; // When falling apply bigger gravity for faster fall

    // To allow time for the jump to happen
    // The frame the jump happens the character is technically airborne but the groundCheck is still true due to the radius
    // so it would trigger the transition immediately.
    private float _jumpTime;
    private bool _isAirborne;


    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
        if (_groundCheck == null) _groundCheck = parent.GetComponentInChildren<GroundCheck>();
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _jumpTime = 0;
        _isAirborne = false;
        _animationHandler.JumpAnimation(true);

        // Apply jump force immediately after entering state
        _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public override void CaptureInput() {}

    public override void Update()
    {
        _jumpTime += Time.deltaTime;

        _isAirborne = !_groundCheck.Check();

        if (_rb.velocity.y < 0) _rb.gravityScale = fallGravity;
        else _rb.gravityScale = gravity;
    }

    public override void FixedUpdate() {}
    
    public override void ChangeState()
    {
        if (!_isAirborne && _jumpTime > .3f) controller.SetState(typeof(IdleState));
    }

    public override void Exit() => _animationHandler.JumpAnimation(false);

}
