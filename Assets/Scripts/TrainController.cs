using UnityEngine;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;

public class TrainController : MonoBehaviour
{
    public SplineFollower splineFollower;
    public float moveDuration = 4.0f;
    private bool isMoving = false;
    public float forwardIncrement = 0.14f;
    public Transform player;
    public Vector3 boardingOffset;
    public Collider boardingCollider;
    public Camera mainCamera; // Reference to the main camera
    public Vector3 cameraOffset; // Offset for the camera
    private Vector4 playerCameraOffset = new Vector4(0.5910273f, 0.9778184f, -0.8691149f);

    private bool isPlayerOnTrain = false;
    private Queue<float> targetQueue = new Queue<float>();
    private float currentTargetPercent = 0f;
    private bool hasStartedMoving = false;

    private Vector4 initialCameraPosition; // Store the initial camera position
    private Quaternion initialCameraRotation; // Store the initial camera rotation

    private void Start()
    {
        if (splineFollower == null)
        {
            splineFollower = GetComponent<SplineFollower>();
            if (splineFollower == null)
            {
                Debug.LogError("SplineFollower component not found.");
            }
        }

        if (boardingCollider == null)
        {
            boardingCollider = GetComponent<Collider>();
            if (boardingCollider == null)
            {
                Debug.LogError("BoardingCollider component not found.");
            }
        }

        // Store the initial camera position and rotation
        if (mainCamera != null)
        {
            initialCameraPosition = mainCamera.transform.position;
            initialCameraRotation = mainCamera.transform.rotation;
        }
        else
        {
            Debug.LogError("Main Camera not assigned.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isPlayerOnTrain)
            {
                OnPlayerExit();
            }
            else
            {
                if (IsPlayerInRange())
                {
                    OnPlayerRide();
                }
                else
                {
                    Debug.Log("Player is not within boarding range.");
                }
            }
        }

        if (isPlayerOnTrain && Input.GetKeyDown(KeyCode.Space)) // Interaction key
        {
            float newTargetPercent = 0.5f; // Example target
            OnInteract(newTargetPercent);
        }

        // Move the train continuously if it's moving
        if (isMoving && hasStartedMoving)
        {
            MoveTrain();
        }
    }

    private bool IsPlayerInRange()
    {
        if (player != null && boardingCollider != null)
        {
            Vector3 closestPoint = boardingCollider.ClosestPoint(player.position);
            float distance = Vector3.Distance(closestPoint, player.position);
            return distance <= boardingCollider.bounds.extents.magnitude;
        }
        return false;
    }

    private void OnPlayerRide()
    {
        if (player != null)
        {
            Debug.Log("Attempting to board the train.");
            player.SetParent(transform);
            player.localPosition = boardingOffset; // Set to boarding offset
            player.localRotation = Quaternion.identity;

            // Set the camera position to the desired offset
            if (mainCamera != null)
            {
                Vector3 trainPosition = transform.position;

                // Adjusting the camera's position relative to the train with the offset
                Vector3 cameraPosition = trainPosition + cameraOffset;
                mainCamera.transform.position = cameraPosition;

                // Optionally: Make the camera look at the train
                mainCamera.transform.LookAt(trainPosition);
            }

            PlayerRideController playerController = player.GetComponent<PlayerRideController>();
            if (playerController != null)
            {
                playerController.SetOnTrain(true);
            }

            isPlayerOnTrain = true;
            Time.timeScale = 1f; // Ensure time is running normally
            Debug.Log("Player boarded the train.");
        }
        else
        {
            Debug.LogError("Player object is not assigned.");
        }
    }

    private void OnPlayerExit()
    {
        if (player != null)
        {
            Debug.Log("Attempting to exit the train.");
            Vector3 exitPosition = splineFollower.transform.position;
            player.position = exitPosition;

            player.SetParent(null);
            isPlayerOnTrain = false;
            Time.timeScale = 1f;

            PlayerRideController playerController = player.GetComponent<PlayerRideController>();
            if (playerController != null)
            {
                playerController.SetOnTrain(false);
            }

            // Restore the camera position and rotation
            if (mainCamera != null)
            {
                // Reset camera position to the initial position
                Vector4 playerCoord = new Vector4(player.position.x, player.position.y, -0.1f + 41f);

                mainCamera.transform.position = playerCoord + playerCameraOffset;
                mainCamera.transform.rotation = initialCameraRotation;
            }

            Debug.Log("Player exited the train and moved to: " + exitPosition);
        }
        else
        {
            Debug.LogError("Player object is not assigned.");
        }
    }

    public void OnInteract(float newTargetPercent)
    {
        if (!isPlayerOnTrain)
        {
            Debug.Log("Cannot interact while not on the train.");
            return;
        }

        float currentPercent = (float)splineFollower.GetPercent();
        if (newTargetPercent > currentPercent)
        {
            Debug.Log("OnInteract called with target: " + newTargetPercent);
            targetQueue.Enqueue(newTargetPercent);

            if (!isMoving)
            {
                Debug.Log("Starting movement to " + newTargetPercent);
                StartCoroutine(ProcessQueue());
            }
            else
            {
                Debug.Log("Already moving, added target to queue.");
            }
        }
        else
        {
            Debug.Log("Target percent must be ahead of current position.");
        }
    }

    private IEnumerator ProcessQueue()
    {
        isMoving = true;
        hasStartedMoving = true;

        while (targetQueue.Count > 0)
        {
            currentTargetPercent = targetQueue.Dequeue();
            Debug.Log("Moving to position " + currentTargetPercent);

            float startPercent = (float)splineFollower.GetPercent();
            float distance = Mathf.Abs(currentTargetPercent - startPercent);
            float currentMoveDuration = distance > 0.25f ? moveDuration * 2 : moveDuration;

            float elapsedTime = 0f;
            while (elapsedTime < currentMoveDuration)
            {
                float newPercent = Mathf.Lerp(startPercent, currentTargetPercent, elapsedTime / currentMoveDuration);
                splineFollower.SetPercent(newPercent);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            splineFollower.SetPercent(currentTargetPercent);
            Debug.Log("Reached target position " + currentTargetPercent);

            // Handle increment at the end of the spline
            if (currentTargetPercent >= 0.86f)
            {
                float newTargetPercent = Mathf.Clamp(currentTargetPercent + forwardIncrement, 0.0f, 1.0f);
                Debug.Log("Near the end of the spline, moving forward a little to: " + newTargetPercent);
                splineFollower.SetPercent(newTargetPercent);
                currentTargetPercent = newTargetPercent;
            }
        }

        isMoving = false;
        hasStartedMoving = false;
        Debug.Log("isMoving set to false, ready for next interaction.");
    }

    private void MoveTrain()
    {
        if (splineFollower != null)
        {
            float currentPercent = (float)splineFollower.GetPercent();
            float nextPercent = Mathf.Clamp(currentPercent + (forwardIncrement / 100f), 0f, 1f);

            if (Mathf.Approximately(currentPercent, nextPercent))
            {
                isMoving = false; // Stop moving if we've reached or are very close to the target
                return;
            }

            splineFollower.SetPercent(nextPercent);
        }
    }
}
