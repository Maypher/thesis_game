using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Airborne/Dash")]
    public class D_Dash : StateMachine.StateData
    {
        public float dashForce = 5f;
        public float dashTime = .2f;
        public float cooldownTime = .5f;
        public AudioClip dashSFX;
        public AttackDetails attackDetails;
    }
}
