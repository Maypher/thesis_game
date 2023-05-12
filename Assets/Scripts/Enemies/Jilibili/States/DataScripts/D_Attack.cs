using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili.States.Data
{
    [CreateAssetMenu(menuName = "Enemies/Jilibili/StateData/AttackState")]
    public class D_Attack : StateMachine.StateData
    {
        public AttackDetails attackDetails;
    }
}
