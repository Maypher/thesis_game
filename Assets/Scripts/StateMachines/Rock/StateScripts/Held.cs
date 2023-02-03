using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rock.States
{
    [CreateAssetMenu(menuName = "States/RockHeld/Held")]
    public class Held : State<RockController>
    {
        [Header("Health")]
        [SerializeField] private int _startHealth;

        public override void Init(RockController parent)
        {
            base.Init(parent);

            controller.CanBeDamaged = true;
        }

        public override void CaptureInput() { }

        public override void ChangeState()
        {
            if (controller.Thrown) controller.SetState(typeof(Thrown));
        }

        public override void Exit()
        {
            controller.CanBeDamaged = false;
        }

        public override void FixedUpdate() { }

        public override void Update() { }
    }
}