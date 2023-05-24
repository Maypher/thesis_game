using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Raccoon.States.Data 
{

    [CreateAssetMenu(menuName = "Raccoon/State Data/ReturnToPlayer")]
    public class D_ReturnToPlayer : StateMachine.StateData
    {
        [Header("Raccoon")]
        public float maxDistanceDelta = 0.1f;
        public Vector2 screenSpawnInset = new(2, 0);
        [Header("Tree")]
        public GameObject treeBranch;
        public Vector2 treeScreenSpawnInset;
        [Header("Tree movement")]
        public Vector2 maxTreeMovement;
        public EasingType goDownEasing;
        public float goDownduration = 5;
        public EasingType goUpEasing;
        public float goUpDuration = 1;
        [Header("SFX")]
        public AudioClip treeShakingSFX;
    }
}
