using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearmanCombat : MonoBehaviour, IAttack
{
    public bool IsAttacking { get; private set; } = false;
    public bool ChargedAttack { get; private set; } = false;
    public float ChargeTime { get; private set; } = 0f;
    public bool IsCharging { get; private set; } = false;

    private BearmanMovement characterController;
    private Transform punchLocation;

    [SerializeField] private int damage = 10;
    [SerializeField] private int smallChargeDamage = 20;
    [SerializeField] private int mediumChargeDamage = 40;
    [SerializeField] private int heavyChargeDamage = 50;


    [SerializeField] private LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<BearmanMovement>();
        punchLocation = transform.Find("PunchCheck");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ChargeTime += Time.deltaTime;
            IsCharging = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            IsCharging = false;
            if (ChargeTime > 0.5) ChargedAttack = true;
            else IsAttacking = true;
        };

        if (!characterController.IsGrounded) IsAttacking = false;
    }

    public void Attack()
    {
        int hitDamage = damage;

        if (ChargeTime > 1 && ChargeTime < 1.5) hitDamage = smallChargeDamage;
        else if (ChargeTime >= 1.5 && ChargeTime < 2.5) hitDamage = mediumChargeDamage;
        else if (ChargeTime >= 2.5) hitDamage = heavyChargeDamage;


        Collider2D[] enemies = Physics2D.OverlapCircleAll(punchLocation.position, 1f, whatIsEnemy);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.TakeDamage(hitDamage);
        }
    }

    public void FinishAttack()
    {
        IsAttacking = false;
        ChargedAttack = false;
        ChargeTime = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        if (!punchLocation) return;
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(punchLocation.position, 1f);
    }
}
