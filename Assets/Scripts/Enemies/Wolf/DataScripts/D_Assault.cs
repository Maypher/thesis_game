using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/AssaultState")]
    public class D_Assault : StateMachine.StateData
    {
        public Vector2 jumpForce;
        public int damage = 2;
    }
}
