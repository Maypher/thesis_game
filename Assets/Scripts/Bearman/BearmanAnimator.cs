using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearmanAnimator : MonoBehaviour
{
    private BearmanMovement movementController;
    private Animator characterAnimator;
    private BearmanCombat combatController;
    private ParticleSystem punchChargeParticles;
    private ParticleSystem.MainModule punchChargeParticlesSettings;

    private bool isFacingRight = true;

    // Used to time flex animation
    private float idleTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<BearmanMovement>();
        characterAnimator = GetComponent<Animator>();
        combatController = GetComponent<BearmanCombat>();
        punchChargeParticles = GetComponentInChildren<ParticleSystem>();
        punchChargeParticlesSettings = punchChargeParticles.main;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo currentAnimationState = characterAnimator.GetCurrentAnimatorStateInfo(0);
        characterAnimator.SetBool("isMoving", movementController.HorizontalDirection != 0);
        characterAnimator.SetBool("isAirborne", !movementController.IsGrounded);
        characterAnimator.SetBool("isAttacking", combatController.IsAttacking);
        characterAnimator.SetBool("chargedAttack", combatController.ChargedAttack);

        // Flip character if moving right and facing left or moving left and facing right
        if (movementController.CanMove() && 
            ((movementController.HorizontalDirection > 0 && !isFacingRight) || (movementController.HorizontalDirection < 0 && isFacingRight))) Flip();


        // If the walking animation is less than 50% done skip its transition to idle
        if (movementController.HorizontalDirection == 0 && currentAnimationState.IsName("Bearman_walk") &&
            currentAnimationState.normalizedTime % 1 < 0.5) characterAnimator.CrossFade("Idle", 0, 0);


        // If time is greater than 10 seconds play flex animation
        if (currentAnimationState.IsName("Idle")) idleTime += Time.deltaTime;
        else idleTime = 0;
        characterAnimator.SetFloat("idleTime", idleTime);

        // Attack animations
        if (combatController.IsCharging)
        {
            characterAnimator.SetBool("isCharging", true);
            if (combatController.ChargeTime > 0.5 && !punchChargeParticles.isEmitting) punchChargeParticles.Play();

            // Change particles color depending on charge time
            if (combatController.ChargeTime >= 2.5) punchChargeParticlesSettings.startColor = Color.yellow;
            else if (combatController.ChargeTime >= 1.5) punchChargeParticlesSettings.startColor = Color.blue;
            else punchChargeParticlesSettings.startColor = Color.white;
        }
        else
        {
            characterAnimator.SetBool("isCharging", false);
            punchChargeParticles.Stop();
        }

        //Crouching
        if (movementController.IsCrouching) characterAnimator.SetBool("isCrouching", true);
        else characterAnimator.SetBool("isCrouching", false);
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
}
