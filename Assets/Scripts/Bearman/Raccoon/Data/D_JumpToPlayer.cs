using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States.Data
{
    [CreateAssetMenu(menuName = "Raccoon/State Data/JumpToPlayer")]
    public class D_JumpToPlayer : StateMachine.StateData
    {
        public Vector2 jumpForce = new(1, 1);
    }
}