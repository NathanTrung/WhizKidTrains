using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private Transform chatBubbleParent; // The transform to attach the chat bubble to
    [SerializeField] private Vector3 chatBubbleOffset = new Vector3(0, 2, 0); // Offset to position the chat bubble
    [SerializeField] private ChatBubble.IconType chatBubbleIcon = ChatBubble.IconType.Happy; // Default icon type
    [SerializeField] private string npcDialogue1;
    [SerializeField] private string npcDialogue2;
    [SerializeField] private float delayBeforeSecondDialogue = 4f;

    public void Interact()
    {
        Debug.Log("Interacted with NPC");

        // Trigger chat bubble upon interaction
        StartCoroutine(ShowDialogues());
    }

    private IEnumerator ShowDialogues()
    {
        Vector3 chatBubblePosition = new Vector3(-1, 0.7f, -1); // Adjust as needed
        ChatBubble.Create(transform, chatBubblePosition, chatBubbleIcon, npcDialogue1);
        string message = "Dialogue = " + npcDialogue1;
        Debug.Log(message);
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeSecondDialogue);
        Debug.Log(message);
        // Show the second chat bubble
        ChatBubble.Create(transform, chatBubblePosition, chatBubbleIcon, npcDialogue2);
        message = "Dialogue = " + npcDialogue2;
        Debug.Log(message);
    }

    
    
}
