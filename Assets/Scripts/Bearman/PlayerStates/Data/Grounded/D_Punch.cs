using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/Punch")]
    public class D_Punch : StateMachine.StateData
    {
        public AttackDetails attackDetails;
    }
}