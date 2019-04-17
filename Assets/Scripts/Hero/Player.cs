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
                print(info.name);
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
                    
                    print("not road");
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
                    print("road");
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
                    
                }

            }

            direction = target - transform.position;
            direction.y = 0;


            if (direction.magnitude < 0.15f)
            {
                direction = Vector3.zero;
                target = transform.position;
            }
            Vector3 forePoint =  Vector3.zero ;
            if (timer == 0)
            {
                forePoint = transform.position;
            }
            timer += Time.deltaTime;
            if (forePoint!=null&&timer >= 1f)
            {
                float dis = (transform.position - forePoint).magnitude;
                if (dis < 0.01f)
                {
                    target = transform.position;
                }
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
