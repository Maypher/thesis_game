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
        GameObject raccoon = Instantiate(_raccoon, position: controller.LaunchPosition.position, Quaternion.identity);
        Rigidbody2D raccoonRb = raccoon.GetComponent<Rigidbody2D>();

        raccoonRb.mass = controller.RaccoonMass;
        raccoonRb.AddForce(controller.AnimationHandler.FacingDirection * controller.RaccoonThrowForce * controller.LaunchPosition.transform.right, ForceMode2D.Impulse);
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
