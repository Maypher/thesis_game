using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kindly stolen from https://github.com/Comp3interactive/FieldOfView
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] [Range(0, 360)] private float _angle;
    [SerializeField] [Range(0, 725)] private float _angleOffset;

    [HideInInspector] public int FacingDirection = 1;

    [SerializeField] private GameObject _target;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    public bool CanSeeTarget { get { return FieldOfViewCheck(); } }

    private bool FieldOfViewCheck()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, _radius, _targetMask);

        if (rangeCheck)
        {
            Transform target = rangeCheck.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right * FacingDirection, directionToTarget) < _angle * .5f)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask)) return true;
                return false;
            }
            else return false;
        }
        else return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, (-_angle + _angleOffset) * .5f);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, (_angle + _angleOffset) * .5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * _radius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * _radius);

        if (CanSeeTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        if (FacingDirection == -1)
        {
            // If the enemy is facing left, flip the angle horizontally
            angleInDegrees += 180;
        }

        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
