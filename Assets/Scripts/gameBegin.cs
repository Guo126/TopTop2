using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class gameBegin : MonoBehaviour {

    public GameObject start;
    private int isFirst = 0;
    VideoPlayer vp = null;
	// Use this for initialization
	void Start () {

            vp = Camera.main.GetComponent<VideoPlayer>();
            vp.Play();
            
           
       
    }
    private void Update()
    {
        wait();
    }

    // Update is called once per frame
    void wait()
    {
        if ((ulong)vp.frame >= vp.frameCount-15)
        {
            start.SetActive(true);
        }
        
    }

    
}
