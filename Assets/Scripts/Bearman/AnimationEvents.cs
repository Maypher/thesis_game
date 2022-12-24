using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [HideInInspector] public UnityEvent AttackAnimationEvent;
    [HideInInspector] public UnityEvent FinishAttackAnimationEvent;

    public void AttackAnimation() => AttackAnimationEvent.Invoke();

    public void FinishAttackAnimation() => FinishAttackAnimationEvent.Invoke();
}
