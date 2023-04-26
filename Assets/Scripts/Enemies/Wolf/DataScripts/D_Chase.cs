using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/ChaseState")]
    public class D_Chase : StateMachine.StateData
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float chaseTime = 3f;

        [Header("Jump")]
        [Min(1)] public float minJumpAngle = 20;
        [Min(1)] public float maxJumpAngle = 45;
        public float maxJumpDistanceX;
        public float maxJumpDistanceY;
        public LayerMask whatIsGround;
    }
}