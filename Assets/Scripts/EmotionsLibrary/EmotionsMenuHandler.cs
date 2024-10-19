using System;
using EmotionsLibrary.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Specific to World UI
/// Responsible for displaying UI related events 
/// to the players
/// </summary>
public class EmotionsMenuHandler : MonoBehaviour
{
    #region Serialized Private Fields
    [SerializeField] private Player playerInstance;

    [Header("Menu Panels")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;

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
    }

    #region Pause Methods

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
        playerInstance.enabled = false;
    }

    public void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        playerInstance.enabled = true;
    }

    public void GameDashboard()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
    }

    #endregion
    #region Settings Panel

    public void Settings()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
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
