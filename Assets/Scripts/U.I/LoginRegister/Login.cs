using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField fEmailInputField;
    [SerializeField]
    private TMP_InputField fPasswordInputField;
    [SerializeField]
    private TextMeshProUGUI fErrorText;

    [DllImport("__Internal")]
    public static extern void SignInWithEmailAndPassword(string email, string password);

    public void OnSubmitLogin()
    {
        string lEmail = fEmailInputField.text;
        string lPassword = fPasswordInputField.text;

        string lCheckUserInfo = checkUserInfo(lEmail, lPassword);
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

    private string checkUserInfo(string email, string password)
    {
        string lResult = "";
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
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
        SignInWithEmailAndPassword("old@gmail.com", "123456789");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
