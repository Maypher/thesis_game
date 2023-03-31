using UnityEngine;

[CreateAssetMenu(fileName = "new move state data", menuName = "Data/State Data/Move State Data")]
public class D_IdleState : ScriptableObject
{
    public float MinIdleTime = 1f;
    public float MaxIdleTime = 2f;
}
