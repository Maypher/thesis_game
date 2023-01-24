using UnityEngine;


[CreateAssetMenu(menuName = "States/Character/Shockwave")]
public class ShockwaveState : State<BearmanCtrl>, IAttack
{

    [SerializeField] private GameObject _shockwavePrefab;
    [SerializeField] private int _shockwaveDamage;

    private bool _spawnedShockwave;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);

        controller.AnimationHandler.ShockwaveAttackAnimation();
        controller.EventsHandler.Shockwave += Attack;

        _spawnedShockwave = false;
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
        if (_spawnedShockwave) controller.SetState(typeof(IdleState));
    }

    public override void Exit() => controller.EventsHandler.Shockwave -= Attack;

    public void Attack()
    {
        Transform shockwavePos = controller.transform.Find("ShockwavePos");
        GameObject shockwave = Instantiate(_shockwavePrefab, shockwavePos.position, shockwavePos.rotation);
        if (controller.AnimationHandler.FacingDirection == -1) shockwave.transform.localScale = new Vector3(-1, 1, 1);
        shockwave.GetComponent<Shockwave>().SetDamage(_shockwaveDamage);

        _spawnedShockwave = true;
    }

    public void FinishAttack()
    {
    }
}
