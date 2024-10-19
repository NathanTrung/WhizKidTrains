using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    [SerializeField] AudioSource swipeSound; // AudioSource for playing the sound effect
    [SerializeField] AudioClip swipeClip; // The sound effect clip
    int index = 0; // Start at page 0
    bool rotate = false;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;

    // Start is called before the first frame update
    private void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        // Set all page rotations to identity (no rotation)
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }

        // Set the first page to be the last sibling (visible page)
        pages[0].SetAsLastSibling();
        backButton.SetActive(false); // Disable back button initially
    }

    public void RotateForward()
    {
        if (rotate || index >= pages.Count - 1) return; // Avoid moving beyond last page
        PlaySwipeSound(); // Play sound effect when rotating forward
        float angle = 180; // Rotate the page forward by 180 degrees around the y-axis
        StartCoroutine(Rotate(angle, true)); // Start forward rotation
    }

    public void ForwardButtonActions()
    {
        if (!backButton.activeInHierarchy)
        {
            backButton.SetActive(true); // Enable back button when moving forward
        }

        if (index >= pages.Count - 1)
        {
            forwardButton.SetActive(false); // Disable forward button on last page
        }
    }

    public void RotateBack()
    {
        if (rotate || index < 0) return; // Avoid moving beyond first page
        PlaySwipeSound(); // Play sound effect when rotating back
        float angle = 0; // Rotate the page back to 0 degrees (original)
        StartCoroutine(Rotate(angle, false)); // Start back rotation
    }

    public void BackButtonActions()
    {
        if (!forwardButton.activeInHierarchy)
        {
            forwardButton.SetActive(true); // Enable forward button when moving back
        }

        if (index < 0)
        {
            backButton.SetActive(false); // Disable back button on first page
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        rotate = true; // Lock rotation
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

        while (true)
        {
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value); // Smoothly rotate the page
            float angleDifference = Quaternion.Angle(pages[index].rotation, targetRotation); // Get the remaining rotation

            if (angleDifference < 0.1f)
            {
                pages[index].rotation = targetRotation; // Snap to final rotation

                if (forward)
                {
                    index++; // Increment index after the forward rotation is done
                    if (index < pages.Count)
                    {
                        pages[index].SetAsLastSibling(); // Show next page on top
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        index--; // Decrement index after back rotation is done
                    }
                    pages[index].SetAsLastSibling(); // Show previous page on top
                }

                rotate = false; // Unlock rotation
                break;
            }

            yield return null;
        }

        // Call button actions after rotation completes
        if (forward) ForwardButtonActions();
        else BackButtonActions();
    }

    // Play the swipe sound effect
    void PlaySwipeSound()
    {
        if (swipeSound != null && swipeClip != null)
        {
            swipeSound.PlayOneShot(swipeClip); // Play the sound clip once
        }
    }

    // Call this method when the canvas becomes inactive
    public void OnCanvasDeactivation()
    {
        index = 0; // Reset the index to the first page
        InitialState(); // Reset pages
        backButton.SetActive(false); // Hide back button
        forwardButton.SetActive(true); // Show forward button
    }
}
