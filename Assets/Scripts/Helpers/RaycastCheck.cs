using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _target;
    [HideInInspector] public int FacingDirection = 1;

    public bool Check()
    {
        return Physics2D.Raycast(transform.position, transform.right * FacingDirection, _raycastDistance, _target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, _raycastDistance * FacingDirection * transform.right);
    }
}
