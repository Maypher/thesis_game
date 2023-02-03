using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shockwave
{
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
            _direction = (int)Mathf.Sign(transform.localScale.x);

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

        // Thanks to Seth Funk https://www.youtube.com/watch?v=B2BCnIIV1WE for the code
        private void AlignToGround()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(GetHitboxTopRight(), -Vector2.up, 5f, _whatIsGround);

            // Add _hitbox.bounds.extents.y to hit2D.point.y because setting transform.position places the center of the object at the location
            // making the ShockwaveAttack be halfway inside the ground. This adds an offset to align it base. 
            if (hit2D) transform.position = new Vector3(transform.position.x, hit2D.point.y + _hitbox.bounds.extents.y, transform.position.z);
        }

        private Collider2D CheckIfInsideGround() => Physics2D.OverlapPoint(GetHitboxBottomCenter(), _whatIsGround);

        private Vector2 GetHitboxBottomCenter() => new(_hitbox.bounds.center.x, _hitbox.bounds.min.y);

        private Vector2 GetHitboxTopRight() => new(_hitbox.bounds.max.x, _hitbox.bounds.max.y);

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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(GetHitboxTopRight(), Vector2.down * 5);
        }
    }
}