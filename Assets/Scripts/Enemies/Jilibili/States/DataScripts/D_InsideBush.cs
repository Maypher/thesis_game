using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Jilibili/StateData/InsideBushState")]
    public class D_InsideBush : StateMachine.StateData
    {
        [Min(0)] public float jumpForce;
        public Vector2 jumpAngle;
    }
}
