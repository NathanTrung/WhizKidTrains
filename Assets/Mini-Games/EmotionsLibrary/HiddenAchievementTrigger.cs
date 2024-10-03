using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAchievementTrigger : MonoBehaviour
{
    public GameEvent onAchievementTrigger;  // Reference to the achievement trigger event
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip achievementSound; // Reference to the sound clip
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            // Trigger the achievement event if the condition is met
            if (onAchievementTrigger != null)
            {
                onAchievementTrigger.TriggerEvent();
                //play sound Assets/Cute UI _ Interact Sound Effects Pack/AUDIO/UI/Success/SFX_UI_Success_Bright_Pop_1.wav
            }
            // Play the achievement sound
            if (audioSource != null && achievementSound != null)
            {
                audioSource.PlayOneShot(achievementSound);
            }
        }
    }
}
