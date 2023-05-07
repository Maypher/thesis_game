using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Gunner.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Gunner/StateData/ShootState")]
    public class D_Shoot : StateMachine.StateData
    {
        [Header("Movement")]
        [Min(0)] public float rotateSpeed = 5;
        public float gunRecoilAngleRange = 45;
        public float minShootInterval = .4f;
        public float maxShootInterval = 2f;

        [Header("Attack")]
        public GameObject bulletTrail;
        public LayerMask whatIsEnemy;
        public AttackDetails attackDetails;
    }
}
