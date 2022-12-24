using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Punch")]
public class PunchState : State<BearmanCtrl>, IAttack
{
    [SerializeField] private float _punchRadius = .5f;
    [SerializeField] private int _normalDamage = 10;
    // Used for charged attack
    [SerializeField] private int _smallDamage = 20;
    [SerializeField] private int _mediumDamage = 30;
    [SerializeField] private int _heavyDamage = 50;

    // Used to change charge attack times (in seconds)
    [SerializeField] private float _smallCharge = 1;
    [SerializeField] private float _mediumCharge = 2;
    [SerializeField] private float _heavyCharge = 2.5f;

    // Variables
    private bool _charging;
    private float _chargeTime;
    private bool _attacking;

    // Components
    Transform _punchLocation;
    BearmanAnimator _animator;
    ParticleSystem _punchParticles;
    AnimationEvents _animationEvents;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        if (_punchLocation == null) _punchLocation = controller.transform.Find("PunchCheck");
        if (_animator == null) _animator = controller.Animator;
        if (_animationEvents == null) _animationEvents = controller.GetComponent<AnimationEvents>();

        _charging = true;
        _attacking = false;
        _chargeTime = 0;

        _animationEvents.AttackAnimationEvent.AddListener(Attack);
        _animationEvents.FinishAttackAnimationEvent.AddListener(FinishAttack);
    }

    public override void CaptureInput()
    {
        _charging = Input.GetKey(KeyCode.Mouse0);
    }

    public override void Update()
    {
        _animator.ChargeAnimation(_charging);
        if (_charging) _chargeTime += Time.deltaTime;
        else if (_chargeTime > 0)
        {
            _attacking = true;
            if (_chargeTime < _smallCharge) _animator.AttackAnimation(true);
            else _animator.ChargedAttackAnimation(true);
        }
    }

    public override void FixedUpdate() {}

    public override void ChangeState()
    {
        if (!_charging && !_attacking) controller.SetState(typeof(IdleState));
    }

    public override void Exit()
    {
        _attacking = false;
        _charging = false;
        _animator.ChargeAnimation(false);
        _animator.AttackAnimation(false);
        _animator.ChargedAttackAnimation(false);
    }

    public void Attack()
    {
        int hitDamage;

        if (_chargeTime > _smallCharge && _chargeTime < _mediumCharge) hitDamage = _smallDamage;
        else if (_chargeTime >= _mediumCharge && _chargeTime < _heavyCharge) hitDamage = _mediumDamage;
        else if (_chargeTime >= _heavyCharge) hitDamage = _heavyDamage;
        else hitDamage = _normalDamage;

         Collider2D[] enemies = Physics2D.OverlapCircleAll(_punchLocation.position, _punchRadius);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(hitDamage);
        }
    }

    public void FinishAttack() 
    {
        _charging = false;
        _attacking = false;
        _animator.ChargedAttackAnimation(false);
        _animator.ChargeAnimation(false);
        _animator.AttackAnimation(false);
        _chargeTime = 0;
    }
}
