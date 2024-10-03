using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;

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
