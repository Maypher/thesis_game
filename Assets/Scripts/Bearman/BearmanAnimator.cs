using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearmanAnimator
{
    readonly Animator _animator;
    readonly Transform _transform;

    public BearmanAnimator(Animator animator, Transform transform)
    {
        _animator = animator;
        _transform = transform;
    }

    public void CorrectRotation(float xInput)
    {
        if (xInput != 0)
        {
            _transform.localScale = new Vector3(Mathf.Sign(xInput), _transform.localScale.y, _transform.localScale.z);
        }
    }

    public void MovingAnimation(bool isGrounded) => _animator.SetBool("isMoving", isGrounded);

    public void JumpAnimation(bool isAirborne) =>_animator.SetBool("isAirborne", isAirborne);

    public void CrouchAnimation(bool isCrouching) => _animator.SetBool("isCrouching", isCrouching);

    public void ChargeAnimation(bool isCharging) => _animator.SetBool("isCharging", isCharging);

    public void AttackAnimation(bool isAttacking) => _animator.SetBool("isAttacking", isAttacking);
    
    public void ChargedAttackAnimation(bool chargeAttack) => _animator.SetBool("isChargedAttack", chargeAttack);

    public void PickUpRock(bool holdingRock) => _animator.SetBool("holdingRock", holdingRock);

    public void DamagedAnimation() => _animator.SetTrigger("Damaged");
}
