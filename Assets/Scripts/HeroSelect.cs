using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelect : MonoBehaviour {

    public GameObject[] heroes;
    public int hero_index = 0;
    public GameObject jobName;
    GameObject hero = null;

 
    bool dragging = false;   //标记是否鼠标在滑动


    void Start () {
        show();
	}
	
	
	void Update () {
        
       
        panduan();
    }

    public void next()
    {
        if (hero_index < 4)
        {
            hero_index++;
        }
        else
        {
            hero_index = 0;
        }
        objectPool.GetInstance().RecycleObj(hero);
        show();
    }

    public void before()
    {

        if (hero_index >0)
        {
            hero_index--;
        }
        else
        {
            hero_index = 4;
        }
        objectPool.GetInstance().RecycleObj(hero);
        show();
    }

    void show()
    {
        hero = objectPool.GetInstance().GetObj(heroes[hero_index].name );
        jobName.GetComponent<Text>().text = hero.name;
    }

   
    void panduan()
    {
        if (Input.GetMouseButtonDown(0))
            dragging = true;
        else if (Input.GetMouseButtonUp(0))
            dragging = false;


        if (dragging)
        {
            float fMouseX = Input.GetAxis("Mouse X");
            
               
            hero.transform.Rotate(Vector3.up, -fMouseX * 20);
       
        }

     
    }
   
   
}
