using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class audioPlay : MonoBehaviour {

    private VideoPlayer videoPlayer;

    private RawImage rawImage;

   
    

    // Use this for initialization

    void Start()
    {

        //获取场景中对应的组件

        videoPlayer = this.GetComponent<VideoPlayer>();

        rawImage = this.GetComponent<RawImage>();



        Invoke("colorChange", 1.5f);
        Invoke("videoChange", 1f);

    }



    // Update is called once per frame

    void Update()
    {

        //如果videoPlayer没有对应的视频texture，则返回

        if (videoPlayer.texture == null)
        {
            return;
        }
        else
        {
           
            rawImage.texture = videoPlayer.texture;
        }

        //把VideoPlayerd的视频渲染到UGUI的RawImage

        

    }

    void colorChange()
    {
        rawImage.color = Color.white;
        
    }


    public void videoChange()
    {
        videoPlayer.Play();
    }

   
}
