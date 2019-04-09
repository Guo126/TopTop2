using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    private Animator anim;
    private GameObject canvas;
    private bool isChat;
    private int speedID = Animator.StringToHash("Speed");
    private int isSpeedUpID = Animator.StringToHash("IsSpeedUp");
    private int horizontalID = Animator.StringToHash("Horizontal");
    private int speedZID = Animator.StringToHash("SpeedZ");
    private int speedRotateID = Animator.StringToHash("SpeedRotate");

    private Vector3 direction = Vector3.zero;
    public Vector3 target;

    public float gravity = 5f;
    
    public float speed = 3f;
    public GameObject perfab;
    
    public AudioClip[] ac = new AudioClip[4];
    private GameObject info;
    public Shoot shoot;
    public Fight fight;
    public ChatWith chat;
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
	    
        if (gameObject.GetComponent<Shoot>() != null)
        {
            shoot = gameObject.GetComponent<Shoot>();
        }
        else
        {
            fight = gameObject.GetComponent<Fight>();
        }
        chat = gameObject.GetComponent<ChatWith>();
	    target = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            LayerMask lm =  LayerMask.NameToLayer("Road");          
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            RaycastHit[] infos;
            infos = Physics.RaycastAll(ray);
            for(int i = 0; i < infos.Length; i++)
            {
                info = infos[i].collider.gameObject;
                if (info.layer != lm)
                {
                    if (info.tag == "enemy")
                    {
                        if (shoot != null)
                        {
                            shoot.hasTarget = true;
                            shoot.enemys = this.info;
                        }
                        else
                        {
                            fight.hasTarget = true;
                            fight.enemys = this.info;
                        }

                        break;
                    }
                    else if (info.tag == "NPC")
                    {
                        print("pp");
                        target = info.transform.position;
                        chat.isChat = true;
                        chat.target = info;
                        break;
                        // dis = (enemys.transform.position - transform.position).magnitude;
                    }
                    else if (info.tag == "magicDoor")
                    {
                        target = info.transform.position;
                        break;
                    }
                    else if (info.tag == "Chest")
                    {
                        target = info.transform.position;
                        break;
                    }
                    continue;
                }
                else
                {
                    chat.isChat = false;
                    if (shoot != null)
                    {
                        shoot.hasTarget = false;
                    }
                    else
                    {
                        fight.hasTarget = false;
                    }
                    if (perfab != null)
                    {
                        GameObject mouse = objectPool.GetInstance().GetObj(perfab.name);
                        mouse.transform.position = infos[i].point;
                        mouse.transform.rotation = perfab.transform.rotation;
                    }

                    target = infos[i].point;
                    break;

                }
            }           
        }

        direction = target - transform.position;
        direction.y = 0;
       

        if (direction.magnitude < 0.3f)
        {
            direction = Vector3.zero;
            target = transform.position;
        }

        target.y = this.transform.position.y;
        this.transform.LookAt(target);

        gameObject.transform.position += direction.normalized * speed*Time.deltaTime;

        anim.SetFloat(speedID, Mathf.Clamp01(Mathf.Abs(direction.sqrMagnitude)));
      
    }

    void Step()
    {
        gameObject.GetComponent<AudioSource>().clip = ac[Random.Range(0, 3)];
        gameObject.GetComponent<AudioSource>().Play();
    }
    
   
}
