using UnityEngine;


[CreateAssetMenu(menuName = "States/Character/Shockwave")]
public class ShockwaveState : State<BearmanCtrl>
{
    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        controller.AnimationHandler.ShockwaveAttackAnimation();
    }

    public override void CaptureInput() {}

    public override void Update()
    {
        //throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void ChangeState()
    {
        controller.SetState(typeof(IdleState));
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

}
