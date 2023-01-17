using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Rock/Held")]
public class HeldState : State<RockController>
{
    private bool _thrown;

    [Header("Health")]
    [SerializeField] private int _startHealth;

    [Header("Throw")]
    [SerializeField] private Vector2 _throwForce;
    [SerializeField] private float _torque;

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

        if (_thrown) // If the rock was thrown instead of released due to damage 
        {
            controller.ThrowForce = _throwForce;
            controller.Torque = _torque;
        }
    }

    public override void FixedUpdate() {}

    public override void Update() {}
}
