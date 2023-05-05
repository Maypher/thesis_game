using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Crocodile.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Crocodile/StateData/HideState")]
    public class D_Hide : StateMachine.StateData
    {
        public float hideTime = 1;
        public float timeUnderwater = 2;
        public float riseTime = 1;
    }
}