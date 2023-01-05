using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/ChargedPunch")]
public class ChargedPunchState : State<BearmanCtrl>, IAttack
{
    private bool _attacking;

    private BearmanAnimationHandler _animationHandler;

    [SerializeField] private int _smallDamage = 20;
    [SerializeField] private int _mediumDamage = 30;
    [SerializeField] private int _heavyDamage = 50;

    [SerializeField] private float _smallChargeTime;
    [SerializeField] private float _mediumChargeTime = 2;
    [SerializeField] private float _heavyChargeTime = 2.5f;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

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

        if (controller.chargeTime > _smallChargeTime && controller.chargeTime < _mediumChargeTime) hitDamage = _smallDamage;
        else if (controller.chargeTime >= _mediumChargeTime && controller.chargeTime < _heavyChargeTime) hitDamage = _mediumDamage;
        else hitDamage = _heavyDamage;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(controller.punchLocation.position, controller.punchRadius);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(hitDamage);
        }
    }

    public void FinishAttack() => _attacking = false;
}
