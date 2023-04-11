using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{

    [CreateAssetMenu(menuName = "Player/State Data/Grounded/Walk")]
    public class D_Walk : StateMachine.StateData
    {
        [Header("Acceleration")]
        public AnimationCurve acceleration;
        public float timeToMaxSpeed = 1f;
        public float maxSpeed = 4f;

        [Header("Deceleration")]
        public AnimationCurve deceleration;
        public float timeToFullStop = 1f;

    }
}