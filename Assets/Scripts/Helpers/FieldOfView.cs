using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float _radius;
    [SerializeField] [Range(0, 360)] private float _angle;
    [SerializeField] [Range(0, 360)] private float _angleOffset;


    [SerializeField] private GameObject _target;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    public bool _canSeeTarget { get { return FieldOfViewCheck(); } }

    private bool FieldOfViewCheck()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, _radius, _targetMask);

        if (rangeCheck)
        {
            Transform target = rangeCheck.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, directionToTarget) < _angle / 2)
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

        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, (-_angle + _angleOffset) / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, (_angle + _angleOffset) / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * _radius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * _radius);

        if (_canSeeTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
