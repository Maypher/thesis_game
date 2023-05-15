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
        UserInput = new();

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
    }

    public void SeCursorState(bool visible, CursorLockMode lockState)
    {
        Cursor.visible = visible;
        Cursor.lockState = lockState;
    }
}
