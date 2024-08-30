using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField fEmailInputField;
    [SerializeField]
    private TMP_InputField fPasswordInputField;
    [SerializeField]
    private TMP_InputField fUsernameInputField;
    [SerializeField]
    private TextMeshProUGUI fErrorText;

    // Firebase Auth JSCalls
    [DllImport("__Internal")]
    public static extern void CreateUserWithEmailAndPassword(string email, string password);

    public void OnSubmitRegister()
    {
        string lEmail = fEmailInputField.text;
        string lPassword = fPasswordInputField.text;
        string lUsername = fUsernameInputField.text;

        string lCheckUserInfo = checkUserInfo(lEmail, lPassword, lUsername);
        if (string.IsNullOrEmpty(lCheckUserInfo))
        {
            Debug.Log("Logged In.");
            SceneManager.LoadScene("World");
        }
        else
        {
            Debug.LogError(lCheckUserInfo);
            fErrorText.text = lCheckUserInfo;
        }
    }

    private string checkUserInfo(string email, string password, string username)
    {
        string lResult = "";
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
        {
            lResult = "Error: Please enter all required fields.";
        }
        else {
            lResult = "";
        }

        return lResult;
    }

    public void removeErrorText()
    {
        fErrorText.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateUserWithEmailAndPassword("new@gmail.com", "987654321");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
