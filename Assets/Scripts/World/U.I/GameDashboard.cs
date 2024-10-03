using UnityEngine;
using UnityEngine.UI;

public class GameDashboard : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Button teleportButton; // Reference to the button in the scene
    [SerializeField] private Transform player; // Reference to the player GameObject
    [SerializeField] private Vector3 teleportCoordinates; // Coordinates to teleport the player to

    void Start()
    {
        // Add a listener to the button to call the Teleport method when clicked
        teleportButton.onClick.AddListener(OnButtonClick);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnButtonClick()
    {
        // Ensure the player reference is set
        if (player != null)
        {
            TeleportPlayer(); // Teleport the player to the specified coordinates
        }
        else
        {
            Debug.LogError("Player reference is not set in the editor.");
        }
    }

    private void TeleportPlayer()
    {
        // Teleport the player to the specified coordinates
        player.position = teleportCoordinates;
        Debug.Log("Player teleported to: " + teleportCoordinates);
    }
}
