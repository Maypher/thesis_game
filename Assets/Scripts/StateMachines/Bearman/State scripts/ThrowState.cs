using UnityEngine;


[CreateAssetMenu(menuName = "States/Character/RaccoonThrow")]
public class ThrowState : State<BearmanCtrl>
{
    [SerializeField] private GameObject _raccoon;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);
        Throw();
    }

    private void Throw()
    {
        GameObject raccoon = Instantiate(_raccoon, controller.launchPosition.position, controller.launchPosition.rotation);
        Rigidbody2D raccoonRb = raccoon.GetComponent<Rigidbody2D>();

        raccoonRb.mass = controller.raccoonMass;
        raccoonRb.AddForce(controller.launchPosition.transform.right * controller.throwForce, ForceMode2D.Impulse);
    }

    public override void CaptureInput() {}

    public override void Update() {}

    public override void FixedUpdate() {}

    public override void ChangeState() 
    {
        controller.SetState(typeof(IdleState));
    }

    public override void Exit() {}
}
