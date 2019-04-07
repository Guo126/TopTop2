using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatWith : MonoBehaviour {

    public GameObject target =null;
    private float dis = 0;
    private float chatRange = 1.2f;
    private Animator animator;
    private Player player;
    public GameObject chatting;
    public  bool isChat =false;
    private  GameObject chatCanvas;

    private void Start()
    {
        chatCanvas = GameObject.Find("Main_Canvas/Chat");
        chatCanvas.SetActive(false);
        animator = transform.GetComponent<Animator>();
        player = gameObject.GetComponent<Player>();
    }
    private void Update()
    {
        WalkTo();
        
    }

    private void WalkTo()
    {
        if (target)
        {
            dis = (gameObject.transform.position - target.transform.position).magnitude;
            if (dis <= chatRange && isChat)
            {
                player.target = this.transform.position;
                
                chatCanvas.SetActive(true);
                chatCanvas.GetComponent<ShowWords>().xmlName = target.name;
                
            }
        }
       
       
        
    }
}
