using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Make the controller a state machine and pass itself as a reference 
public class BearmanCtrl : StateMachine<BearmanCtrl>, IDamageable, IAttack
{
    public BearmanAnimator Animator;
    public CharacterEvents.EventsHandler EventsHandler;
    public BearmanAnimationHandler AnimationHandler;

    [SerializeField] private int maxHealth = 40;
    private int health;

    protected override void Awake()
    {
        base.Awake();
        Animator = new BearmanAnimator(GetComponent<Animator>(), transform);
        EventsHandler = new CharacterEvents.EventsHandler();
        AnimationHandler = GetComponent<BearmanAnimationHandler>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    [HideInInspector] public bool TakeDamage(int damagePt)
    {
        SetState(typeof(DamageState));

        health -= damagePt;
        if (health <= 0) 
        {
            Kill();
            return true;
        }

        return false;
    }

    [HideInInspector] public void Kill()
    {
        Destroy(this.gameObject);
    }

    public void Attack()
    {
        EventsHandler.InvokeAttackEvent();
    }

    public void FinishAttack()
    {
        EventsHandler.InvokeFinishAttackEvent();
    }
}
