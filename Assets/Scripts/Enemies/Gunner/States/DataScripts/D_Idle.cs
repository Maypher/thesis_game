using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Gunner.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Gunner/StateData/IdleState")]
    public class D_Idle : StateMachine.StateData
    {
        [Header("Flip")]
        [Min(0)] public float minFlipTime = 1;
        [Min(0)] public float maxFlipTime = 5;
        public bool canFlip = false;

        [Header("Gun movement")]
        [Min(0)] public float rotationSpeed = 5;
        [Range(0, 360)] public float rotationAngle = 45;
        [Range(0, 360)] public float rotationOffset = 0;
    }
}