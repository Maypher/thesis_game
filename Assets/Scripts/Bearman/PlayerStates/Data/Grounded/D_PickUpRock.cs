using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Substates.Data
{
    [CreateAssetMenu(menuName = "Player/State Data/Grounded/PickUpRock")]
    public class D_PickUpRock : StateMachine.StateData
    {
        public GameObject rockPrefab;
    }
}