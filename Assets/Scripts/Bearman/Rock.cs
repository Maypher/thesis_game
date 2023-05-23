using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Rock : MonoBehaviour, IDamageable
    {
        private Rigidbody2D rb;
        private SpriteRenderer sprite;

        [SerializeField] private int MaxHealth = 3;
        private int health;

        [Header("Fade data")]
        [SerializeField] [Tooltip("Time after rock is static to start fading")] private float timeToFade = 4;
        [SerializeField] private float fadeTime = 1;

        [SerializeField] private AttackDetails attackDetails;

        private float timeToFadeTimer;
        private bool isFading;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();

            isFading = false;
            timeToFadeTimer = 0;

            health = MaxHealth;

            rb.isKinematic = true;
        }

        // Update is called once per frame
        void Update()
        {
            rb.isKinematic = transform.parent;

            if (!isFading && !rb.isKinematic && rb.velocity == Vector2.zero)
            {
                timeToFadeTimer += Time.deltaTime;
                if (timeToFadeTimer > timeToFade) StartCoroutine(FadeOut());
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")) 
            {
                AttackDetails ad = attackDetails;
                ad.attackPostion = collision.GetContact(0).point;

                collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(ad);
            }
        }

        private IEnumerator FadeOut()
        {
            isFading = true;

            // Get initial material color
            Color initialColor = sprite.material.color;
            Color targetColor = initialColor;
            targetColor.a = 0;

            // Start fading out
            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                float normalizedTime = timer / fadeTime;
                Color lerpedColor = Color.Lerp(initialColor, targetColor, normalizedTime);
                sprite.material.color = lerpedColor;
                yield return null;
            }

            // Destroy object once fading is complete
            Destroy(gameObject);
        }

        public void TakeDamage(AttackDetails attackDetails)
        {
            health -= attackDetails.damage;

            if (health <= 0) Kill();
        }
       
        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}