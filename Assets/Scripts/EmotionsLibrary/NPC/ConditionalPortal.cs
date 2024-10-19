using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EmotionsLibrary.Player;

public class ConditionalPortal : portal
{
    [SerializeField]
    protected GameObject videoObject; //what has our video script
    [SerializeField]
    protected NPCConversation myConversation;

    //! Methods
    //? Unity
    //optimise later by doing one getcomponent
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<Player>(out var player))
        { 
            //check if video is set before allowing a tp
                if (videoObject.GetComponent<VideoSelector>().currentVideoIndex != -1)
                {
                    player.Teleport(destination.position, destination.rotation);
                    videoObject.GetComponent<VideoSelector>().PlayVideo();
                }
                else
                {
                    ConversationManager.Instance.StartConversation(myConversation);
                }
        }
    }

}
