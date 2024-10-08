using Autodesk.Fbx;
using UnityEngine;

public class PlayerRideController : MonoBehaviour
{
    public Transform train; // Assign this in the Inspector
    public Vector3 offset; // Offset from the train's position
    private bool isOnTrain = false; // Track if the player is currently on the train

    private void Update()
    {
        if (isOnTrain && train != null)
        {
            // Update the player's position to follow the train
            transform.position = train.position + offset;
            transform.rotation = train.rotation; // Optionally match rotation
        }
    }

    public void SetOnTrain(bool onTrain)
    {
        isOnTrain = onTrain;
    }
}
