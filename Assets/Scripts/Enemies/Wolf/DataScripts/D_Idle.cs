using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/IdleState")]
    public class D_Idle : StateMachine.StateData
    {
        [Min(0)] public float MinIdleTime = 2f;
        public float MaxIdleTime = 5f;
        [Tooltip("Chances of the character turning around when exiting the state")] [Range(0, 1)] public float flipChance = .4f;
        public AudioClip smellSFX;
    }
}