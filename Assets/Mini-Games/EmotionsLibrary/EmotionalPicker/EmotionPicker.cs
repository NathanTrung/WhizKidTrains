using UnityEngine;

public class EmotionPicker : MonoBehaviour
{
    public Transform teleportLocation; // Single teleport location
    public GameObject emotionPanel; // Reference to the EmotionPanel
    public VideoSelector videoSelector; // Reference to the VideoSelector script

    private void Start()
    {
        emotionPanel.SetActive(false); // Ensure the panel is hidden initially

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

        // Only allow activation if the video is not playing
        if (!videoSelector.videoPlayer.isPlaying)
        {
            // Panel activation is handled in OnTriggerEnter
        }
        else
        {
            emotionPanel.SetActive(false); // Ensure the panel remains hidden while the video is playing
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Only show the panel if the video has ended
            if (videoSelector != null && videoSelector.videoPlayer != null && !videoSelector.videoPlayer.isPlaying)
            {
                emotionPanel.SetActive(true); // Show the panel when the player enters the trigger area
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            emotionPanel.SetActive(false); // Hide the panel when the player leaves the trigger area
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
                emotionPanel.SetActive(false); // Hide the panel after teleporting
            }
        }
        else
        {
            Debug.LogError("Teleport location is not assigned.");
        }
    }
}
