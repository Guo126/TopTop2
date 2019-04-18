using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int hero_index,needUpdate;
    public GameObject players ,hero;
    public IUnityEvent ResetBlood,ResetMagic;
    public FUntyEvent UpdateBlood,UpdateMagic;
    public List<Text> texts;

    // Use this for initialization
    void Awake() {
        //hero_index = 3;
        hero_index = PlayerPrefs.GetInt("hero_index");
        hero = (GameObject)Instantiate(Resources.Load("Hero/" + hero_index.ToString()));
        hero.transform.SetParent(players.transform);
        hero.transform.localPosition = new Vector3(0,0,0);
        switch (hero_index)
        {
            case 0:
                PlayerMes.getInstance().Init("1",400, 400, 200, 200, 60, 5, 10, 10,500);
                break;
            case 1:
                
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 5, 10, 10,500);
                break;
            case 2:
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 5, 10, 10,500);
                break;
            case 3:
                PlayerMes.getInstance().Init("1", 400, 400, 200, 200, 60, 5, 10, 10, 500);
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
            UpdateMagic.Invoke((float)PlayerMes.getInstance().MagicNum / PlayerMes.getInstance().MagicMax);
            needUpdate = 0;
        }
        else if (needUpdate == 3)
        {
            SetPlayerMes();
            needUpdate = 0;
        }
        HeroDead();
        
    }

    public void HeroDead()
    {
        if (PlayerMes.getInstance().BloodNum <= 0)
        {
            objectPool.GetInstance().DeleteAll();
            SceneManager.LoadScene(1);
        }
    }

    public void MakeDead()
    {
        objectPool.GetInstance().DeleteAll();
        SceneManager.LoadScene(1);
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
