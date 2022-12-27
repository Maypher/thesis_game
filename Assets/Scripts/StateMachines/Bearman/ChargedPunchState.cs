using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/ChargedPunch")]
public class ChargedPunchState : State<BearmanCtrl>, IAttack
{
    [Header("Damage values")]
    [SerializeField] private int _smallDamage = 20;
    [SerializeField] private int _mediumDamage = 30;
    [SerializeField] private int _heavyDamage = 50;

    private float _chargeTime;
    private bool _attacking;


    [Header("Charge time")]
    private float _smallCharge;
    [SerializeField] private float _mediumCharge = 2;
    [SerializeField] private float _heavyCharge = 2.5f;

    [Header("Components")]
    private Transform _punchLocation;
    private BearmanAnimationHandler _animationHandler;
    [SerializeField] private float _punchRadius = .5f;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_punchLocation == null) _punchLocation = controller.transform.Find("PunchCheck");
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        _chargeTime = (controller.PreviousState as ChargeState).ChargeTime;
        _smallCharge = (controller.PreviousState as ChargeState).ChargeToHeavyAttack;
        _attacking = true;

        controller.EventsHandler.FinishAttackEvent += FinishAttack;

        _animationHandler.ChargedAttackAnimation();
    }

    public override void CaptureInput() { }

    public override void FixedUpdate()
    {
       // throw new System.NotImplementedException();
    }

    public override void Update()
    {
    //    throw new System.NotImplementedException();
    }

    public override void ChangeState()
    {
        if (!_attacking) controller.SetState(typeof(IdleState));
    }

    public override void Exit()
    {
        controller.EventsHandler.FinishAttackEvent -= FinishAttack;
    }

    public void Attack()
    {
        int hitDamage;

        if (_chargeTime > _smallCharge && _chargeTime < _mediumCharge) hitDamage = _smallDamage;
        else if (_chargeTime >= _mediumCharge && _chargeTime < _heavyCharge) hitDamage = _mediumDamage;
        else hitDamage = _heavyDamage;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_punchLocation.position, _punchRadius);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(hitDamage);
        }
    }

    public void FinishAttack() => _attacking = false;
}
