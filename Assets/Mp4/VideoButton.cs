using UnityEngine;
using UnityEngine.Video;


public class VideoButton : MonoBehaviour
{
    public GameObject videoObject;

    public void PlayPause()
    {   if (videoObject == null)
              return;
        VideoPlayer player = videoObject.GetComponent<VideoPlayer>();

        if (player == null)
            return;

        if (player.isPlaying)
            player.Pause();
        else
            player.Play();
    
    }
}
