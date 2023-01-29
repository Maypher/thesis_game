using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

//[CreateAssetMenu(menuName = "States/Character/Punch")]
public class PunchState : State<BearmanCtrl>, IAttack
{
    private bool _charging;
    private float _chargeTime;

    private bool _attacking;
    private bool _triggerAttack;
    private bool _finishedAttack;

    [Header("Damage")]
    [SerializeField] private int _damage = 5;

    [Header("Charged Damage")]
    [SerializeField] private int _smallDamage = 20;
    [SerializeField] private int _mediumDamage = 30;
    [SerializeField] private int _heavyDamage = 50;

    [Header("Charge Time")]
    [SerializeField] private float _smallChargeTime = 1;
    [SerializeField] private float _mediumChargeTime = 2;
    [SerializeField] private float _heavyChargeTime = 2.5f;

    private BearmanAnimationHandler _animationHandler;
    private AttackCheck _attackCheck;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;
        if (_attackCheck == null) _attackCheck = controller.GetComponentInChildren<AttackCheck>();

        controller.EventsHandler.AttackEvent += Attack;
        controller.EventsHandler.ChargedAttackEvent += Attack;
        controller.EventsHandler.FinishAttackEvent += FinishAttack;


        _attacking = false;
        _finishedAttack = false;
        _charging = true;
        _triggerAttack = false;

        _chargeTime = 0;
    }


    public override void CaptureInput() 
    {
        _charging = controller.UserInput.Player.Punch.IsPressed();
        _triggerAttack = controller.UserInput.Player.Punch.WasReleasedThisFrame();
    }

    public override void Update() 
    {
        _animationHandler.ChargeAnimation(_charging);

        // Since CaptureInput gets called every frame even when the attack is ongoing the animation triggers can be
        // Continuously set. By blocking the functionality if its already attacking this problem gets fixed
        if (!_attacking)
        {
            if (_charging) _chargeTime += Time.deltaTime;
            else if (_triggerAttack)
            {
                if (_chargeTime < _smallChargeTime) _animationHandler.AttackAnimation();
                else _animationHandler.ChargedAttackAnimation();
                _attacking = true;
            }
        }
    }

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        if (_finishedAttack) controller.SetState(typeof(IdleState));
    }


    public override void Exit()
    {
        controller.EventsHandler.AttackEvent -= Attack;
        controller.EventsHandler.ChargedAttackEvent -= Attack;
        controller.EventsHandler.FinishAttackEvent -= FinishAttack;
    }

    public void Attack()
    {
        int hitDamage;

        if (_chargeTime < _smallChargeTime) hitDamage = _damage;
        else if (_chargeTime < _mediumChargeTime) hitDamage = _smallDamage;
        else if (_chargeTime < _heavyChargeTime) hitDamage = _mediumDamage;
        else hitDamage = _heavyDamage;

        Collider2D[] enemies = _attackCheck.Check();

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(hitDamage);
        }
    }

    public void FinishAttack() => _finishedAttack = true;
}
