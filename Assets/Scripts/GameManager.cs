using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Player.Player Player { get; private set; }

    public static UserInput UserInput { get; private set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player.Player>();

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UserInput = new();
        UserInput.Player.Enable();
    }
}
