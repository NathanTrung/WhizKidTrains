using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{

    /*    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text)
        {
            Transform chatBubbleTransform = Instantiate(GameAssets.Instance.pfChatBubble, parent);
            chatBubbleTransform.localPosition = localPosition;

            chatBubbleTransform.GetComponent<ChatBubble>().Setup(iconType, text);

            Destroy(chatBubbleTransform.gameObject, 4f);

        }
    */
    public static void Create(Transform parent, Vector3 relativePosition, IconType iconType, string text)
    {
        // Adjust the position to be above the NPC or player
        Vector3 localPosition = new Vector3(relativePosition.x, relativePosition.y + 2f, relativePosition.z); // Adjust y-offset as needed

        // Log position for debugging
        Debug.Log($"Creating chat bubble at position: {parent.position + localPosition}");

        // Instantiate chat bubble prefab
        Transform chatBubbleTransform = Instantiate(GameAssets.Instance.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;

        // Initialize chat bubble
        ChatBubble chatBubble = chatBubbleTransform.GetComponent<ChatBubble>();
        if (chatBubble != null)
        {
            chatBubble.Setup(iconType, text);
        }
        else
        {
            Debug.LogError("ChatBubble prefab missing ChatBubble component.");
        }

        // Destroy chat bubble after 4 seconds
        Destroy(chatBubbleTransform.gameObject, 4f);
    }

    public enum IconType
    {
        Happy,
        Neutral, 
        Angry,
    }
    [SerializeField] private Sprite happyIconSprite;
    [SerializeField] private Sprite angryIconSprite;
    [SerializeField] private Sprite neutralIconSprite;

    [SerializeField] private Vector2 padding = new Vector2(4f, 2f);
    [SerializeField] private bool useIcon = true;

    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;
    private Camera mainCamera;

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background")?.GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon")?.GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text")?.GetComponent<TextMeshPro>();
        mainCamera = Camera.main;

        if (backgroundSpriteRenderer == null || textMeshPro == null)
        {
            Debug.LogError("ChatBubble: Missing components.");
        }

        if (iconSpriteRenderer == null && useIcon)
        {
            Debug.LogWarning("ChatBubble: Icon usage is enabled, but no Icon object is found.");
        }
    }

  

    private void Setup(IconType iconType, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 adjustedSize = textSize + padding;

        if (backgroundSpriteRenderer != null)
        {
            // Set background size
            backgroundSpriteRenderer.size = adjustedSize + padding;
            Vector3 offset = new Vector2(-2f, 0f);
            float verticalOffset = -0.25f;
            // Position the background to be centered with the text
            backgroundSpriteRenderer.transform.localPosition = new Vector3(adjustedSize.x / 2.37f - textMeshPro.rectTransform.pivot.x * adjustedSize.x, verticalOffset, 0f);
        }

        if (iconSpriteRenderer != null && !useIcon)
        {
             iconSpriteRenderer.gameObject.SetActive(false);
        } else
        {
            iconSpriteRenderer.transform.localPosition = new Vector3(iconSpriteRenderer.transform.localPosition.x + 0.05f, iconSpriteRenderer.transform.localPosition.y, 0.02f);
            iconSpriteRenderer.sprite = GetIconSprite(iconType);
        }

        if (textMeshPro != null && !useIcon)
        {
            textMeshPro.gameObject.SetActive(false);
        } else
        {
            textMeshPro.transform.localPosition = new Vector3(textMeshPro.transform.localPosition.x + 0.3f, textMeshPro.transform.localPosition.y, 0.02f);
        }

    }

    private void Update()
    {
        Vector3 toCamera = mainCamera.transform.position - transform.position;
        float dotProduct = Vector3.Dot(transform.forward, toCamera);

        bool isFacingCamera = dotProduct > 0f;
        textMeshPro.enabled = isFacingCamera;
        iconSpriteRenderer.enabled = isFacingCamera;
        transform.LookAt(mainCamera.transform);
    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Happy:
                return happyIconSprite;
            case IconType.Neutral:
                return neutralIconSprite;
            case IconType.Angry:
                return angryIconSprite;
        }
    }
}

   