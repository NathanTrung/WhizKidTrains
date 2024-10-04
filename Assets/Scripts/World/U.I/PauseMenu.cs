using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu; // Reference to the Settings Menu UI
    [SerializeField] private Slider volumeSlider; // Example: For controlling volume
    [SerializeField] private Dropdown qualityDropdown; // Example: For changing quality settings
    public bool isPaused;

    private void Start()
    {
        ResumeGame();
        volumeSlider.onValueChanged.AddListener(SetVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
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
        Time.timeScale = 0f; // Enable for single player
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false); // Hide the settings menu when resuming the game
        Time.timeScale = 1f; // Enable for single player
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
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
