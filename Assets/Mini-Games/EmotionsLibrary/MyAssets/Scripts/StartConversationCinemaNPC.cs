using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class StartConversationCinemaNPC : MonoBehaviour
{
    public NPCConversation[] conversations; // Array of conversations for each video
    [SerializeField]
    protected NPCConversation movieConvo; //convo for when trying to talk during movie
    public VideoSelector videoSelector; // Reference to the VideoSelector script
    private bool conversationStarted = false; // To prevent re-triggering the conversation

    private void Start()
    {
        // Ensure videoSelector is assigned
        if (videoSelector == null)
        {
            Debug.LogError("VideoSelector is not assigned!");
        }
    }

    // Method to start the NPC conversation based on the current video
    private void StartConversationBasedOnVideo()
    {
        int currentVideoIndex = videoSelector.currentVideoIndex;

        // Check if the current video index is valid and matches a conversation
        if (currentVideoIndex >= 0 && currentVideoIndex < conversations.Length)
        {
            NPCConversation conversation = conversations[currentVideoIndex];
            if (conversation != null)
            {
                Debug.Log("Starting conversation for video index: " + currentVideoIndex);
                ConversationManager.Instance.StartConversation(conversation); // Start the conversation
                conversationStarted = true; // Prevent conversation from being triggered multiple times
            }
            else
            {
                Debug.LogError("No conversation assigned for this video index: " + currentVideoIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid video index or conversation not found.");
        }
    }

    // Reset the conversation state when the video stops playing
    private void LateUpdate()
    {
        if (!videoSelector.videoPlayer.isPlaying && conversationStarted)
        {
            conversationStarted = false;
            Debug.Log("Conversation reset.");
        }
    }

    private void StartDuringMovieConvo()
    {
        if (movieConvo != null)
        {
            ConversationManager.Instance.StartConversation(movieConvo);
        }
        else
        {
            Debug.LogError("Unset conversation for interrupting movie");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!videoSelector.videoPlayer.isPlaying)
            { 
                StartConversationBasedOnVideo();
            }  
            else 
                {
                StartDuringMovieConvo();
            }
        }
    }
}

