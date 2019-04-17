using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control : MonoBehaviour {

    private string[] heroNames = new string[4] { "巫师" ,"领主","射手","战士"};
    public  int hero_index = 0;
    public GameObject heroName;
    public GameObject heroObject;
    public GameObject[] heros;
    public Animator[] animators;
    private float target;
    private float fill = 0;
    public GameObject slider;
    public GameObject start;
    public GameObject loading;
    private float timer;
    public TextAsset heroTxt;
    private string[] mes;
    public GameObject heroMes;
    public Text per;

    private void Start()
    {
        animators[2].SetBool("isOk", true);
        string text = heroTxt.text;
        mes = text.Split('\n');
        heroMes.GetComponent<Text>().text = mes[hero_index];
    }
    private void Update()
    {
      
    }

    public void preHero()
    {
         
        if (hero_index < 4 && hero_index!=0)
        {
            Camera.main.transform.position = Vector3.Lerp
                (Camera.main.transform.position, new Vector3(Camera.main.transform.position.x - 5, 0, -10), 1f);
            
            --hero_index;
            isCanTurn();
            //animators[hero_index].SetBool("isOk", true);
        }

        heroName.GetComponent<Text>().text = heroNames[hero_index];
        heroMes.GetComponent<Text>().text = mes[hero_index];
    }

    public void nextHero()
    {
            
        if (hero_index >= 0 && hero_index != 3)
        {
            Camera.main.transform.position = Vector3.Lerp
            (Camera.main.transform.position, new Vector3(Camera.main.transform.position.x + 5, 0, -10), 1f);
            
            ++hero_index;
            isCanTurn();
           // animators[hero_index].SetBool("isOk", true);
            
        }
        heroName.GetComponent<Text>().text = heroNames[hero_index];
        heroMes.GetComponent<Text>().text = mes[hero_index];
    }

    void isCanTurn()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == hero_index)
            {
                animators[i].SetBool("isOk", true);
               // heros[i].GetComponent<Animator>().enabled = true;
            }
            else
            {
                animators[i].SetBool("isOk", false);
                //heros[i].GetComponent<Animator>().enabled = false;
                //heros[i].transform.eulerAngles = new Vector3(-180, 0, -180);
            }

        }
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void play()
    {
        start.SetActive(false);
        heroObject.SetActive(false);
        loading.SetActive(true);
        PlayerPrefs.SetInt("hero_index", hero_index);
        StartCoroutine("change");
    }


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
