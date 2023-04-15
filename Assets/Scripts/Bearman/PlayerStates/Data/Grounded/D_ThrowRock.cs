using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/ThrowRock")]
    public class D_ThrowRock : StateMachine.StateData
    {
        public Vector2 throwForce = new(2, 3);
    }
}