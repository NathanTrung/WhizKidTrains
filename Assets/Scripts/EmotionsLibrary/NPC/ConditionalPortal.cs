using DialogueEditor;
using UnityEngine;

namespace WhizKid.EmotionsLibrary
{
    public class ConditionalPortal : portal
    {
        [SerializeField]
        protected GameObject videoObject; //what has our video script
        [SerializeField]
        protected NPCConversation myConversation;

        private void OnTriggerEnter(Collider other)
        {
            VideoSelector vidSelector = videoObject.GetComponent<VideoSelector>();
            if (other.CompareTag("Player") && other.TryGetComponent<Player>(out var player))
            {
                //check if video is set before allowing a tp
                if (vidSelector.currentVideoIndex != -1)
                {
                    player.Teleport(destination.position, destination.rotation);
                    vidSelector.PlayVideo();
                }
                else
                {
                    ConversationManager.Instance.StartConversation(myConversation);
                }
            }
        }

    }
}
