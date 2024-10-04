using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WhizKid.Player;

public class WorldMenuHandler : MonoBehaviour
{
    #region Serialized Private Fields
    [SerializeField] private PlayerController playerInstance;

    [Header("Menu Panels")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject gameDashboardPanel;
    [SerializeField] private GameObject settingsMenuPanel;

    [Header("Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Dropdown qualityDropdown;
    #endregion

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(false);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    #region Introduction Methods

    public void EnableIntro() => introPanel.SetActive(true);
    public void DisableIntro() => introPanel.SetActive(false);
    public bool IsIntroActive() => introPanel.activeSelf;

    #endregion
    #region Pause Methods

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        gameDashboardPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
    }

    public void Unpause()
    {
        pauseMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);

        // playerinstance present in both world manager 
        // require code redesign

        playerInstance.enabled = true; 
        WorldManager.isPaused = false;
    }

    public void GameDashboard()
    {
        pauseMenuPanel.SetActive(false);
        gameDashboardPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
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
