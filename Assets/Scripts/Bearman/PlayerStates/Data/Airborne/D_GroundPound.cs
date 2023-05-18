using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Airborne/Groundpound")]
    public class D_GroundPound : StateMachine.StateData
    {
        public Vector2 jumpForce = new(1, 3);
        public float jumpTime = .3f;
        public float fallForce = 10f;
        public float airHangTime = .3f;
        public ScreenShakeProfile screenShakeProfile;

        public AttackDetails attackDetails;
    }
}