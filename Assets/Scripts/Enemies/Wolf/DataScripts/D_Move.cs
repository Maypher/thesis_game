using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/MoveState")]
    public class D_Move : StateMachine.StateData
    {
        public float minWalkTime = 3f;
        public float maxWalkTime = 5f;
        public float moveSpeed = 5f;

        public AudioClip pantingSFX;
    }
}
