using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/ThrowRaccoon")]
    public class D_ThrowRaccoon : StateMachine.StateData
    {
        public StateMachine.Entity raccoon;
    }
}
