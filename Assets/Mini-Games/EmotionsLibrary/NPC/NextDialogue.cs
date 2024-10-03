using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NextDialogue : MonoBehaviour
{
//    int index = 2;

    // Update is called once per frame
    void Update()
    {
        if(ConversationManager.Instance != null)
        {
            if(ConversationManager.Instance.IsConversationActive)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ConversationManager.Instance.SelectPreviousOption();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ConversationManager.Instance.SelectNextOption();
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    ConversationManager.Instance.PressSelectedOption();
                }
            }

        }
    }
}
