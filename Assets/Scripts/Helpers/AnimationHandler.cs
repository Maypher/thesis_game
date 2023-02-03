using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = transform;

    }

    public float FacingDirection
    {
        get { return _transform.localScale.x; }
    }

    public string GetCurrentAnimation() => _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

    // Handled directly within the animation handler to avoid repetition in multiple states
    public void CorrectRotation(float xInput)
    {
        if (xInput != 0)
        {
            _transform.localScale = new Vector3(Mathf.Sign(xInput), _transform.localScale.y, _transform.localScale.z);
        }
    }

    public void SetParameter(string animation) => _animator.SetTrigger(Animator.StringToHash(animation));

    public void SetParameter(string animation, bool isTrue) => _animator.SetBool(Animator.StringToHash(animation), isTrue);

    public void SetParameter(string animation, float value) => _animator.SetFloat(Animator.StringToHash(animation), value);
}
