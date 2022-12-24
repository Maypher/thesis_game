using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Rock")]
public class RockState : State<BearmanCtrl>
{
    private Rigidbody2D _rb;
    private BearmanAnimator _animator;

    private float _xDirection;
    [SerializeField] private float _speed;
    [SerializeField] private float _stepInterval;

    private float _stepTimer;
    private bool _throw;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = controller.GetComponent<Rigidbody2D>();
        if (_animator == null) _animator = controller.Animator;

        _stepTimer = 0;
        _throw = false;
        _animator.PickUpRock(true);
    }

    public override void CaptureInput()
    {
        _xDirection = Input.GetAxisRaw("Horizontal");
        _throw = Input.GetKeyDown(KeyCode.Mouse0);
    }

    public override void Update()
    {
        _stepTimer += Time.deltaTime;
        _animator.CorrectRotation(_xDirection);
    }

    public override void FixedUpdate()
    {
        if (_stepTimer > _stepInterval)
        {
            _rb.AddForce(_speed * _xDirection * Vector2.right, ForceMode2D.Impulse);
            _stepTimer = 0;
        }
    }

    public override void ChangeState()
    {
        if (_throw) controller.SetState(typeof(IdleState));
    }

    public override void Exit()
    {
        _animator.PickUpRock(false);
    }

}
