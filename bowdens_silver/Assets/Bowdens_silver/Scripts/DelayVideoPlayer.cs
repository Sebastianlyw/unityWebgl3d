using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class DelayedVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool isPlaying = false;
    private void Start()
    {
        this.videoPlayer = GetComponent<VideoPlayer>();
        this.videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Landing-Page-Header-Mining_SMALL.mp4");//GetComponent<VideoPlayer>();
        //this.videoPlayer.playOnAwake = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }

            isPlaying = !isPlaying;
        }
    }

}
