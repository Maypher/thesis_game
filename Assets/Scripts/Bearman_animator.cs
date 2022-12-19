using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearman_animator : MonoBehaviour
{
    private Bearman_movement movement_controller;
    private Animator character_animator;
    private Bearman_combat combat_controller;
    private ParticleSystem punchChargeParticles;
    private ParticleSystem.MainModule punchChargeParticlesSettings;

    private bool isFacingRight = true;

    // Used to time flex animation
    private float idleTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        movement_controller = GetComponent<Bearman_movement>();
        character_animator = GetComponent<Animator>();
        combat_controller = GetComponent<Bearman_combat>();
        punchChargeParticles = GetComponentInChildren<ParticleSystem>();
        punchChargeParticlesSettings = punchChargeParticles.main;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo currentAnimationState = character_animator.GetCurrentAnimatorStateInfo(0);
        character_animator.SetBool("isMoving", movement_controller.HorizontalDirection != 0);
        character_animator.SetBool("isAirborne", !movement_controller.IsGrounded);
        character_animator.SetBool("isAttacking", combat_controller.IsAttacking);
        character_animator.SetBool("chargedAttack", combat_controller.ChargedAttack);

        // Flip character if moving right and facing left or moving left and facing right
        if (movement_controller.CanMove() && 
            ((movement_controller.HorizontalDirection > 0 && !isFacingRight) || (movement_controller.HorizontalDirection < 0 && isFacingRight))) Flip();


        // If the walking animation is less than 50% done skip its transition to idle
        if (movement_controller.HorizontalDirection == 0 && currentAnimationState.IsName("Bearman_walk") &&
            currentAnimationState.normalizedTime % 1 < 0.5) character_animator.CrossFade("Idle", 0, 0);


        // If time is greater than 10 seconds play flex animation
        if (currentAnimationState.IsName("Idle")) idleTime += Time.deltaTime;
        else idleTime = 0;
        character_animator.SetFloat("idleTime", idleTime);

        // Attack animations
        if (combat_controller.IsCharging)
        {
            character_animator.SetBool("isCharging", true);
            if (combat_controller.ChargeTime > 0.5 && !punchChargeParticles.isEmitting) punchChargeParticles.Play();

            // Change particles color depending on charge time
            if (combat_controller.ChargeTime >= 2.5) punchChargeParticlesSettings.startColor = Color.yellow;
            else if (combat_controller.ChargeTime >= 1.5) punchChargeParticlesSettings.startColor = Color.blue;
            else punchChargeParticlesSettings.startColor = Color.white;
        }
        else
        {
            character_animator.SetBool("isCharging", false);
            punchChargeParticles.Stop();
        }

        //Crouching
        if (movement_controller.IsCrouching) character_animator.SetBool("isCrouching", true);
        else character_animator.SetBool("isCrouching", false);
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
}
