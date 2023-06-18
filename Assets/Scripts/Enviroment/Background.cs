using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Transform cam; // Main camera
    Vector3 camStartPos;
    float distance; // distance between the camera start pos and its current pos

    private GameObject[] backgrounds;
    private Material[] mats;
    private float[] backSpeed;

    private float farthestBack = 0;

    [SerializeField][Min(0)] private float parallaxSpeed;

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mats = new Material[backCount];
        backgrounds = new GameObject[backCount];
        backSpeed = new float[backCount];


        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mats[i] = backgrounds[i].GetComponent<Renderer>().material;
        }


        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // Find the furthest back element
        {
            if (backgrounds[i].transform.position.z - cam.position.z > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) // Set the speed of backgrounds
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector2(cam.position.x, transform.position.y);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mats[i].SetTextureOffset("_MainTex", new(distance * speed, 0));
        }
    }
}
