using UnityEngine;

[CreateAssetMenu(fileName = "new entity data", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    [Header ("Collision check")]
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public LayerMask whatIsGround;
}
