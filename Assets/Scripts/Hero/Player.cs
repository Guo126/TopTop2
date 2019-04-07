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
    private GameObject enemys;
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
            RaycastHit info;
            if (!Physics.Raycast(ray, out info, 1000f))
                return;
            enemys = info.collider.gameObject;
            print(enemys.name);
            if (enemys.layer != lm)
            {
                Action();
                return;
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
                
            }
                

            if (perfab != null)
            {
                GameObject mouse = objectPool.GetInstance().GetObj(perfab.name);
                mouse.transform.position = info.point;
                mouse.transform.rotation = perfab.transform.rotation;
            }

            target = info.point;
            
        }

        direction = target - transform.position;
        direction.y = 0;
        //direction = direction.normalized;

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
    
    void Action()
    {
        if (enemys.tag == "enemy")
        {
            if (shoot != null)
            {
                shoot.hasTarget = true;
                shoot.enemys = this.enemys;
            }
            else
            {
                fight.hasTarget = true;
                fight.enemys = this.enemys;
            }
            

        }else if (enemys.tag == "shop")
        {
            target =enemys.transform.position;
            chat.isChat = true;
            chat.target = enemys;

            // dis = (enemys.transform.position - transform.position).magnitude;
        }else if (enemys.tag == "magicDoor")
        {
            target = enemys.transform.position;
        }
        
              
       
        
    }
}
