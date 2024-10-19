using UnityEngine;
using DialogueEditor;
using EmotionsLibrary.Player;

public class StartConversation : MonoBehaviour
{
    public NPCConversation myConversation;
    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }

    void OnEnable()
    {
        ConversationManager.OnConversationStarted += HandleConversationStart;
        ConversationManager.OnConversationEnded += HandleConversationEnd;
    }

    void OnDisable()
    {
        ConversationManager.OnConversationStarted -= HandleConversationStart;
        ConversationManager.OnConversationEnded -= HandleConversationEnd;
    }

    private void HandleConversationEnd()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.enabled = true;
    }

    private void HandleConversationStart()
    {
        Cursor.lockState = CursorLockMode.Confined;
        player.enabled = false;
    }
}
