using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class gameBegin : MonoBehaviour {

    public GameObject start,canvas;
    private int isFirst = 0;
    private bool isStart = true;
    VideoPlayer vp = null;
	// Use this for initialization
	void Start () {
            PlayerPrefs.DeleteAll();
            vp = Camera.main.GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        if (isStart)
        {
            if (vp.frame >= 1)
            {
                canvas.SetActive(false);
            }
            if ((ulong)vp.frame >= vp.frameCount - 15)
            {
                start.SetActive(true);
                
                isStart = false;
            }
        }
        
    }

   

    
}
