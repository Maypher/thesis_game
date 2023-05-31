using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lightning : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ParticleSystem lightningPS;
    [SerializeField] private Light2D lightningFlash;

    [Header("Lightning")]
    [SerializeField] private float minLightningTime;
    [SerializeField] private float maxLightningTime;
    private float lightningTimer;

    [Header("Lights")]
    [SerializeField] private float minFlashTime = 0.1f;
    [SerializeField] private float maxFlashTime = 0.4f;
    [SerializeField] private int minFlashTimes = 1;
    [SerializeField] private int maxFlashTimes = 4;

    [Header("SFX")]
    [SerializeField] private AudioSource lightningAudioSource;
    [SerializeField] private AudioClip[] thunderSFX;

    // Start is called before the first frame update
    void Start()
    {
        lightningTimer = Random.Range(minLightningTime, maxLightningTime);
    }

    // Update is called once per frame
    void Update()
    {
        lightningTimer -= Time.deltaTime;

        if (lightningTimer <= 0)
        {
            lightningPS.Play();
            StartCoroutine(FlashLights());
            lightningAudioSource.PlayOneShot(thunderSFX[Mathf.RoundToInt(Random.Range(0, thunderSFX.Length))]);

            lightningTimer = Random.Range(minLightningTime, maxLightningTime);
        }
    }

    private IEnumerator FlashLights()
    {
        int timesToFlash = Mathf.RoundToInt(Random.Range(minFlashTimes, maxFlashTimes));

        while (timesToFlash > 0)
        {
            lightningFlash.intensity = 1;
            yield return new WaitForSeconds(Random.Range(minFlashTime, maxFlashTime));
            lightningFlash.intensity = 0;
            yield return new WaitForSeconds(Random.Range(minFlashTime, maxFlashTime));

            timesToFlash--;
        }
    }
}
