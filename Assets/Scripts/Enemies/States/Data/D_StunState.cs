using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new stun state data", menuName = "Data/State Data/Stun State Data")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3f;
    public float stunKnockbackTime = 0.2f;
    public Vector2 stunKnockbackAngle;
    public float stunKnockbackSpeed = 2f;
}
