using UnityEngine;

public class PlayerCanvasTrigger : MonoBehaviour
{
    public GameObject canvas;  // Reference to the canvas.
    private bool isCollided = false;  // Track if collision has occurred.
    private bool hasPressedEnter = false;  // Ensure Enter works only once.

    private void Start()
    {
        // Ensure the canvas is initially hidden.
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the object.
        if (other.CompareTag("Player") && !hasPressedEnter)
        {
            isCollided = true;  // Mark that a collision occurred.
            canvas.SetActive(true);  // Show the canvas.
        }
    }

    private void Update()
    {
        // If the player is in collision and presses Enter.
        if (isCollided && Input.GetKeyDown(KeyCode.Return))
        {
            canvas.SetActive(false);  // Hide the canvas.
            hasPressedEnter = true;  // Ensure it works only once.
        }
    }
}
