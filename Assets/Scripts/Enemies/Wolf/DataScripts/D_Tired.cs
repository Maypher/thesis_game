using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/TiredState")]
    public class D_Tired : StateMachine.StateData
    {
        public float tiredTime = 3f;
    }
}