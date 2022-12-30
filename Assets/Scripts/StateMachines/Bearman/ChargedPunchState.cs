using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/ChargedPunch")]
public class ChargedPunchState : State<BearmanCtrl>, IAttack
{
    private bool _attacking;

    [Header("Components")]
    private BearmanAnimationHandler _animationHandler;

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

        if (controller.chargeTime > controller.smallChargeTime && controller.chargeTime < controller.mediumChargeTime) hitDamage = controller.smallDamage;
        else if (controller.chargeTime >= controller.mediumChargeTime && controller.chargeTime < controller.heavyChargeTime) hitDamage = controller.mediumDamage;
        else hitDamage = controller.heavyDamage;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(controller.punchLocation.position, controller.punchRadius);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(hitDamage);
        }
    }

    public void FinishAttack() => _attacking = false;
}
