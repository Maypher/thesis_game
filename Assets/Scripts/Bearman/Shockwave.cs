using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{

    [Header("Size")]
    [SerializeField] private float _maxSize;
    [SerializeField] private float _lifetime;
    [SerializeField] private AnimationCurve _sizeOverLifetime; // Normalized

    [Header("Movement")]
    [SerializeField] private AnimationCurve _speedOverLifeime;
    [SerializeField] private float _maxSpeed;
    
    [Header("Misc")]
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] [Range(0, 1)] private float _darkenedAmount;

    private int _damage;
    private float _currentLifetime;
    private BoxCollider2D _hitbox;
    private Rigidbody2D _rbd2;
    private SpriteRenderer _sprite;

    private int _direction;

    void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
        _rbd2 = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        
        _currentLifetime = 0;
        _direction = (int) Mathf.Sign(transform.localScale.x);

        SetToGroundColor();
        AlignToGround();
        Destroy(gameObject, _lifetime); // Instead of checking every frame if the element should be destroyed just delay it
    }

    // Update is called once per frame
    void Update()
    {
        _currentLifetime += Time.deltaTime / _lifetime;

        SetSize();
        AlignToGround();

        SetXSpeed(_speedOverLifeime.Evaluate(_currentLifetime) * _maxSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(_damage);
    }

    private void AlignToGround()
    {
        // When shock wave is instantiated it gets spawned inside the ground. This is used to push it out
        Collider2D insideGround = CheckIfInsideGround();

        // Used for when outside ground and should be moved down
        float distanceToGround = GetDistanceToGround();

        if (insideGround != null)
        {
            float distanceToBorder = insideGround.bounds.max.y - _hitbox.bounds.min.y;
            transform.position = new Vector3(transform.position.x, transform.position.y + distanceToBorder, transform.position.z);
        }
        else if (distanceToGround != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - distanceToGround, transform.position.z);
        }
    }

    private Collider2D CheckIfInsideGround() => Physics2D.OverlapPoint(GetHitboxBottomCenter(), _whatIsGround);

    private float GetDistanceToGround()
    {
        return Physics2D.Raycast(GetHitboxBottomCenter(), Vector2.down, 5, _whatIsGround).distance;
    }

    private Vector2 GetHitboxBottomCenter() => new(_hitbox.bounds.center.x, _hitbox.bounds.min.y);

    private void SetXSpeed(float speed) => _rbd2.velocity = new Vector2(speed * _direction, _rbd2.velocity.y);

    private void SetToGroundColor() 
    {
        Collider2D ground = CheckIfInsideGround();

        if (ground != null)
        {
            Color groundColor = ground.GetComponent<SpriteRenderer>().color;
            _sprite.color = new Color(groundColor.r - _darkenedAmount, groundColor.g - _darkenedAmount, groundColor.b - _darkenedAmount);
        }
    }

    private void SetSize()
    {
        float currentSize = _maxSize * _sizeOverLifetime.Evaluate(_currentLifetime);

        transform.localScale = new Vector3(currentSize * _direction, currentSize, currentSize);
    }

    public void SetDamage(int damage) => _damage = damage;
}
