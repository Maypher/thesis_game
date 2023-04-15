using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/HoldRock")]
    public class D_HoldRock : StateMachine.StateData
    {
        public float moveSpeed = 2f;
        public float moveInterval = 1f;
    }
}