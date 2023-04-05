using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States.Generics.Data
{
    [CreateAssetMenu(menuName = "Enemies/State Data/Idle State")]
    public class D_Idle : StateMachine.StateData
    {
        [Min(0)] public float MinIdleTime = 2f;
        public float MaxIdleTime = 5f;
    }
}
