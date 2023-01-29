using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Rock/Held")]
public class HeldState : State<RockController>
{
    [Header("Health")]
    [SerializeField] private int _startHealth;

    public override void Init(RockController parent)
    {
        base.Init(parent);

        controller.CanBeDamaged = true;
    }

    public override void CaptureInput() {}

    public override void ChangeState()
    {
        if (controller.Thrown) controller.SetState(typeof(ThrownState));
    }

    public override void Exit() {
        controller.CanBeDamaged = false;
    }

    public override void FixedUpdate() {}

    public override void Update() {}
}
