using System;
using System.Collections.Generic;
using UnityEngine;
using WhizKid.Player;

public class WorldManager : MonoBehaviour
{
    static public WorldManager Instance;
    static public PlayerController PlayerInstance;

    [SerializeField] private WorldMenuHandler menuHandler;
    [SerializeField] private PlayerController playerInstance;

    public static bool isPaused;

    void Start()
    {
        isPaused = false;
        menuHandler.EnableIntro();
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        playerInstance.enabled = false;
        PlayerInstance = playerInstance;
    }

    void Update()
    {
        if (menuHandler == null || playerInstance == null)
        {
            Debug.LogError("WorldManger Update(): Missing objects");
            return;
        }
        HandleInput();
    }

    void HandleInput()
    {
        // Handle Intro
        if (menuHandler.IsIntroActive())
        {
            Debug.Log("WorldManager HandleInput(): Intro Panel is still active");
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
            {
                menuHandler.DisableIntro();
                Cursor.lockState = CursorLockMode.Locked;
                playerInstance.enabled = true;
                Time.timeScale = 1f;
            }
            return;
        }

        // Handle Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                menuHandler.Unpause();
                isPaused = false;
                playerInstance.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
            else
            {
                menuHandler.Pause();
                isPaused = true;
                playerInstance.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0f;
            }
        }
    }
}

public enum LocationType
{
    SpawnPoint,
    House,
    School,
    Airport,
    Aquarium,
    Cinemas,
}
