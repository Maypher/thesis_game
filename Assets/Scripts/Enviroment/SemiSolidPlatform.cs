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

    private Collider2D hitbox;


    void Start()
    {
        timer = 0;

        hitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.UserInput.Player.Crouch.IsPressed())
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
        hitbox.enabled = false;
        yield return new WaitForSeconds(disabledTime);
        hitbox.enabled = true;
    }
}
