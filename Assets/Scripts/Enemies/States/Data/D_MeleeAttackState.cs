using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new melee attack data", menuName = "Data/State Data/Melee attack state Data")]
public class D_MeleeAttackState : ScriptableObject
{
    [Header("Damage")]
    public int damage = 2;

    [Header("Movement")]
    public float velocity;
    public Vector2 angle;
}
