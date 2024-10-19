using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WhizKid.Player;

/// <summary>
/// Specific to World UI
/// Responsible for displaying UI related events 
/// to the players
/// </summary>
public class WorldMenuHandler : MonoBehaviour
{
    #region Serialized Private Fields
    [SerializeField] private PlayerController playerInstance;

    [Header("Menu Panels")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject gameDashboardPanel;

    [Header("Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Dropdown qualityDropdown;

    #endregion

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(false);
    }

    #region Introduction Methods

    public void EnableIntro()
    {
        introPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
    }
    public void DisableIntro()
    {
        introPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    public bool IsIntroActive() => introPanel.activeSelf;

    #endregion
    #region Pause Methods

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(false);
        Time.timeScale = 1f;

        // playerInstance present in both world manager 
        // require code redesign

        playerInstance.enabled = true;
        WorldManager.isPaused = false;
    }

    public void GameDashboard()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(true);
    }

    #endregion
    #region Settings Panel

    public void Settings()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
        gameDashboardPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality set to: " + qualityIndex);
    }

    #endregion

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
