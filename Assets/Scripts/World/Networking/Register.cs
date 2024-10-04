using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField fEmailInputField;
    [SerializeField]
    private TMP_InputField fPasswordInputField;
    [SerializeField]
    private TextMeshProUGUI fErrorText;

    [DllImport("__Internal")]
    public static extern void CreateUserWithEmailAndPassword(string email, string password);

    public void OnSubmitRegister()
    {
        /*
            TO-DO:
                - Include username
                - Retrieve & Store user details
        */
        string lEmail = fEmailInputField.text.Trim();
        string lPassword = fPasswordInputField.text.Trim();
        if (ValidateInput(lEmail, lPassword))
        {
            CreateUserWithEmailAndPassword(lEmail, lPassword);
        }
    }

    // Method to be called by JavaScript code
    private void RegisterUser(int result)
    {
        if (result == 0)
        {
            Debug.Log("Register: Creation Failed");
            fErrorText.text = "Unable to create account";
        }
        else
        {
            Debug.Log("Register: Creation Success");
            SceneManager.LoadScene("World");
        }
    }
    private bool ValidateInput(string email, string password)
    {
        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
        {
            fErrorText.text = "Please enter all required fields.";
            return false;
        }
        
        if (!ValidationService.IsValidEmail(email))
        {
            fErrorText.text = "Please a enter valid email";
            return false;
        }

        if (!ValidationService.IsValidPassword(password))
        {
            fErrorText.text = "Please enter password with 8 or more characters";
            return false;
        }

        Debug.Log("Register: Input Validation Success");
        return true;
    }

    public void removeErrorText()
    {
        fErrorText.text = "";
    }
}
