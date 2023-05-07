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
        private bool moving = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (moving) transform.position += transform.right * speed * Time.deltaTime;

            lifetime += Time.deltaTime;

            if (lifetime >= lifespan) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If colliding with player(3 and 8) or ground(6)
            if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
            {
                moving = false;
                // Spawn gushot particles
            }
        }
    }
}
