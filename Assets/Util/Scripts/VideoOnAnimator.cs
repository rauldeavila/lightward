using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoOnAnimator : MonoBehaviour {

    public GameObject videoPlayer; // drag and drop
    private VideoHandler videoRef;

    void Awake(){
        videoRef = videoPlayer.GetComponent<VideoHandler>();
    }

    public void PlayVideo(){
        videoRef.PlayVideo();
    }

    public void StopVideo(){
        videoRef.StopVideo();
    }

    public void Disable(){
        videoRef.Disable();
    }




}
