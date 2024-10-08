using UnityEngine;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.ComponentModel;

public class TrainController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 boardingOffset;

    [Header("Camera Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float onTrainFOV = 10f;
    [SerializeField] private float offTrainFOV = 50f;
    [Description("aaaa")]

    [Header("Train Settings")]
    public SplineFollower splineFollower;
    [SerializeField] private Collider boardingCollider;
    [SerializeField] private float moveDuration = 4.0f;
    [SerializeField] private float forwardIncrement = 0.14f;
    private bool isMoving = false;
    private bool isPlayerOnTrain = false;
    private Queue<float> targetQueue = new Queue<float>();
    // private bool hasStartedMoving = false;

    #region MonoBehaviour CallBacks

    private void Start()
    {
        if (player == null)
        {
            Debug.Log("TrainController Start(): Player not assigned");
            return;
        }

        if (mainCamera == null)
        {
            Debug.Log("TrainController Start(): Camera not assigned");
            return;
        }

        mainCamera.fieldOfView = offTrainFOV;

        if (splineFollower == null)
        {
            Debug.LogError("TrainController Start(): SplineFollower component not found.");
            return;
        }

        if (boardingCollider == null)
        {
            Debug.LogError("TrainController Start(): BoardingCollider component not found.");
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!IsPlayerInRange())
            {
                Debug.Log("TrainController Update(): Player in range");
                return;
            }
            Debug.LogWarning("TrainController Update(): Player not in range of Train");

            if (isPlayerOnTrain)
            {
                OnPlayerExit();
            }
            else
            {
                OnPlayerEnter();
            }
        }


        // Move the train continuously if it's moving or if there are targets in the queue
        if (isMoving || targetQueue.Count > 0)
        {
            MoveTrain();
        }
    }

    #endregion

    private bool IsPlayerInRange()
    {
        Vector3 closestPoint = boardingCollider.ClosestPoint(player.position);
        float distance = Vector3.Distance(closestPoint, player.position);
        return distance <= boardingCollider.bounds.extents.magnitude;
    }

    private void OnPlayerEnter()
    {
        Debug.Log("TrainController OnPlayerEnter(): Entering Train");
        player.SetParent(transform);
        player.localPosition = boardingOffset; // Set to boarding offset
        player.localRotation = Quaternion.identity;

        // Adjust FOV when the player is on the train
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = onTrainFOV;
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

    private void OnPlayerExit()
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

        // Adjust FOV when the player exits the train
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = offTrainFOV;
        }

        Debug.Log("Player exited the train and moved to: " + exitPosition);
    }

    public void OnInteract(float newTargetPercent)
    {
        if (!isPlayerOnTrain)
        {
            Debug.Log("Cannot interact while not on the train.");
            return;
        }

        float currentPercent = (float)splineFollower.GetPercent();
        newTargetPercent = Mathf.Clamp(newTargetPercent, 0f, 1f);

        // Ensure the target percent is ahead of the current position or very close
        if (newTargetPercent >= currentPercent || Mathf.Approximately(newTargetPercent, currentPercent))
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
        // hasStartedMoving = true;

        while (targetQueue.Count > 0)
        {
            float targetPercent = targetQueue.Dequeue();
            Debug.Log("Moving to position " + targetPercent);

            float startPercent = (float)splineFollower.GetPercent();
            float moveDuration = this.moveDuration;
            float distance = Mathf.Abs(targetPercent - startPercent);

            // Adjust move duration based on distance
            if (distance > 0.25f)
            {
                moveDuration *= 2;
            }

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                float newPercent = Mathf.Lerp(startPercent, targetPercent, elapsedTime / moveDuration);
                splineFollower.SetPercent(newPercent);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            splineFollower.SetPercent(targetPercent);
            Debug.Log("Reached target position " + targetPercent);

            // Ensure targets are processed sequentially
            HandleSplineLoop(targetPercent);
        }

        isMoving = false;
        // hasStartedMoving = false;
        Debug.Log("Queue is empty, ready for next interaction.");
    }

    private void HandleSplineLoop(float currentTargetPercent)
    {
        if (currentTargetPercent >= 1f)
        {
            Debug.Log("End of spline reached, looping back to start.");
            splineFollower.SetPercent(0f); // Reset to start
            // Optionally, re-enqueue remaining targets to ensure proper processing
        }
    }

    private void MoveTrain()
    {
        if (splineFollower != null)
        {
            float currentPercent = (float)splineFollower.GetPercent();
            float nextPercent = Mathf.Clamp(currentPercent + (forwardIncrement / 100f), 0f, 1f);

            // Check if we're close to the end of the spline and need to loop
            if (Mathf.Approximately(nextPercent, 1f))
            {
                Debug.Log("End of spline reached, looping back to start.");
                nextPercent = 0f; // Loop back to start
            }

            // Move the train to the next position
            splineFollower.SetPercent(nextPercent);

            // If nextPercent is very close to currentPercent, consider stopping
            if (Mathf.Approximately(currentPercent, nextPercent))
            {
                isMoving = false; // Stop moving if we've reached or are very close to the target
            }
        }
    }
}