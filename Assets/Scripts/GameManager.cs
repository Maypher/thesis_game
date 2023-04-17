using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Player.Player Player;

    private void Awake()
    {
        Player = FindObjectOfType<Player.Player>();
    }
}
