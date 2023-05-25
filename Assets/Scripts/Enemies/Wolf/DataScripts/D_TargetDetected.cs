using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/TargetDetected")]
    public class D_TargetDetected : StateMachine.StateData
    {
        public float warnTime = 2f;
        public AudioClip warnSFX;
    }
}
