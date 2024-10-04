using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject destination1; // Assign the destination game object in the Unity editor
    public Transform PlayerBody; // Reference to the player's transform (assuming the player's transform is on a separate GameObject)
    public Transform destination; // Reference to the destination's transform

    // Function to teleport the player to the destination
    public void TeleportToDestination()
    {
        // Check if the destination object and player's transform are not null
        if (destination1 != null && PlayerBody != null)
        {
            PlayerBody.position = destination1.transform.position; // Teleport the player to the destination's position
        }
        else
        {
            Debug.LogError("Destination1 or Player transform is not assigned!");
        }
    }

    // Example of how you could call this script from another script or event
    void Start()
    {
        // Example of triggering the teleportation when this script is called
        TeleportToDestination();
    }
}
