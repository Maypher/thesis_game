using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States.Generics.Data {

    [CreateAssetMenu(menuName = "Enemies/State Data/Move State")]
    public class D_Move : StateMachine.StateData
    {
        public float moveSpeed = 3f;
        [Tooltip("Min time before transitioning to idle")] public float minWalkTime = 3f;
        [Tooltip("Max time before transitioning to idle")] public float maxWalkTime = 7f;
    } 
}
