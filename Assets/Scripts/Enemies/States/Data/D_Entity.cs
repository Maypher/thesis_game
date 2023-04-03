using UnityEngine;

[CreateAssetMenu(fileName = "new entity data", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    [Header("Entity data")]
    public float maxHealth = 5;
    public float damageHopSpeed = 1f;
    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    [Header ("Collision check")]
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public LayerMask whatIsGround;
}
