using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearmanAnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private  CharacterEvents.EventsHandler _eventsHandler;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _eventsHandler = GetComponent<BearmanCtrl>().EventsHandler;
        _transform = transform;

        _eventsHandler.WalkingEvent += WalkingAnimation;
        _eventsHandler.JumpEvent += JumpAnimation;
        _eventsHandler.ChargeEvent += ChargeAnimation;
    }

    // Handled directly within the animation handler to avoid repetition in multiple states
    public void CorrectRotation(float xInput)
    {
        if (xInput != 0)
        {
            _transform.localScale = new Vector3(Mathf.Sign(xInput), _transform.localScale.y, _transform.localScale.z);
        }
    }

    private void WalkingAnimation(bool isMoving) => _animator.SetBool("isMoving", isMoving);

    private void JumpAnimation(bool isAirborne) => _animator.SetBool("isAirborne", isAirborne);

    private void ChargeAnimation(bool isCharging) => _animator.SetBool("isCharging", isCharging);

    public void AttackAnimation() => _animator.SetTrigger("Attack");

    public void ChargedAttackAnimation() => _animator.SetTrigger("ChargedAttack");
}
