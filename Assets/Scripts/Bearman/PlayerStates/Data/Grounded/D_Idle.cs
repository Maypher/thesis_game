using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/Idle")]
    public class D_Idle : StateMachine.StateData
    {
        public float timeToFlex = 10f;
    }
}