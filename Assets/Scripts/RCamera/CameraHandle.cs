﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour {

	private Transform player_Transform;//玩家角色
    private GameObject Player;
	private List<GameObject> collideredObjects;//本次射线hit到的GameObject
	private List<GameObject> bufferOfCollideredObjects;//上次射线hit到的GameObject
	
	void Start () {
		collideredObjects=new List<GameObject>();
		bufferOfCollideredObjects=new List<GameObject>();
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		bufferOfCollideredObjects.Clear();
		for(int temp=0;temp<collideredObjects.Count; temp++)
		{
			bufferOfCollideredObjects.Add(collideredObjects[temp]);//得到上次的
		}
		collideredObjects.Clear();


        //发射射线
        player_Transform = Player.transform;
		Vector3 dir = (transform.position - player_Transform.position).normalized;
		RaycastHit[] hits;
		hits = Physics.RaycastAll(player_Transform.position, dir, Vector3.Distance(player_Transform.position,transform.position));
		Debug.DrawLine(player_Transform.position, transform.position, Color.red);//让其显示以便观测
		
		for(int i=0;i<hits.Length;i++)
		{
			if(hits[i].collider.gameObject.tag!="Player")
			{

				collideredObjects.Add(hits[i].collider.gameObject);//得到现在的
                print(collideredObjects[i].name + "22");
                 SetMaterialsColor(collideredObjects[i].GetComponent<Renderer>(),true);
               // killObjects(collideredObjects[i], true);
            }
        }
        //上次与本次对比，本次还存在的物体则赋值为null
        for (int i = 0; i < bufferOfCollideredObjects.Count; i++)
        {
            
            for (int j = 0; j < collideredObjects.Count; j++)
            {
                if (collideredObjects[j] != null)
                {
                   // print(bufferOfCollideredObjects[i].name+"   "+collideredObjects[i].name);
                    if (bufferOfCollideredObjects[i].name == collideredObjects[j].name)
                    {
                        print("sa");
                        bufferOfCollideredObjects[i] = null;
                        break;
                    }
                }
            }
        }
        //把上次的还原，这次的透明
        for (int i = 0; i < bufferOfCollideredObjects.Count; i++)
        {
            if (bufferOfCollideredObjects != null)
            {
                print(bufferOfCollideredObjects[i].name);
                SetMaterialsColor(bufferOfCollideredObjects[i].GetComponent<Renderer>(),false);
            }

            // killObjects(bufferOfCollideredObjects[i], false);

        }
        
    }
	
	//是否搞透明
	void SetMaterialsColor(Renderer r,bool isClear)
	{
		if(isClear)
		{
			int materialsNumber = r.sharedMaterials.Length;
			for (int i = 0; i < materialsNumber; i++)
			{
                Color _color = r.materials[i].color;
                _color.a = 0.6f;
                r.materials[i].SetColor("_Color", _color);

            }
        }
		else
		{
			int materialsNumber = r.sharedMaterials.Length;
			for (int i = 0; i < materialsNumber; i++)
			{
                Color _color = r.materials[i].color;
                _color.a = 1.0f;
                r.materials[i].SetColor("_Color", _color);

            }
        }
	}
    //去掉遮挡物
    void killObjects(GameObject @object,bool isOprate)
    {
        if (isOprate)
        {
            @object.SetActive(false);
        }
        else
        {
            @object.SetActive(true);
        }
    }
}
