using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour {

	public Transform player_Transform;//玩家角色
	    
	private List<GameObject> collideredObjects;//本次射线hit到的GameObject
	private List<GameObject> bufferOfCollideredObjects;//上次射线hit到的GameObject
	
	void Start () {
		collideredObjects=new List<GameObject>();
		bufferOfCollideredObjects=new List<GameObject>();
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
		Vector3 dir = (transform.position - player_Transform.position).normalized;
		RaycastHit[] hits;
		hits = Physics.RaycastAll(player_Transform.position, dir, Vector3.Distance(player_Transform.position,transform.position));
		//Debug.DrawLine(player_Transform.position, transform.position, Color.red);//让其显示以便观测
		
		for(int i=0;i<hits.Length;i++)
		{
			if(hits[i].collider.gameObject.name!="Player")
			{

				collideredObjects.Add(hits[i].collider.gameObject);//得到现在的
			}
		}
		//把上次的还原，这次的透明
		for(int i=0;i<bufferOfCollideredObjects.Count;i++)
		{
			SetMaterialsColor(bufferOfCollideredObjects[i].GetComponent<Renderer>(),false);
		}
		for(int i=0;i<collideredObjects.Count;i++)
		{
			SetMaterialsColor(collideredObjects[i].GetComponent<Renderer>(),true);
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
                _color.a = 0.3f;
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
}
