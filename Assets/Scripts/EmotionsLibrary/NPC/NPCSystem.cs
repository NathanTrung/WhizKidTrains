using UnityEngine;
using DialogueEditor;

namespace WhizKid.EmotionsLibrary
{
    public class NPCSystem : MonoBehaviour
{
    public NPCConversation myConversation;
    bool player_detection = false;

    // Update is called once per frame
    void Update()
    {
        if(player_detection && Input.GetKeyDown(KeyCode.F))
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}
}
