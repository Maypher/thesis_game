using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies.Crocodile.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Crocodile/StateData/BiteState")]
    public class D_Bite : StateMachine.StateData
    {
        [Min(0)] public float shakeDuration = 1;
        [Min(0)] public float shakeIntensity = 0.1f;

        public AudioClip biteSFX;

        public AttackDetails attackDetails;
    }
}