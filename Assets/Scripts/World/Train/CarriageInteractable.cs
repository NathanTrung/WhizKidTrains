using UnityEngine;

public class CarriageInteractable : MonoBehaviour
{
    public TrainController trainController; // Reference to the TrainController script on the train
    public float[] positions = { 0.1f, 0.2f, 0.5f, 0.65f, 0.85f }; // Predefined positions
    public int currentIndex = 0; // Index for cycling through positions

    void Start()
    {
        if (trainController == null)
        {
            trainController = GetComponentInParent<TrainController>();
            if (trainController == null)
            {
                Debug.LogError("TrainController not found in parent!");
            }
        }
    }

    public void OnMouseDown() // Example of interaction using a mouse click
    {
        if (trainController != null)
        {
            float nextPosition = positions[currentIndex];
            trainController.OnInteract(nextPosition);

            currentIndex = (currentIndex + 1) % positions.Length; // Cycle through positions

            // Check if the percent has wrapped around
            double percent = trainController.splineFollower.GetPercent();
            if (percent >= 1.0)
            {
                Debug.Log("Percent has reached or exceeded 1. Resetting index.");
                trainController.splineFollower.SetPercent(0);
            }
        }
        else
        {
            Debug.LogError("TrainController not found!"); // Error log
        }
    }
}