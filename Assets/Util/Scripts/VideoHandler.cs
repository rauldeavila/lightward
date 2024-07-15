using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour {

    private VideoPlayer videoPlayer;

    void Awake(){
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void PlayVideo(){
        videoPlayer.Play();
    }

    public void StopVideo(){
        videoPlayer.Stop();
    }

    public void Disable(){
        this.gameObject.SetActive(false);
    }






}
