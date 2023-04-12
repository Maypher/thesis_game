using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Airborne/AirMove")]
    public class D_AirMove : StateMachine.StateData
    {
        public float maxSpeed = 5f;
        public float fallingGravity = 1f;
        [Tooltip("Time after a dash the player can jump")] public float doubleJumpGap = .4f;
        public float coyoteTime = .3f;
    }
}