using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kindly stolen from https://github.com/Comp3interactive/FieldOfView
public class FieldOfView : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _viewRadius;
    [Range(0, 360)]
    [SerializeField] private float _viewAngle;
    [SerializeField] [Range(-180, 180)] private float _angleOffset;
    public LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    [HideInInspector] public Vector2 Scale = new(1, 1);

    public GameObject Target;

    public bool Check()
    {
        // Check if the target is within the set radius
        if (Vector2.Distance(transform.position, Target.transform.position) < _viewRadius)
        {
            float radToCheck = (_angleOffset + transform.parent.transform.eulerAngles.y) * Mathf.Deg2Rad;

            Vector2 directionToTarget = (Target.transform.position - transform.position).normalized;
            Vector2 viewDirection = new(Mathf.Cos(radToCheck) * Scale.x, Mathf.Sin(radToCheck) * Scale.y);

            float angleToTarget = Vector2.Angle(directionToTarget, viewDirection);

            if (angleToTarget < _viewAngle / 2f)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, Target.transform.position);
                // Return true if there are no obstructions between the object and the target
                return !Physics2D.Raycast(transform.position, directionToTarget, distanceToPlayer, _obstacleMask);
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Vector3 rightBoundary = Quaternion.AngleAxis(_viewAngle / 2f + _angleOffset, Vector3.forward) * transform.right * Scale;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, rightBoundary * _viewRadius);

        Vector3 leftBoundary = Quaternion.AngleAxis(-_viewAngle / 2f + _angleOffset, Vector3.forward) * transform.right * Scale;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftBoundary * _viewRadius);

        if (Check())
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Target.transform.position);
        }
    }
}
