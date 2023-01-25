using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Rock")]
public class RockState : State<BearmanCtrl>
{
    private Rigidbody2D _rb;
    private BearmanAnimationHandler _animationHandler;
    private Transform _rockSpawnPos;

    [Header("Movement")]
    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private AnimationCurve _deceleration;
    [SerializeField] private float _maxSpeed = 5;
    [SerializeField] private float _timeToMaxSpeed = 1;
    [SerializeField] private float _timeToFullStop = 1;

    [Header("Shield")]
    [SerializeField] private GameObject _rockPrefab;

    [Header("Thrown")]
    [SerializeField] private Vector2 _throwForce;
    [SerializeField] private float _torque = -20;

    private GameObject _rock;

    private float _xDirection;
    private float _walkDirection;
    private bool _throw;
    private float _walkTime;
    private float _slowDownTime;
    private bool _raisedRock;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = controller.GetComponent<Rigidbody2D>();
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;
        if (_rockSpawnPos == null) _rockSpawnPos = controller.transform.Find("RockSpawnPos");

        _throw = false;
        _walkTime = 0;
        _slowDownTime = 1; // Start at the end (no speed)
        _walkDirection = controller.AnimationHandler.FacingDirection;
        Debug.Log(_walkDirection);
        _raisedRock = false;

        _animationHandler.PickUpRockAnimation(true);

        _rock = Instantiate(_rockPrefab, _rockSpawnPos.position, _rockSpawnPos.rotation, _rockSpawnPos);
    }

    public override void CaptureInput()
    {
        _xDirection = Input.GetAxisRaw("Horizontal");
        _throw = Input.GetKeyDown(KeyCode.Mouse0);
        _raisedRock = Input.GetKey(KeyCode.W);
    }

    public override void Update()
    {
        controller.AnimationHandler.RaiseRockAnimation(_raisedRock);

        // Only move the character in the direction it was facing when state was entered
        if (_xDirection == _walkDirection) Accelerate();
        else Decelerate();
    }

    public override void FixedUpdate()
    {
    }

    public override void ChangeState()
    {
        if (_throw) 
        {
            controller.SetState(typeof(IdleState)); 
        }
    }

    public override void Exit()
    {
        _animationHandler.PickUpRockAnimation(false);
        controller.AnimationHandler.RaiseRockAnimation(false);

        if (_throw) ThrowRock(new Vector2(_throwForce.x * _walkDirection, _throwForce.y), _torque);
        else ThrowRock(Vector2.zero, 0);
    }

    private void Accelerate()
    {
        _walkTime += Time.deltaTime / _timeToMaxSpeed;
        _slowDownTime = _walkTime;

        _rb.velocity = new Vector2(_acceleration.Evaluate(_walkTime) * _maxSpeed * _walkDirection, _rb.velocity.y);
    }

    private void Decelerate()
    {
        _slowDownTime += Time.deltaTime / _timeToFullStop;
        _walkTime = _slowDownTime;

        _rb.velocity = new Vector2(_deceleration.Evaluate(_slowDownTime) * _maxSpeed * _walkDirection, _rb.velocity.y);
    }

    private void ThrowRock(Vector2 throwForce, float torque)
    {
        _rock.transform.SetParent(null, true);

        RockController script = _rock.GetComponent<RockController>();
        script.IgnoreCollisionWith(controller.GetComponent<Collider2D>());
        script.ThrowForce = throwForce;
        script.Torque = torque;
    }
}