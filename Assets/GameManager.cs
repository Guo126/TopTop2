﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int hero_index,needUpdate;
    public GameObject players ,hero;
    public IUnityEvent ResetBlood,ResetMagic;
    public FUntyEvent UpdateBlood;
    public List<Text> texts;

    // Use this for initialization
    void Awake() {
       // hero_index = 4;
        hero_index = PlayerPrefs.GetInt("hero_index");
        hero = (GameObject)Instantiate(Resources.Load("Hero/" + hero_index.ToString()));
        hero.transform.SetParent(players.transform);
        hero.transform.position = players.transform.position;
        switch (hero_index)
        {
            case 0:
                PlayerMes.getInstance().Init("1",400, 400, 200, 200, 60, 20, 10, 10,500);
                break;
            case 1:
                
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 20, 10, 10,500);
                break;
            case 2:
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 20, 10, 10,500);
                break;
            case 3:
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 20, 10, 10, 500);
                break;
            case 4:
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 20, 10, 10, 500);
                break;
        }
        ResetBlood.Invoke(PlayerMes.getInstance().BloodNum);
        ResetMagic.Invoke(PlayerMes.getInstance().MagicNum);
        SetPlayerMes();
    }
    


    // Update is called once per frame
    void Update () {
        if (needUpdate==1)
        {          
            UpdateBlood.Invoke((float)PlayerMes.getInstance().BloodNum / PlayerMes.getInstance().BloodMax);          
            needUpdate = 0;
        }else if (needUpdate == 2)
        {
            SetPlayerMes();
            needUpdate = 0;
        }
        HeroDead();
        
    }

    void HeroDead()
    {
        if (PlayerMes.getInstance().BloodNum <= 0)
        {
            objectPool.GetInstance().DeleteAll();
            SceneManager.LoadScene(1);
        }
    }

    public void NeedUpdate(int a)
    {

        needUpdate = a;
    }

    void SetPlayerMes() {

        texts[0].text = PlayerMes.getInstance().BloodMax.ToString();
        texts[1].text = PlayerMes.getInstance().MagicMax.ToString();
        texts[2].text = PlayerMes.getInstance().Attack.ToString();
        texts[3].text = PlayerMes.getInstance().Defence.ToString();
        texts[4].text = PlayerMes.getInstance().HidePer.ToString();
        texts[5].text = PlayerMes.getInstance().BigHit.ToString();
        texts[6].text = PlayerMes.getInstance().Gold.ToString();
    }
}
