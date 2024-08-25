using UnityEngine;

public class CarriageInteractable : MonoBehaviour
{
    public TrainController trainController; // Reference to the TrainController script on the train
    public float[] positions = {0.1f, 0.2f, 0.5f, 0.65f, 0.85f, 0.86f}; // Predefined positions
    public int currentIndex = 0; // Index for cycling through positions

    void Start()
    {
        if (trainController == null)
        {
            trainController = GetComponentInParent<TrainController>();
        }
    }

    public void OnMouseDown() // Example of interaction using a mouse click
    {
        if (trainController != null)
        {
            float nextPosition = positions[currentIndex];
            trainController.OnInteract(nextPosition);
            currentIndex = (currentIndex + 1) % positions.Length; // Cycle through positions
        }
        else
        {
            Debug.LogError("TrainController not found!"); // Error log
        }
    }
}
