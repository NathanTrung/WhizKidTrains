using UnityEngine;

public class EmotionPicker : MonoBehaviour
{
    public Transform teleportLocation; // Single teleport location
    public GameObject emotionPanel; // Reference to the EmotionPanel
    public VideoSelector videoSelector; // Reference to the VideoSelector script

    private bool isPanelVisible = false; // Track the panel's visibility state

    private void Start()
    {
        HideEmotionPanel(); // Ensure the panel is hidden initially

        // Ensure videoSelector is assigned
        if (videoSelector == null)
        {
            Debug.LogError("VideoSelector is not assigned!");
        }
    }

    private void Update()
    {
        // Check if videoSelector or videoPlayer is null
        if (videoSelector == null || videoSelector.videoPlayer == null)
        {
            Debug.LogError("VideoSelector or VideoPlayer is not assigned!");
            return; // Prevent further execution
        }

        // Ensure the panel is hidden if the video is playing
        if (videoSelector.videoPlayer.isPlaying && isPanelVisible)
        {
            HideEmotionPanel(); // Hide the panel if the video starts playing
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Only show the panel if the video is not playing
            if (videoSelector != null && !videoSelector.videoPlayer.isPlaying)
            {
                ShowEmotionPanel();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideEmotionPanel(); // Hide the panel when the player exits the trigger area
        }
    }

    public void SelectEmotion(int segmentIndex)
    {
        if (teleportLocation != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Debug.Log("Teleporting Player to location");
                CharacterController playerController = player.GetComponent<CharacterController>();
                if (playerController != null)
                {
                    playerController.enabled = false; // Disable controller before teleporting
                    player.transform.position = teleportLocation.position; // Set position
                    playerController.enabled = true; // Re-enable controller after teleporting
                }
                else
                {
                    player.transform.position = teleportLocation.position; // Fallback to direct transform movement
                }
                HideEmotionPanel(); // Hide the panel after teleporting
            }
        }
        else
        {
            Debug.LogError("Teleport location is not assigned.");
        }
    }

    private void ShowEmotionPanel()
    {
        if (emotionPanel != null)
        {
            emotionPanel.SetActive(true); // Show the panel
            isPanelVisible = true; // Track visibility state
        }
    }

    private void HideEmotionPanel()
    {
        if (emotionPanel != null)
        {
            emotionPanel.SetActive(false); // Hide the panel
            isPanelVisible = false; // Track visibility state
        }
    }
}
