using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToWorld : MonoBehaviour
{
    [SerializeField]
    private string _sceneName; //might be bad but i want to set this from the editor

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
        }
    }
}
