using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject loginRegisterPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject settingsPanel;

    void Start()
    {
        Time.timeScale = 1f;
        mainPanel.SetActive(true);
        loginRegisterPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void PlayGame()
    {
        mainPanel.SetActive(false);
        loginRegisterPanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        loginRegisterPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Login()
    {
        mainPanel.SetActive(false);
        loginRegisterPanel.SetActive(false);
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void Register()
    {
        mainPanel.SetActive(false);
        loginRegisterPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void Main()
    {
        mainPanel.SetActive(true);
        loginRegisterPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
