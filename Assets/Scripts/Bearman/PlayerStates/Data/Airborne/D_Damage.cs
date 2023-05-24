using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Airborne/Damage")]
    public class D_Damage : StateMachine.StateData
    {
        public float invincibilityTime = 1;
        public int flashTimes = 3;

        public AudioClip damageSFX;
    }
}