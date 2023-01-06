using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearmanAnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = transform;
    }

    public string GetCurrentAnimation() => _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

    public float GetCurrentAnimationNormalizedTime() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

    public void PlayAnimation(string id) => _animator.CrossFade(id, 0);


    public void DamageAnimation() => _animator.SetTrigger("damaged");

    // Handled directly within the animation handler to avoid repetition in multiple states
    public void CorrectRotation(float xInput)
    {
        if (xInput != 0)
        {
            _transform.localScale = new Vector3(Mathf.Sign(xInput), _transform.localScale.y, _transform.localScale.z);
        }
    }

    public void FlexAnimation() => _animator.SetTrigger("flex");

    public void WalkingAnimation(bool isMoving) => _animator.SetBool("isMoving", isMoving);

    public void JumpAnimation(bool isAirborne) => _animator.SetBool("isAirborne", isAirborne);

    public void ChargeAnimation(bool isCharging) => _animator.SetBool("isCharging", isCharging);

    public void CrouchAnimation(bool isCrouching) => _animator.SetBool("isCrouching", isCrouching);

    public void PickUpRockAnimation(bool holdingRock) => _animator.SetBool("holdingRock", holdingRock);

    public void AttackAnimation() => _animator.SetTrigger("attack");

    public void ChargedAttackAnimation() => _animator.SetTrigger("chargedAttack");

    public void AimRaccoonAnimation(bool isAiming) => _animator.SetBool("aiming", isAiming);

    public void ThrowRacoonAnimation() => _animator.SetTrigger("throwing");
}
