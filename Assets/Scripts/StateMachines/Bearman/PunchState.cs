using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "States/Character/Punch")]
public class PunchState : State<BearmanCtrl>, IAttack
{
    [SerializeField] private float _punchRadius = .5f;
    [SerializeField] private int _damage = 10;

    private bool _attacking;
    Transform _punchLocation;

    private BearmanAnimationHandler _animationHandler;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_punchLocation == null) _punchLocation = controller.transform.Find("PunchCheck");
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;

        controller.EventsHandler.AttackEvent += Attack;
        controller.EventsHandler.FinishAttackEvent += FinishAttack;
        

        _attacking = true;
        _animationHandler.AttackAnimation();
    }

    public override void CaptureInput() {}

    public override void Update() {}

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        if (!_attacking) controller.SetState(typeof(IdleState));
    }

    public override void Exit()
    {
        controller.EventsHandler.AttackEvent -= Attack;
        controller.EventsHandler.ChargedAttackEvent -= Attack;
    }

    public void Attack()
    {
         Collider2D[] enemies = Physics2D.OverlapCircleAll(_punchLocation.position, _punchRadius);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(_damage);
        }
    }

    public void FinishAttack() => _attacking = false;
}
