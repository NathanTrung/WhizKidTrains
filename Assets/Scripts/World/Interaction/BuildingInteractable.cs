using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingInteractable : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void Interact()
    {
        Debug.Log("Interacting with building...");
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        yield return null; // REQUIRED: yield a frame to wait for next scene to load
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        SceneManager.UnloadSceneAsync("World");
    }
}
