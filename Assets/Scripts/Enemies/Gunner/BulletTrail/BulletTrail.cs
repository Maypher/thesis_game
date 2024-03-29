using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Gunner.Misc
{
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifespan = 5;

        private float lifetime = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position += speed * Time.deltaTime * transform.right;

            lifetime += Time.deltaTime;

            if (lifetime >= lifespan) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If colliding with player(3 and 8) or ground(6)
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("InvinsibleToEnemies"))
            {
                Destroy(gameObject);
            }
        }
    }
}
