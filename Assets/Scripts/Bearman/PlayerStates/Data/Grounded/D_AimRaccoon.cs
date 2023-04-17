using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/AimRaccoon")]
    public class D_AimRaccoon : StateMachine.StateData
    {
        public float rotationAngle = 45;
        public float rotationSpeed = 3;
        public float rotationOffset = 0;
    }
}