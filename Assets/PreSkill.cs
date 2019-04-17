using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSkill : MonoBehaviour {

    private GameObject player;
   
    private bool isOpened = false;
    private float timer = 0;
    public IUnityEvent ievent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isOpened)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                
                PlayerMes.getInstance().MagicNum -= 5;
                ievent.Invoke(2);
                timer = 0;
            }
            
        }
    }
    public void preSkill()
    {
        if (!isOpened)
        {
            player.transform.GetChild(0).gameObject.SetActive(true);
            PlayerMes.getInstance().Defence += 5;
            isOpened = true;
        }
        else
        {
            player.transform.GetChild(0).gameObject.SetActive(false);
            PlayerMes.getInstance().Defence -= 5;
            isOpened = false;
        }
       
    }
    public void Recover()
    {
        player.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        PlayerMes.getInstance().BloodNum += 20;
        ievent.Invoke(1);
    }
}
