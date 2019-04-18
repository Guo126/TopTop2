using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class changeMenu : MonoBehaviour {

    public Sprite[] menus  ;
    private int index = 0;
    public GameObject menu;
    public GameObject start, world, overlay, hero,maneger;
    public VideoClip story1;
    private bool isPlay = false;
    public  AudioClip clik,ac;


    // Use this for initialization
    void Start () {
        
        
        if (PlayerPrefs.GetInt("isFirst") != 1)
        {
            menu.GetComponent<Image>().overrideSprite = menus[0];
            index = 0;
        }
        else
        {
            menu.GetComponent<Image>().overrideSprite = menus[1];
            index = 1;
        }


    }


    private void Update()
    {
        if (isPlay&&Camera.main.GetComponent<VideoPlayer>().frame>=1)
        {
           
            if (Input.GetKeyDown(KeyCode.Escape))
            {             
                Camera.main.GetComponent<VideoPlayer>().Stop();
               // Camera.main.GetComponent<VideoPlayer>().frame = (long)Camera.main.GetComponent<VideoPlayer>().frameCount; 
            }
           
            if (Camera.main.GetComponent<VideoPlayer>().frame >= (long)Camera.main.GetComponent<VideoPlayer>().frameCount)
            {
                world.SetActive(true);
                overlay.SetActive(true);
                hero.SetActive(true);
            }
        }
    }


    public void theMenu()
    {
        MusicManager.Instance.PlayMusic(ac);
        switch (index)
        {
            case 0:
                Camera.main.GetComponent<VideoPlayer>().clip = story1;
                Camera.main.GetComponent<VideoPlayer>().Play();
                isPlay = true;
                PlayerPrefs.SetInt("isFirst", 1);
                Invoke("wait", 1.5f);
                break;
            case 1:
                gameObject.SetActive(false);
                maneger.GetComponent<control>().play();
                break;
            case 2:
                break;
            case 3:
                Application.Quit();
                break;
        }
        
    }
	
	public void nextMenu()
    {
        MusicManager.Instance.PlayMusic(clik);
        if (index == 3)
        {
            index = 0;
        }
        else{
            index += 1;
        }
       
        menu.GetComponent<Image>().overrideSprite = menus[index];
    }

    public void beforeMenu()
    {
        MusicManager.Instance.PlayMusic(clik);
        if (index == 0)
        {
            index = 3;
        }
        else
        {
            index -= 1;
        }

        menu.GetComponent<Image>().overrideSprite = menus[index];
    }

    void wait()
    {
        start.SetActive(false);
    }

   
}
