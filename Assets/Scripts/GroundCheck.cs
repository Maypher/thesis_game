using UnityEngine;

public class GroundCheck: MonoBehaviour
{
    [SerializeField] private float _checkRadius = 0.3f;
    [SerializeField] private LayerMask _whatIsGround;

    public bool Check()
    {
        return Physics2D.OverlapCircle(transform.position, _checkRadius, _whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
