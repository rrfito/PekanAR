using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void OnEnable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();  
            videoPlayer.Play();
        }
    }

    void OnDisable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}
