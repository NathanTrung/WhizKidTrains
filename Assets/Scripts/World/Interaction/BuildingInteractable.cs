using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingInteractable : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void Interact()
    {
        Debug.Log("Interacting with building...");
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
