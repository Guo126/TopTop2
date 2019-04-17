using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {

    public GameObject reward;
    public Sprite gold, exp,s1,s2,s3;
    public Transform mainCanvas;
    
    public void Show(int Mlevel)
    {
        if (Mlevel == 1)
        {
            showTip(gold, 20);
            StartCoroutine(wait(exp,10));
            
        }
    }

    public void Chest(int Clevel)
    {
        if(Clevel == 1)
        {
            showTip(gold, 100);
        }
    }

    public void stone(int order)
    {
        switch (order)
        {
            case 1:showTip(s1, 1);
                break;
            case 2:
                showTip(s2, 1);
                break;
            case 3:
                showTip(s3, 1);
                break;
        }
    }

    void showTip(Sprite g,int num)
    {
        GameObject rw = Instantiate(reward, mainCanvas);
        rw.transform.GetChild(0).GetComponent<Image>().sprite = g;
        rw.transform.GetChild(1).GetComponent<Text>().text = " + "+num.ToString();
        Destroy(rw, 1.6f);
    }

    IEnumerator wait(Sprite s,int num)
    {
        //yield return new WaitForSeconds(2);
        yield return new WaitForSecondsRealtime(1);
        showTip(s, num);
    }
}
