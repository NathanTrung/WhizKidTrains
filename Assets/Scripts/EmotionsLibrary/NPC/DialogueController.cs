using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance != null) return;
        if (ConversationManager.Instance.IsConversationActive) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ConversationManager.Instance.SelectPreviousOption();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ConversationManager.Instance.SelectNextOption();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ConversationManager.Instance.PressSelectedOption();
        }
    }
}
