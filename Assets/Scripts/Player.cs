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

    public float speed = 3f;
    public GameObject perfab;
    private CharacterController characterController;
    public AudioClip[] ac = new AudioClip[4];

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
	    characterController = GetComponent<CharacterController>();

	    target = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        ChatWith();
        if (Input.GetMouseButtonDown(1))
        {
            LayerMask lm = 1 << LayerMask.NameToLayer("Road");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            RaycastHit info;
            Physics.Raycast(ray, out info, 1000f, lm.value);
           
            if (perfab != null)
            {
                GameObject mouse = objectPool.GetInstance().GetObj(perfab.name);
                mouse.transform.position = info.point;
                mouse.transform.rotation = perfab.transform.rotation;
            }

              //  GameObject.Instantiate(perfab, info.point,perfab.transform.rotation);

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
        characterController.SimpleMove(direction.normalized * speed);
        anim.SetFloat(speedID, Mathf.Clamp01(Mathf.Abs(direction.sqrMagnitude)));
        //anim.SetFloat(speedRotateID, direction.z * 126);

        //anim.SetFloat(speedID, Input.GetAxis("Vertical"));
        //anim.SetFloat(horizontalID, Input.GetAxis("Horizontal"));
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    anim.SetBool(isSpeedUpID, true);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    anim.SetBool(isSpeedUpID, false);
        //}
    }

    void Step()
    {
        gameObject.GetComponent<AudioSource>().clip = ac[Random.Range(0, 3)];
        gameObject.GetComponent<AudioSource>().Play();
    }
    
    void ChatWith()
    {
        float dis = 10;
        if (Input.GetMouseButtonDown(1))
        {
            
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(r, out hit))
            {
                
                if (!isChat&&hit.collider.gameObject.tag == "shop1")
                {
                    target = hit.collider.gameObject.transform.position;
                    dis = (hit.collider.gameObject.transform.position - transform.position).magnitude;
                }
                 
            }
           
        }
        if (dis <= 3)
        {
            target = transform.position;
            canvas = (GameObject)Instantiate(Resources.Load("menu/Chat"));
            isChat = true;
            canvas.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "我可是通灵圣地有名的铁匠！";
        }
        else
        {
            if (canvas != null&&Input.GetMouseButton(0))
            {
                isChat = false;
                canvas.gameObject.SetActive(false);
            }
        }
    }
}
