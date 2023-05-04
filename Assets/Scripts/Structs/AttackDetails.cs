using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AttackDetails
{
    public int damage;

    [HideInInspector] public Vector2 attackPostion;
    public float knockbackForce;
    public Vector2 knockbackAngle;
}
