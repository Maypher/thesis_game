using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States.Data
{
    [CreateAssetMenu(menuName = "Raccoon/State Data/Launch")]
    public class D_Launch : StateMachine.StateData
    {
        public float launchForce = 30f;
        public AttackDetails attackDetails;
    }
}
