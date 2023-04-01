using UnityEngine;

[CreateAssetMenu(fileName = "new idle state data", menuName = "Data/State Data/Idle State Data")]
public class D_IdleState : ScriptableObject
{
    public float MinIdleTime = 1f;
    public float MaxIdleTime = 2f;
}
