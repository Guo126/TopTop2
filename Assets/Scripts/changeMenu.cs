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
    public void theMenu()
    {
        if (index == 0)
        {
            //Camera.main.GetComponent<VideoPlayer>().clip = story1;
            //Camera.main.GetComponent<VideoPlayer>().Play();
            //PlayerPrefs.SetInt("isFirst", 1);
            //Invoke("wait", 1.5f);
            //Invoke("wait2", 92f);
            wait();
            wait2();

        }
        else if(index == 1)
        {
            gameObject.SetActive(false);
            maneger.GetComponent<control>().play();
        }
    }
	
	public void nextMenu()
    {
        if(index == 3)
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

    void wait2()
    {
        world.SetActive(true);
        overlay.SetActive(true);
        hero.SetActive(true);
    }
}
