using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Airborne/Dash")]
    public class D_Dash : StateMachine.StateData
    {
        [Header("Dash")]
        public float dashForce = 5f;
        public float dashTime = .2f;
        public float cooldownTime = .5f;
        public AudioClip dashSFX;

        [Header("Attack")]
        public float invinsibilityTime = .4f;
        public AttackDetails attackDetails;
    }
}
