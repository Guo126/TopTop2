using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SelfStory : Story {

    public GameObject videoCanvas;
    public VideoClip videoClip;
    private VideoPlayer videoPlayer;
	// Use this for initialization
	void Start () {
        videoPlayer = videoCanvas.GetComponent<VideoPlayer>();

    }
	
	// Update is called once per frame
	void Update () {
        while ((ulong)videoPlayer.frame == (videoPlayer.frameCount - 2))
        {
            videoCanvas.SetActive(false);
            gameObject.SetActive(false);
        }
	}
    override
    public void showStory()
    {
        videoCanvas.SetActive(true);
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
        
    }
}
