using Supercyan.AnimalPeopleSample;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroductionWindow : MonoBehaviour
{
    public GameObject introductionPanel; // Assign this in the Inspector
    public Button okButton; // Assign this in the Inspector
    private SimpleSampleCharacterControl cameraController;
    public bool isPaused;
    public static bool isPaused1;



    private bool isWindowActive = true; // Track if the introduction panel is active

    void Start()
    {
        // Ensure the introduction panel is active at the start
        if (introductionPanel != null)
        {
            introductionPanel.SetActive(true);
        }

        // Pause the game
        if (cameraController == null)
        {
            cameraController = FindObjectOfType<SimpleSampleCharacterControl>();
        }
        // Pause the game
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        Debug.Log("Game paused. Time.timeScale set to 0.");

        // Set up button listener
        if (okButton != null)
        {
            okButton.onClick.AddListener(OnOkButtonClick);
        }
        else
        {
            Debug.LogWarning("OK Button is not assigned.");
        }
    }

    void Update()
    {

        // Check for any key press to dismiss the introduction window
        if (isWindowActive && Input.GetKeyDown(KeyCode.Escape) || isWindowActive && Input.GetKeyDown(KeyCode.Space))
        {
            OnOkButtonClick();

        }
    }

    private void OnOkButtonClick()
    {
        // Hide the introduction panel and resume the game
        if (introductionPanel != null)
        {
            introductionPanel.SetActive(false);
        }
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f; // Resume the game
        Debug.Log("Game resumed. Time.timeScale set to 1.");

        // Update the flag to indicate the window is no longer active
        isWindowActive = false;
    }
}
