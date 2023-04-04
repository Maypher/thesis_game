using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new player data", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("Use a normalized curve")] public AnimationCurve MovementSpeed;
    public float MaxSpeed = 5f;
    public float TimeToMaxSpeed = 1f;
}
