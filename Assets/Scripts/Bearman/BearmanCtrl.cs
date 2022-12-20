using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make the controller a state machine and pass itself as a reference 
public class BearmanCtrl : StateMachine<BearmanCtrl>
{
    public BearmanAnimator Animator;

    protected override void Awake()
    {
        base.Awake();
        Animator = new BearmanAnimator(GetComponent<Animator>(), GetComponent<Transform>());
    }
}
