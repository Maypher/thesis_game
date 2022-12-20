using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearmanAnimator
{
    private readonly Animator _animator;
    private readonly Transform _transform;

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

    public void MovingAnimation(bool isGrounded)
    {
        _animator.SetBool("isMoving", isGrounded);
    }

    public void JumpAnimation(bool isAirborne)
    {
        _animator.SetBool("isAirborne", isAirborne);
    }

    public float GetCurrentLoopProgress() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
    
    public IEnumerator Wait(Func<bool> waitUntil)
    {
        yield return new WaitUntil(waitUntil);
    }
}
