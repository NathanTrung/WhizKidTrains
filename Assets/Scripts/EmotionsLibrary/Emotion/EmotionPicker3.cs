using System.Collections.Generic; // Add this line
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmotionPicker3 : MonoBehaviour, IPointerClickHandler
{
    public Image emotionImage; // The image component
    public Text emotionText; // Reference to the Text component for displaying emotion names
    public Transform teleportLocation; // Location to teleport players
    public GameObject emotionPanel; // The emotion panel to hide after teleportation
    public Button teleportButton; // The button to trigger teleportation
    public GameObject markerPrefab; // Prefab for the marker (O) to be instantiated
    private GameObject[] markerInstances; // Array of marker instances

    // Updated to allow for pre-initialized selections
    public List<string> emotionOptions1; // Options for the first dropdown
    public List<string> emotionOptions2; // Options for the second dropdown
    public List<string> emotionOptions3; // Options for the third dropdown

    private string selectedEmotion; // Store the selected emotion

    // Define your updated emotions matrix
    private string[,] emotions = {
        { "Rage", "Terror", "Loathing", "", "Disgust", "Grief", "Disapproval", "Bitterness", "", "" },
        { "Fearfulness", "Contempt", "Resentment", "Annoyance", "Apprehension", "", "Shame", "Embarrassment", "Disdain", "Skepticism" },
        { "Confusion", "Distraction", "Pensiveness", "Boredom", "Melancholy", "Nostalgia", "Regret", "", "Submission", "Remorse" },
        { "Amazement", "Awe", "Fascination", "Interest", "Curiosity", "", "Hope", "Optimism", "", "" },
        { "Admiration", "Trust", "Acceptance", "Love", "Pride", "Happiness", "Euphoria", "Ecstasy", "Relief", "Serenity" },
        { "Calmness", "Vigilance", "Excitement", "Enthusiasm", "Joy", "Sadness", "Fear", "Surprise", "Kindness", "Awe" },
        { "", "Worry", "Anguish", "Doubt", "Anger", "Sorrow", "Longing", "Empathy", "", "" },
        { "", "Apathy", "Reluctance", "Despair", "", "", "", "", "", "" },
        { "", "Hope", "", "Elation", "", "", "", "", "", "Gratitude" },
        { "Anticipation", "", "Disappointment", "Satisfaction", "Guilt", "", "", "", "", "" }
    };

    void Start()
    {
        // Disable the button at the start
        teleportButton.interactable = false;

        // Add listener to the teleport button
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(TeleportAnyPlayer);
        }

        // Create and hide the marker instances
        markerInstances = new GameObject[4]; // 4 markers (1 user selected + 3 dropdowns)
        for (int i = 0; i < markerInstances.Length; i++)
        {
            markerInstances[i] = Instantiate(markerPrefab, emotionImage.transform);
            markerInstances[i].SetActive(false); // Initially hide all markers
        }

        // Initialize dropdowns with pre-defined options
        InitializeDropdowns();
    }

    private void InitializeDropdowns()
    {
        // Clear existing options and set up dropdowns with predefined options
        for (int i = 0; i < markerInstances.Length; i++)
        {
            Dropdown dropdown = markerInstances[i].GetComponent<Dropdown>();
            if (dropdown != null)
            {
                dropdown.ClearOptions();
                List<string> options = new List<string>();

                // Assign options based on the dropdown index
                switch (i)
                {
                    case 0:
                        options = emotionOptions1;
                        break;
                    case 1:
                        options = emotionOptions2;
                        break;
                    case 2:
                        options = emotionOptions3;
                        break;
                }

                dropdown.AddOptions(options);
                dropdown.onValueChanged.AddListener(delegate { UpdateDropdownMarker(i); });
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(emotionImage.rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        SelectEmotion(localPoint); // Select the emotion when clicked

        // If a valid emotion is selected, update the text and enable the teleport button
        if (!string.IsNullOrEmpty(selectedEmotion))
        {
            emotionText.text = selectedEmotion; // Update the text to show the selected emotion
            teleportButton.interactable = true; // Enable the teleport button
            UpdateMarkerPosition(localPoint, 0); // Update user-selected marker position
        }
        else
        {
            emotionText.text = ""; // Clear the text if the selection is invalid
            teleportButton.interactable = false; // Disable the teleport button
            markerInstances[0].SetActive(false); // Hide the user-selected marker if no valid selection
        }
    }

    private void SelectEmotion(Vector2 localPoint)
    {
        float normalizedX = Mathf.Clamp01((localPoint.x + emotionImage.rectTransform.rect.width / 2) / emotionImage.rectTransform.rect.width);
        float normalizedY = Mathf.Clamp01((localPoint.y + emotionImage.rectTransform.rect.height / 2) / emotionImage.rectTransform.rect.height);

        // Clamp the index to ensure it stays between 0 and 9
        int emotionIndexX = Mathf.Clamp(Mathf.FloorToInt(normalizedX * 10), 0, 9);
        int emotionIndexY = Mathf.Clamp(Mathf.FloorToInt(normalizedY * 10), 0, 9);

        selectedEmotion = emotions[emotionIndexY, emotionIndexX];

        // Check if the selected emotion is empty
        if (string.IsNullOrEmpty(selectedEmotion))
        {
            Debug.Log("Selected Emotion is empty.");
            selectedEmotion = ""; // Clear the selected emotion if it's empty
        }
        else
        {
            Debug.Log("Selected Emotion: " + selectedEmotion);
        }
    }

    private void UpdateMarkerPosition(Vector2 localPoint, int markerIndex)
    {
        // Set the marker's anchored position directly to the localPoint
        markerInstances[markerIndex].GetComponent<RectTransform>().anchoredPosition = localPoint;
        markerInstances[markerIndex].SetActive(true); // Show the marker
    }

    private void UpdateDropdownMarker(int dropdownIndex)
    {
        // Get the selected index from the dropdown
        Dropdown dropdown = markerInstances[dropdownIndex].GetComponent<Dropdown>();
        int selectedIndex = dropdown.value;
        string selectedDropdownEmotion = dropdown.options[selectedIndex].text;

        // Find the corresponding emotion in the matrix
        int emotionIndexX = -1;
        int emotionIndexY = -1;

        for (int y = 0; y < emotions.GetLength(0); y++)
        {
            for (int x = 0; x < emotions.GetLength(1); x++)
            {
                if (emotions[y, x] == selectedDropdownEmotion)
                {
                    emotionIndexX = x;
                    emotionIndexY = y;
                    break;
                }
            }
            if (emotionIndexX != -1 && emotionIndexY != -1)
            {
                break;
            }
        }

        if (emotionIndexX != -1 && emotionIndexY != -1)
        {
            // Calculate the position based on the selected emotion's index
            float markerX = (emotionIndexX + 0.5f) / emotions.GetLength(1) * emotionImage.rectTransform.rect.width - markerInstances[dropdownIndex].GetComponent<RectTransform>().rect.width / 2 - 150f; // Offset 5 to the right
            float markerY = (emotionIndexY + 0.5f) / emotions.GetLength(0) * emotionImage.rectTransform.rect.height - markerInstances[dropdownIndex].GetComponent<RectTransform>().rect.height / 2 - 150f; // Offset 5 down

            // Set the marker position
            markerInstances[dropdownIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(markerX, markerY);
            markerInstances[dropdownIndex].SetActive(true); // Show the marker
        }
        else
        {
            Debug.LogWarning("Selected emotion from dropdown not found in the emotions matrix.");
        }
    }

    private void TeleportAnyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (teleportLocation != null && player != null)
        {
            Debug.Log("Teleporting Player to location with emotion: " + selectedEmotion);
            CharacterController playerController = player.GetComponent<CharacterController>();
            Vector3 teleportPosition = teleportLocation.position;

            // Check if the player has a CharacterController
            if (playerController != null)
            {
                playerController.enabled = false; // Disable the CharacterController temporarily
                player.transform.position = teleportPosition; // Teleport the player
                playerController.enabled = true; // Re-enable the CharacterController
            }

            // Hide the emotion panel after teleportation
            if (emotionPanel != null)
            {
                emotionPanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Teleport location or player not found.");
        }
    }
}
