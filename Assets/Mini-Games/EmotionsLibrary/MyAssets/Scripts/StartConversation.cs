using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class StartConversation : MonoBehaviour
{
    public NPCConversation myConversation;

    /*
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
