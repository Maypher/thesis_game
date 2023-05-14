using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States.Data 
{

    [CreateAssetMenu(menuName = "Raccoon/State Data/ReturnToPlayer")]
    public class D_ReturnToPlayer : StateMachine.StateData
    {
        public float maxDistanceDelta = 0.1f;
        [Min(0)] public float screenSpawnInset = 2;
    }
}
