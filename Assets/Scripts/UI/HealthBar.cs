using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Face container")]
    [SerializeField] Image faceImg;

    [Header("Face images")]
    [SerializeField] Sprite fullHealthFace;
    [SerializeField] Sprite halfHealthFace;
    [SerializeField] Sprite lowHealthFace;

    [Header("Face images hatless")]
    [SerializeField] Sprite fullHealthFaceHatless;
    [SerializeField] Sprite halfHealthFaceHatless;
    [SerializeField] Sprite lowHealthFaceHatless;

    [Header("Life")]
    [SerializeField] private Image heartPrefab;

    private Transform heartContainer;

    private void Awake()
    {
        heartContainer = transform.Find("HeartContainer");
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts();

        GameManager.Player.DamageTaken += UpdateHearts;
        GameManager.Player.DamageTaken += UpdateFace;
    }

    private void UpdateHearts()
    {
        foreach (Transform child in heartContainer.transform) Destroy(child.gameObject);

        for (int i = 0; i < GameManager.Player.Health; i++)
        {
            GameObject.Instantiate(heartPrefab, heartContainer);
        }
    }

    private void UpdateFace()
    {
        float healthPercentage = GameManager.Player.Health / GameManager.Player.MaxHealth;

        if (!GameManager.Player.Raccoon)
        {
            if (healthPercentage < 0.33) faceImg.sprite = lowHealthFace;
            else if (healthPercentage < 0.66) faceImg.sprite = halfHealthFace;
            else faceImg.sprite = fullHealthFace;
        }
        else
        {
            if (healthPercentage < 0.33) faceImg.sprite = lowHealthFaceHatless;
            else if (healthPercentage < 0.66) faceImg.sprite = halfHealthFaceHatless;
            else faceImg.sprite = fullHealthFaceHatless;
        }
    }


}
