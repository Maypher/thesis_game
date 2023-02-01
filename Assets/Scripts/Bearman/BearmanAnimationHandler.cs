using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BearmanAnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;

    public enum Parameters
    {
        Flex,
        IsMoving,
        IsCrouching,
        IsAirborne,
        HoldingRock,
        PickUpRock,
        TakeDamage,
        IsCharging,
        Attack,
        ChargedAttack,
        Aiming,
        Shockwave
    };

    private Dictionary<Parameters, int> _parameterHash;

    public float FacingDirection 
    {
        get { return transform.localScale.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = transform;

        if (_parameterHash == null) _parameterHash = new()
        {
            { Parameters.Flex, Animator.StringToHash("flex") },
            { Parameters.IsMoving, Animator.StringToHash("isMoving") },
            { Parameters.IsAirborne, Animator.StringToHash("isAirborne") },
            { Parameters.IsCrouching, Animator.StringToHash("isCrouching") },
            { Parameters.HoldingRock, Animator.StringToHash("holdingRock") },
            { Parameters.PickUpRock, Animator.StringToHash("pickUpRock") },
            { Parameters.TakeDamage, Animator.StringToHash("takeDamage") },
            { Parameters.IsCharging, Animator.StringToHash("isCharging") },
            { Parameters.Attack, Animator.StringToHash("attack") },
            { Parameters.ChargedAttack, Animator.StringToHash("chargedAttack") },
            { Parameters.Aiming, Animator.StringToHash("aiming") },
            { Parameters.Shockwave, Animator.StringToHash("shockwave") }
        };
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

    public void SetParameter(Parameters animation) => _animator.SetTrigger(_parameterHash[animation]);
    
    public void SetParameter(Parameters animation, bool isTrue) => _animator.SetBool(_parameterHash[animation], isTrue);

    public void SetParameter(Parameters animation, float value) => _animator.SetFloat(_parameterHash[animation], value);
}
