using UnityEngine;

namespace Bearman.States
{
    [CreateAssetMenu(menuName = "States/Character/ShockwaveAttack")]
    public class ShockwaveAttack : State<BearmanCtrl>, IAttack
    {

        [SerializeField] private GameObject _ShockwaveAttackPrefab;
        [SerializeField] private int _ShockwaveAttackDamage;

        private bool _spawnedShockwaveAttack;

        public override void Init(BearmanCtrl parent)
        {
            base.Init(parent);

            controller.AnimationHandler.SetParameter(BearmanCtrl.Shockwave);
            controller.AnimationEvent += Attack;

            _spawnedShockwaveAttack = false;
        }

        public override void CaptureInput() { }

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
            if (_spawnedShockwaveAttack) controller.SetState(typeof(Idle));
        }

        public override void Exit() => controller.AnimationEvent -= Attack;

        public void Attack()
        {
            Transform ShockwaveAttackPos = controller.transform.Find("ShockwaveSpawnPos");
            GameObject ShockwaveAttack = Instantiate(_ShockwaveAttackPrefab, ShockwaveAttackPos.position, ShockwaveAttackPos.rotation);
            if (controller.AnimationHandler.FacingDirection == -1) ShockwaveAttack.transform.localScale = new Vector3(-1, 1, 1);
            ShockwaveAttack.GetComponent<Shockwave.Shockwave>().SetDamage(_ShockwaveAttackDamage);

            _spawnedShockwaveAttack = true;
        }

        public void FinishAttack()
        {
        }
    }
}