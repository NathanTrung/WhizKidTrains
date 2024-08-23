using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find existing instance in the scene
                _instance = FindObjectOfType<GameAssets>();

                // If no instance found, create a new one
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameAssets).Name);
                    _instance = singletonObject.AddComponent<GameAssets>();

                    // Optionally, make it persistent across scenes
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    public Transform pfChatBubble;

    private void Awake()
    {
        // Check if instance already exists and destroy this object if it does
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (pfChatBubble == null)
        {
            Debug.LogError("pfChatBubble is not assigned in GameAssets.");
        }
    }
}
