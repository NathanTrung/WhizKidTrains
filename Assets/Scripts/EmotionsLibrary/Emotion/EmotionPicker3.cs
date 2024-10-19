using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmotionPicker3 : MonoBehaviour, IPointerClickHandler
{
    public Image emotionImage;
    public Text emotionText;
    public Transform teleportLocation;
    public GameObject emotionPanel;
    public Button teleportButton;
    public GameObject markerPrefab;

    private GameObject[] markerInstances;
    public List<string> emotionOptions1;
    public List<string> emotionOptions2;
    public List<string> emotionOptions3;

    private string selectedEmotion;
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
        teleportButton.interactable = false;

        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(TeleportAnyPlayer);
        }

        markerInstances = new GameObject[4];
        for (int i = 0; i < markerInstances.Length; i++)
        {
            markerInstances[i] = Instantiate(markerPrefab, emotionImage.transform);
            markerInstances[i].SetActive(false);
        }

        InitializeDropdowns();
    }

    private void InitializeDropdowns()
    {
        for (int i = 0; i < markerInstances.Length; i++)
        {
            Dropdown dropdown = markerInstances[i].GetComponent<Dropdown>();
            if (dropdown != null)
            {
                dropdown.ClearOptions();
                List<string> options = i switch
                {
                    0 => emotionOptions1,
                    1 => emotionOptions2,
                    2 => emotionOptions3,
                    _ => new List<string>()
                };
                dropdown.AddOptions(options);
                dropdown.onValueChanged.AddListener(delegate { UpdateDropdownMarker(i); });
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(emotionImage.rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        SelectEmotion(localPoint);

        if (!string.IsNullOrEmpty(selectedEmotion))
        {
            emotionText.text = selectedEmotion;
            teleportButton.interactable = true;
            UpdateMarkerPosition(localPoint, 0);
        }
        else
        {
            ResetSelection(); // Reset if no valid emotion is selected
        }
    }

    private void SelectEmotion(Vector2 localPoint)
    {
        float normalizedX = Mathf.Clamp01((localPoint.x + emotionImage.rectTransform.rect.width / 2) / emotionImage.rectTransform.rect.width);
        float normalizedY = Mathf.Clamp01((localPoint.y + emotionImage.rectTransform.rect.height / 2) / emotionImage.rectTransform.rect.height);

        int emotionIndexX = Mathf.Clamp(Mathf.FloorToInt(normalizedX * 10), 0, 9);
        int emotionIndexY = Mathf.Clamp(Mathf.FloorToInt(normalizedY * 10), 0, 9);

        selectedEmotion = emotions[emotionIndexY, emotionIndexX];
        if (string.IsNullOrEmpty(selectedEmotion))
        {
            Debug.Log("Selected Emotion is empty.");
            selectedEmotion = "";
        }
        else
        {
            Debug.Log("Selected Emotion: " + selectedEmotion);
        }
    }

    private void UpdateMarkerPosition(Vector2 localPoint, int markerIndex)
    {
        markerInstances[markerIndex].GetComponent<RectTransform>().anchoredPosition = localPoint;
        markerInstances[markerIndex].SetActive(true);
    }

    private void UpdateDropdownMarker(int dropdownIndex)
    {
        Dropdown dropdown = markerInstances[dropdownIndex].GetComponent<Dropdown>();
        int selectedIndex = dropdown.value;
        string selectedDropdownEmotion = dropdown.options[selectedIndex].text;

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
            if (emotionIndexX != -1) break;
        }

        if (emotionIndexX != -1 && emotionIndexY != -1)
        {
            float markerX = (emotionIndexX + 0.5f) / emotions.GetLength(1) * emotionImage.rectTransform.rect.width - 150f;
            float markerY = (emotionIndexY + 0.5f) / emotions.GetLength(0) * emotionImage.rectTransform.rect.height - 150f;

            markerInstances[dropdownIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(markerX, markerY);
            markerInstances[dropdownIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Selected emotion not found in the matrix.");
        }
    }

    private void TeleportAnyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (teleportLocation != null && player != null)
        {
            Debug.Log("Teleporting Player with emotion: " + selectedEmotion);
            CharacterController playerController = player.GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false;
                player.transform.position = teleportLocation.position;
                playerController.enabled = true;
            }

            if (emotionPanel != null)
            {
                emotionPanel.SetActive(false);
            }

            ResetSelection(); // Reset after teleporting
        }
        else
        {
            Debug.LogWarning("Teleport location or player not found.");
        }
    }

    private void ResetSelection()
    {
        selectedEmotion = "";
        emotionText.text = "";
        teleportButton.interactable = false;

        foreach (GameObject marker in markerInstances)
        {
            marker.SetActive(false);
        }
    }
}
