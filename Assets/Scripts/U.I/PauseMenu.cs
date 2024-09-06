using Supercyan.AnimalPeopleSample;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For working with sliders/buttons

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu; // Reference to the Settings Menu UI
    public bool isPaused;
    public static bool isPaused1;
    private SimpleSampleCharacterControl cameraController;

    // Settings options
    public Slider volumeSlider; // Example: For controlling volume
    public Dropdown qualityDropdown; // Example: For changing quality settings

    private void Start()
    {
        cameraController = GetComponent<SimpleSampleCharacterControl>();
        ResumeGame();

        // Initialize settings values if needed
        volumeSlider.onValueChanged.AddListener(SetVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Initialize dropdown with current quality level
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }


    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false); // Hide the settings menu when paused
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;
        isPaused1 = true;
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false); // Hide the settings menu when resuming the game
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        isPaused1 = false;
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    // Method to open the settings menu from the pause menu
    public void OpenSettingsMenu()
    {
        pauseMenu.SetActive(false); // Hide the pause menu
        settingsMenu.SetActive(true); // Show the settings menu
    }

    // Method to go back to the pause menu from settings
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false); // Hide the settings menu
        pauseMenu.SetActive(true); // Show the pause menu
    }

    // Example methods for settings
    public void SetVolume(float volume)
    {
        // Adjust game volume
        AudioListener.volume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        // Adjust game quality
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality set to: " + qualityIndex);
    }
}
