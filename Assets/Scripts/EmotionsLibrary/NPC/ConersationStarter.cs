using UnityEngine;
using DialogueEditor;
namespace WhizKid.EmotionsLibrary.NPC
{
    public class ConersationStarter : MonoBehaviour
    {
        [SerializeField] private NPCConversation myConversation;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ConversationManager.Instance.StartConversation(myConversation);
                }
            }
        }
    }
}
