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
    private  GameObject mainCanvas;

    private void Start()
    {
        mainCanvas = GameObject.Find("Main_Canvas");
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
                GameObject chatCanvas = Instantiate(chatting, mainCanvas.transform);
                if (chatCanvas)
                {

                }
            }
        }
       
       
        
    }
}
