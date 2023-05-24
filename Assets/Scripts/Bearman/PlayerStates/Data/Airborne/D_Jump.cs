using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Airborne/Jump")]
    public class D_Jump : StateMachine.StateData
    {
        public float jumpForce = 5f;
        [Tooltip("When jumping less gravity than normal can be applied")] public float jumpGravity = 1f;
        public AudioClip jumpSfx;
    }
}