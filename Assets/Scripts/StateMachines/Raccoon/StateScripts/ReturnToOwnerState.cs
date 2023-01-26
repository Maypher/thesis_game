using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Raccoon/ReturnToOwner")]
public class ReturnToOwnerState : State<RaccoonController>
{
    [SerializeField] private GameObject _targetToReturnTo;
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;

    public override void Init(RaccoonController parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = controller.GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.zero;
    }

    public override void CaptureInput() {}

    public override void Update()
    {
        float targetInFrontOrBehind = Mathf.Sign(controller.transform.position.x - _targetToReturnTo.transform.position.x);

        _rb.velocity = new Vector2(_moveSpeed * targetInFrontOrBehind, _rb.velocity.y);

        controller.transform.localScale = new Vector3(Mathf.Sign(_rb.velocity.x), 1, 1);
    }

    public override void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeState()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

}
