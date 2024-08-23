using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginRegister : MonoBehaviour
{
    //[SerializeField] //(Only for variables) Show in editor, not other scripts


    public void OnSubmitLogin()
    {
        Debug.Log("Login");
        SceneManager.LoadScene("Login");
    }

    public void OnSubmitRegister()
    {
        Debug.Log("Register");
        SceneManager.LoadScene("Register"); 
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
