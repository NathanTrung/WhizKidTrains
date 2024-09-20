using UnityEngine;
using UnityEngine.SceneManagement; // Import the Scene Management namespace

public class BuildingInteractable : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Name of the scene to load

    public void Interact()
    {
        Debug.Log("Interacting with building...");
        LoadScene();
    }

    private void LoadScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
