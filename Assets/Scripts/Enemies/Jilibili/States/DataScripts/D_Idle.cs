using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies.Jilibili.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Jilibili/StateData/IdleState")]
    public class D_Idle : StateMachine.StateData
    {
        public float minAttackTime = 1;
        public float maxAttackTime = 3;
        [Range(0, 1)] public float retreatProbability = 0.4f;
    }
}
