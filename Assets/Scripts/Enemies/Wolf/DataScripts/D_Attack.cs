using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Wolf/StateData/AttackState")]
    public class D_Attack : StateMachine.StateData
    {
        public AudioClip attackSFX;
        public AttackDetails details;
    }
}
