using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Jilibili/StateData/JumpForwardState")]
    public class D_JumpForward : StateMachine.StateData
    {
        public float jumpForce;
        public Vector2 jumpAngle;
    }
}