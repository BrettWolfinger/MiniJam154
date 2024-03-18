using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundVideoPlayer : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    private void Awake() {
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"background.mp4");
        //
    }
}
