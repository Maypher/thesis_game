using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/LookForTargetState")]
    public class D_LookForTarget : StateMachine.StateData
    {
        public int flipTimes;
        [Min(0)] public float minFlipDuration;
        [Min(0)] public float maxFlipDuration;
        public AudioClip lookSFX;
    }
}
