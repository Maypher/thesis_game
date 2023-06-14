using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Vector2 movementSpeed;
    [SerializeField] private float playerVelocityMultiplier = .1f;

    private Material material;

    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        playerRb = GameManager.Player.Rb;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = (playerRb.velocity.x * playerVelocityMultiplier) * Time.deltaTime * movementSpeed;
        material.mainTextureOffset += offset;
    }
}
