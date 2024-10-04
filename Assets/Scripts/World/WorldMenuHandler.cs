using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WhizKid.Player;

public class WorldMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private PlayerController playerInstance;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    #region Introduction Methods

    public void EnableIntro()
    {
        introPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
    }

    public void DisableIntro()
    {
        introPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public bool IsIntroActive()
    {
        return introPanel.activeSelf;
    }

    #endregion
    #region Pause Methods

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Unpause()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);

        // playerinstance present in both world manager 
        // require code redesign

        playerInstance.enabled = true; 
        Cursor.lockState = CursorLockMode.Locked;
        WorldManager.isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OpenSettingsPanel()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        settingsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
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
}
