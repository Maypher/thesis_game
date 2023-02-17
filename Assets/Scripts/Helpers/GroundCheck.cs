using UnityEngine;

public class GroundCheck: MonoBehaviour
{
    public enum CheckType { Circle, Box }

    public CheckType _checkType = CheckType.Circle;

    [SerializeField] private float _checkRadius = 0.3f;

    [SerializeField] private Vector2 _dimensions = new Vector2(.3f, .3f);

    [SerializeField] private LayerMask _whatIsGround;

    public bool Check()
    {
        if (_checkType == CheckType.Circle) return Physics2D.OverlapCircle(transform.position, _checkRadius, _whatIsGround);
        else return Physics2D.OverlapBox(transform.position, _dimensions, 0, _whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;

        if (_checkType == CheckType.Circle) Gizmos.DrawWireSphere(transform.position, _checkRadius);
        else if (_checkType == CheckType.Box) Gizmos.DrawWireCube(transform.position, _dimensions);
    }
}
