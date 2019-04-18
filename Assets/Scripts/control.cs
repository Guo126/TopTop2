using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control : MonoBehaviour {

    private string[] heroNames = new string[4] { "巫师" ,"领主","射手","战士"};
    public  int hero_index = 1;
    public GameObject heroName;
    public GameObject heroObject;
    public GameObject[] heros;
    public Animator[] animators;
    private float target;
    private float fill = 0;
    public GameObject slider, start, loading;
    private float timer;
    public TextAsset heroTxt;
    private string[] mes;
    public GameObject heroMes;
    public Text per,diyName;
    public AudioClip clip1, clip2;

    private void Start()
    {
        animators[1].SetBool("isOk", true);
        string text = heroTxt.text;
        mes = text.Split('\n');
        heroMes.GetComponent<Text>().text = mes[hero_index];
    }

    //上一个英雄
    public void preHero()
    {
        MusicManager.Instance.PlayMusic(clip1);
        if (hero_index < 4 && hero_index!=0)
        {
            Camera.main.transform.position = Vector3.Lerp
                (Camera.main.transform.position, new Vector3(Camera.main.transform.position.x - 5, 0, -10), 1f);
            
            --hero_index;
            isCanTurn();
        }

        heroName.GetComponent<Text>().text = heroNames[hero_index];
        heroMes.GetComponent<Text>().text = mes[hero_index];
    }

    //下一个英雄
    public void nextHero()
    {
        MusicManager.Instance.PlayMusic(clip2);
        if (hero_index >= 0 && hero_index != 3)
        {
            Camera.main.transform.position = Vector3.Lerp
            (Camera.main.transform.position, new Vector3(Camera.main.transform.position.x + 5, 0, -10), 1f);
            
            ++hero_index;
            isCanTurn();         
        }
        heroName.GetComponent<Text>().text = heroNames[hero_index];
        heroMes.GetComponent<Text>().text = mes[hero_index];
    }

    //中间的旋转，其它暂停
    void isCanTurn()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == hero_index)
            {
                animators[i].SetBool("isOk", true);             
            }
            else
            {
                animators[i].SetBool("isOk", false);             
            }
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
    //踏上征途，
    public void play()
    {
        start.SetActive(false);                  
        heroObject.SetActive(false);
        loading.SetActive(true);
        PlayerPrefs.SetInt("hero_index", hero_index);   //传入英雄编号
        PlayerPrefs.SetString("myName", diyName.text);
        StartCoroutine("change");
    }

    //加载场景，获取进度
    IEnumerator change()
    {
        yield return new WaitForEndOfFrame();   //等待帧结束
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);   //异步加载场景API，返回异步参数
        asyncOperation.allowSceneActivation = false;   //设置不允许加载完成后自动跳转界面
        while (!asyncOperation.isDone)       //是否加载完成
        {
            target = asyncOperation.progress;          //  加载进度
            fill = Mathf.Lerp(fill, target, 0.1f);                  //fill均匀增加
            slider.GetComponent<Image>().fillAmount = fill;          
            per.text = (fill * 100).ToString("f0") + "%"; 
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;               //计时器
            if (timer > 4f)
            {
                asyncOperation.allowSceneActivation = true;           //四秒后进入场景
            }

   
        }
    }

  

}
