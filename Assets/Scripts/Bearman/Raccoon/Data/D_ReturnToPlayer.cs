using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States.Data 
{

    [CreateAssetMenu(menuName = "Raccoon/State Data/ReturnToPlayer")]
    public class D_ReturnToPlayer : StateMachine.StateData
    {
        public LayerMask whatIsGround;

        public float moveSpeed = 10f;
    }
}
