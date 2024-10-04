using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WhizKid.Player;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private WorldMenuHandler menuHandler;
    [SerializeField] private PlayerController playerInstance;
    public static bool isPaused;

    void Start()
    {
        isPaused = false;
        menuHandler.EnableIntro();
        playerInstance.enabled = false;
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
                playerInstance.enabled = true;
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
            }
            else
            {
                menuHandler.Pause();
                isPaused = true;
                playerInstance.enabled = false;
            }
        }
    }
}
