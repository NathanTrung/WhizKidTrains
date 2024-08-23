using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f; // Range for interaction

    // Update is called once per frame
    void Update()
    {
        // Check for player input to interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Get all colliders within the interaction range
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

            // Loop through each collider to check for interactable components
            foreach (Collider collider in colliderArray)
            {
                // Check for NPC interaction
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact(); // This will show the chat bubble as well
                    break; // Exit the loop after interacting with the first valid NPC
                }

                // Check for Carriage interaction
                if (collider.TryGetComponent(out CarriageInteractable carriageInteractable))
                {
                    carriageInteractable.Interact();
                    break; // Exit the loop after interacting with the first valid Carriage
                }

                // Check for Building interaction
                if (collider.TryGetComponent(out BuildingInteractable buildingInteractable))
                {
                    buildingInteractable.Interact();
                    break; // Exit the loop after interacting with the first valid Building
                }
            }
        }
    }
}
