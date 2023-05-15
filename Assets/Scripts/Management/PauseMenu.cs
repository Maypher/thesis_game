using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [SerializeField] private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        IsPaused = false;

        GameManager.UserInput.Pause.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.UserInput.Pause.PauseGame.triggered) ChangePauseState();
    }

    public void ChangePauseState()
    {
        IsPaused = !IsPaused;


        pauseMenu.SetActive(IsPaused);
        if (IsPaused) GameManager.UserInput.Player.Disable();
        else GameManager.UserInput.Player.Enable();

        Time.timeScale = IsPaused ? 0 : 1;
        GameManager.Instance.SeCursorState(IsPaused, IsPaused ? CursorLockMode.Confined : CursorLockMode.Locked);
        Cursor.visible = IsPaused;
    }
}
