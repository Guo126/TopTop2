using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    private Animator anim;
    private GameObject canvas;
    public bool  isChat = false;
    private int speedID = Animator.StringToHash("Speed");
    private int isSpeedUpID = Animator.StringToHash("IsSpeedUp");
    private int horizontalID = Animator.StringToHash("Horizontal");
    private int speedZID = Animator.StringToHash("SpeedZ");
    private int speedRotateID = Animator.StringToHash("SpeedRotate");
    private double timer = 0;
    private Vector3 direction = Vector3.zero;
    public Vector3 target;

    public float gravity = 5f;
    
    public float speed = 3f;
    public GameObject perfab;
    
    public AudioClip[] ac = new AudioClip[4];
    private RaycastHit infoPoint;
    private GameObject info;
    public Shoot shoot;
    public Fight fight;
    public ChatWith chat;
    private bool isRoad;
    Vector3 forePoint = Vector3.zero;
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
        if (!isChat)
        {
            if (Input.GetMouseButtonDown(1))
            {
                LayerMask lm = LayerMask.NameToLayer("Road");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.DrawRay(ray.origin, ray.direction, Color.blue);
                RaycastHit[] infos;
                infos = Physics.RaycastAll(ray,10);
              
                for (int i = 0; i < infos.Length; i++)
                {
                    info = infos[i].collider.gameObject;
                    infoPoint = infos[i];
                    if (info.layer == LayerMask.NameToLayer("Tree"))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                
                if (info.layer == lm)
                {
                    isRoad = true;
                }
                else
                {
                    isRoad = false;
                }
                

                if (!isRoad)
                {
                    
                    print("not road"+ info.name);
                    if (info.tag == "enemy")
                    {
                        print("enemy");
                       // transform.LookAt(info.transform);
                        if (shoot != null)
                        {

                            shoot.hasTarget = true;
                            shoot.enemys = this.info.transform.GetChild(0).GetChild(0).gameObject;
                        }
                        else
                        {
                            print("fight");
                            fight.hasTarget = true;
                            fight.enemys = this.info.transform.GetChild(0).GetChild(0).gameObject;
                        }


                    }
                    else if (info.layer == LayerMask.NameToLayer("StoryThing"))
                    {
                        if (info.name == "magic01")
                        {
                            info.GetComponent<SelfAction>().Action();
                        }else if (info.name == "magic02")
                        {
                            info.GetComponent<selfActionSeco>().Action();
                        }
                        else if (info.name == "magic03")
                        {
                            info.GetComponent<selfActionThr>().Action();
                        }else if (PlayerMes.getInstance().Snum==3&&info.name == "magic04")
                        {
                            info.GetComponent<SelfStory>().showStory();
                            
                        }
                        target = transform.position;

                    }
                    else if (info.tag == "npc")
                    {
                        print("npc");
                        info.transform.LookAt(transform);
                        target = info.transform.position;
                        chat.isChat = true;
                        chat.target = info;

                        
                    }
                    else if (info.tag == "magicDoor")
                    {
                        print("magicDoor");
                        target = info.transform.position;

                    }
                    else if (info.tag == "Chest")
                    {
                        print("chest");
                        target = info.transform.position;

                    }
                    
                    
                }
                else
                {
                    print(" road" + info.name);
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
                        mouse.transform.position = new Vector3(infoPoint.point.x, infoPoint.point.y+0.2f, infoPoint.point.z);
                        mouse.transform.rotation = perfab.transform.rotation;
                    }
                    // target = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                    // Debug.Log(Camera.main.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)));
                    target = infoPoint.point;
                    timer = 0;
                }

            }

            direction = target - transform.position;
            direction.y = 0;


            if (direction.magnitude < 0.15f)
            {
                direction = Vector3.zero;
                target = transform.position;
            }
         
            if (timer == 0)
            {
              
                forePoint = transform.position;
               // print(forePoint);
            }
            timer += Time.deltaTime;
            if (forePoint!=null&&timer >= 0.85f)
            {
                //Debug.Log((transform.position - forePoint).magnitude);
                // float dis = Vector3.Distance(transform.position, forePoint);
                float dis = (new Vector3(transform.position.x,0, transform.position.z) - new Vector3(forePoint.x,0,forePoint.z)).magnitude;
                print(dis);
                if (dis < 1)        
                {
                    target = transform.position;    
                    
                }
                timer = 0;
            }
            target.y = this.transform.position.y;
            this.transform.LookAt(target);
            gameObject.transform.position += direction.normalized * speed * Time.deltaTime;

            anim.SetFloat(speedID, Mathf.Clamp01(Mathf.Abs(direction.sqrMagnitude)));

        }

    }

    void Step()
    {
        gameObject.GetComponent<AudioSource>().clip = ac[Random.Range(0, 3)];
        gameObject.GetComponent<AudioSource>().Play();
    }
    
   
}
