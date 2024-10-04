using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSelector : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public Button[] videoButtons; // Array of buttons to select videos
    public VideoClip[] videoPaths; // Array of video file paths
    public int currentVideoIndex = -1; //currently selected video (-1 is invalid)

    private void Start()
    {
        // Assign button click listeners dynamically
        for (int i = 0; i < videoButtons.Length; i++)
        {
            int index = i; // Capture the loop variable
            //videoButtons[i].onClick.AddListener(() => PlayVideo(index));
        }
    }

    // Method to play the selected video
    public void PlayVideo()
    {
        if (currentVideoIndex >= 0 && currentVideoIndex < videoPaths.Length)
        {
            //videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoPaths[currentVideoIndex]);
            videoPlayer.clip = videoPaths[currentVideoIndex];
            videoPlayer.Play();
            Debug.Log("Playing video: " + videoPaths[currentVideoIndex]);
        }
        else
        {
            Debug.LogError("Invalid video index: " + currentVideoIndex);
        }
    }

    public void SetIndex(int index)
    {
        //added error checking stuff here later
        currentVideoIndex = index;
    }
}
