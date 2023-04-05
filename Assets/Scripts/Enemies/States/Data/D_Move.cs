using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States.Generics.Data {

    [CreateAssetMenu(menuName = "Enemies/State Data/Move State")]
    public class D_Move : StateMachine.StateData
    {
        public float moveSpeed = 3f;
    } 
}
