using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Material waterMaterial;

    [SerializeField] private float scrollSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        waterMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        waterMaterial.SetTextureOffset("_MainTex", new(Time.time * scrollSpeed, 0));
    }
}
