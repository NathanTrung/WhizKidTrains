#define PRODUCTION
// #define PRODUCTION
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using System;

public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField fEmailInputField;
    [SerializeField]
    private TMP_InputField fPasswordInputField;
    [SerializeField]
    private TextMeshProUGUI fErrorText;
    [DllImport("__Internal")]
    private static extern int SignInWithEmailAndPassword(string email, string password);

    public void OnSubmitLogin()
    {
        string lEmail = fEmailInputField.text.Trim();
        string lPassword = fPasswordInputField.text.Trim();

#if TESTING
        if (ValidateInput(lEmail, lPassword))
        {
            // SignInWithEmailAndPassword("old@gmail.com", "123456789"); // correct
            // SignInWithEmailAndPassword("old@gmail.com", "123456789"); // incorrect
        }
#elif PRODUCTION
        if (ValidateInput(lEmail, lPassword))
        {
            Debug.Log("Login: Input Validation Success");
            SignInWithEmailAndPassword(lEmail, lPassword);
        }
#endif
    }


    // Method to be called by JavaScript code
    private void ValidateAuthentication(int result)
    {
        if (result == 0)
        {
            Debug.Log("Login: Authentication Failed");
            fErrorText.text = "Incorrect email or password";
        }
        else
        {
            Debug.Log("Login: Authentication Success");
            SceneManager.LoadScene("World");
        }
    }

    private bool ValidateInput(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Login: Input Validation Failed");
            fErrorText.text = "Error: Please enter all required fields.";
            return false;
        }

        Debug.Log("Login: Input Validation Success");
        return true;
    }

    public void removeErrorText()
    {
        fErrorText.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
