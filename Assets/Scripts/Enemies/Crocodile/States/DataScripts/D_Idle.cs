using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Crocodile.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Crocodile/StateData/IdleState")]
    public class D_Idle : StateMachine.StateData
    {
        [Min(0)] public float minTimeToSound = 1;
        [Min(0)] public float maxTimeToSound = 6;
    }
}