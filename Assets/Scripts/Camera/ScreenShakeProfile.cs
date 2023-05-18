using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScreenShake/New Profile")]
public class ScreenShakeProfile : ScriptableObject
{
    [Header("Impulse Source Settings")]
    public float impactTime = 0.2f;
    public float impactForce = 1;
    public Vector3 defaultVelocity = new(0, -1, 0);
    public AnimationCurve impulseCurve;

    [Header("Impulse Listener Settings")]
    public float listenerAmplitude = 1;
    public float listenerFrequency = 1;
    public float listenerDuration = 1;
}
