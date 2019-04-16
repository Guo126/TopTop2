using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour {

	private Transform player_Transform;//玩家角色
    private GameObject Player;
	private List<GameObject> collideredObjects;//本次射线hit到的GameObject
	private List<GameObject> bufferOfCollideredObjects;//上次射线hit到的GameObject
    public Material material;
	
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
                 SetMaterialsColor(collideredObjects[i].GetComponent<Renderer>(),true);//变透明
                //SetMaterials(collideredObjects[i].GetComponent<Renderer>(), true);
            }
        }
        //上次与本次对比，本次还存在的物体则赋值为null
        for (int i = 0; i < bufferOfCollideredObjects.Count; i++)
        {          
            for (int j = 0; j < collideredObjects.Count; j++)
            {
                if (collideredObjects[j] != null)
                {                 
                    if (bufferOfCollideredObjects[i].name == collideredObjects[j].name)
                    {                       
                        bufferOfCollideredObjects[i] = null;
                        break;
                    }
                }
            }
        }
        //把上次的还原，这次的透明
        for (int i = 0; i < bufferOfCollideredObjects.Count; i++)
        {
            if (bufferOfCollideredObjects[i] != null)
            {
               // SetMaterials(bufferOfCollideredObjects[i].GetComponent<Renderer>(), false);
                SetMaterialsColor(bufferOfCollideredObjects[i].GetComponent<Renderer>(),false);
            }
            
        }
        
    }

    //是否搞透明
    void SetMaterialsColor(Renderer r, bool isClear)
    {


        int materialsNumber = r.sharedMaterials.Length;
        if (isClear)
        {
            for (int i = 0; i < materialsNumber; i++)
            {
                Color _color = r.materials[i].color;
                _color.a = 0;
                r.materials[i].SetColor("_Color", _color);

            }
        }
        else
        {

            for (int i = 0; i < materialsNumber; i++)
            {
                Color _color = r.materials[i].color;
                _color.a = 1.0f;
                r.materials[i].SetColor("_Color", _color);

            }


        }
    }

    void SetMaterials(Renderer r, bool isClear)
    {

        int materialsNumber = r.sharedMaterials.Length;
        if (isClear)
        {
            for (int i = 0; i < materialsNumber; i++)
            {
               // Color _color = r.materials[i].color;
                r.materials[i] = this.material;



            }
        }
        else
        {

            for (int i = 0; i < materialsNumber; i++)
            {
                r.materials[i] = material;

            }


        }
    }

}
