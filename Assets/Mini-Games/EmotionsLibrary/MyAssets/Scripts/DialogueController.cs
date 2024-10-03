using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
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
    }
}
