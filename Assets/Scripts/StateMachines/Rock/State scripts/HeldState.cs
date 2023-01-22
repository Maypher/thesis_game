using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Rock/Held")]
public class HeldState : State<RockController>
{
    private bool _thrown;

    [Header("Health")]
    [SerializeField] private int _startHealth;

    public override void Init(RockController parent)
    {
        base.Init(parent);
        _thrown = false;

        controller.CanBeDamaged = true;
    }

    public override void CaptureInput()
    {
        _thrown = Input.GetKeyDown(KeyCode.Mouse0);
    }

    public override void ChangeState()
    {
        if (_thrown) controller.SetState(typeof(ThrownState));
    }

    public override void Exit() {
        controller.CanBeDamaged = false;
    }

    public override void FixedUpdate() {}

    public override void Update() {}
}
