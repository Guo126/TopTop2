using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {

    public GameObject reward;
    public Sprite gold, exp;
    public Transform mainCanvas;
    
    public void Show(int Mlevel)
    {
        if (Mlevel == 1)
        {
            showTip(gold, 20);
            StartCoroutine(wait(exp,10));
            
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
