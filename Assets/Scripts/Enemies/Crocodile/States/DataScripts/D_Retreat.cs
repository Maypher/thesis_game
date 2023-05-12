using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Jilibili/StateData/RetreatState")]
    public class D_Retreat : StateMachine.StateData
    {
        [Min(0)] public float jumpForce = 5;
        public Vector2 jumpAngle;
    }
}