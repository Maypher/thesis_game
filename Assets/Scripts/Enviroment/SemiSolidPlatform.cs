using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlatformEffector2D))]
public class SemiSolidPlatform : MonoBehaviour
{
    [SerializeField] private float holdTime = .5f;
    [SerializeField] private float disabledTime = .6f;
    private float timer;

    private bool playerOnTop;

    private Collider2D hitbox;


    void Start()
    {
        timer = 0;

        hitbox = GetComponent<Collider2D>();

        playerOnTop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.UserInput.Player.Crouch.IsPressed() && playerOnTop)
        {
            timer += Time.deltaTime;

            if (timer >= holdTime) 
            {
                StartCoroutine(DisableCollision());
                timer = 0;
            }
        }
    }

    private IEnumerator DisableCollision()
    {
        Physics2D.IgnoreCollision(hitbox, GameManager.Player.GetComponent<Collider2D>());
        yield return new WaitForSeconds(disabledTime);
        Physics2D.IgnoreCollision(hitbox, GameManager.Player.GetComponent<Collider2D>(), false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerOnTop = collision.collider.CompareTag("Player");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerOnTop = collision.collider.CompareTag("Player");
    }
}
