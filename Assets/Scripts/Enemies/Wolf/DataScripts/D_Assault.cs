using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/AssaultState")]
    public class D_Assault : StateMachine.StateData
    {
        public float jumpForce = 6;
        public Vector2 jumpAngle;
    }
}
