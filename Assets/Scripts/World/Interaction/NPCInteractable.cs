using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private Transform chatBubbleParent; // The transform to attach the chat bubble to
    [SerializeField] private Vector3 chatBubbleOffset = new Vector3(0, 2, 0); // Offset to position the chat bubble
    [SerializeField] private ChatBubble.IconType chatBubbleIcon = ChatBubble.IconType.Happy; // Default icon type

    [SerializeField] private List<string> npcDialogues; // List to hold dialogue lines

    [SerializeField] private float delayBetweenDialogues = 4f; // Delay between dialogues

    public void Interact()
    {
        Debug.Log("Interacted with NPC");

        // Trigger chat bubble upon interaction
        StartCoroutine(ShowDialogues());
    }

    private IEnumerator ShowDialogues()
    {
        Vector3 chatBubblePosition = new Vector3(-1, 0.7f, -1); // Adjust as needed

        for (int i = 0; i < npcDialogues.Count; i++)
        {
            string dialogue = npcDialogues[i];
            ChatBubble.Create(transform, chatBubblePosition, chatBubbleIcon, dialogue);
            Debug.Log("Dialogue = " + dialogue);

            // Wait for the specified delay before showing the next dialogue
            if (i < npcDialogues.Count - 1) // Avoid waiting after the last dialogue
            {
                yield return new WaitForSeconds(delayBetweenDialogues);
            }
        }
    }
}
