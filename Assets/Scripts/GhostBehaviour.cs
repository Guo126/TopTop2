using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostBehaviour : MonoBehaviour
{

    //怪物对象
    private Ghost ghost;
    private GameObject player;
    //属性参数
    private Transform g_transform;
    private Animator anim;
    [SerializeField] public int ghostBlood = 1000;
    [SerializeField] private int maxBlood = 1000;
    [SerializeField] private float ghostAR = 40f;
    [SerializeField] private float ghostFR = 20f;
    [SerializeField] private float ghostMS = 1f;
    private int preBlood;

   
    public FUntyEvent OnBloodChanged;
    
    
    private bool isInjured = false;     //是否受到攻击

    private int isDeadID = Animator.StringToHash("IsDead");
    private int isStandingID = Animator.StringToHash("IsStanding");
    private int skillID = Animator.StringToHash("Skill");
    private int isRunID = Animator.StringToHash("IsRun");
    private int isFightID = Animator.StringToHash("IsFight");
    private bool isdead = false;

    void Start()
    {
        preBlood = maxBlood;
        ghostBlood = maxBlood;
        g_transform = gameObject.transform.parent.transform.parent.GetComponent<Transform>();
        anim = gameObject.transform.parent.transform.parent.GetComponent<Animator>();
        
        ghost = new Ghost(ghostBlood, ghostAR, g_transform.position, ghostFR, ghostMS, false);
        OnBloodChanged.Invoke((float)ghostBlood / maxBlood);
    }

    void Update()
    {

        if (preBlood <= 0&&!isdead)
        {
            isdead = true;
            anim.SetBool(isDeadID, true);
            anim.SetBool(isStandingID, false);
            anim.SetBool(isRunID, false);
            anim.SetBool(isFightID, false);
            player.GetComponent<Animator>().SetBool("isAttacking", false);
            if (player.GetComponent<Shoot>() != null)
            player.GetComponent<Shoot>().hasTarget = false;
            if (player.GetComponent<Fight>() != null)
                player.GetComponent<Fight>().hasTarget = false;
            gameObject.GetComponent<AudioSource>().Play();
            Invoke("dead", 2f);
            return;
        }
        else if(!isdead)
        {
            
            if (preBlood != ghostBlood)
            {
                OnBloodChanged.Invoke((float)ghostBlood / maxBlood);
            }
            preBlood = ghostBlood;

            if (isInjured)      //受到伤害
            {
                player = GameObject.FindGameObjectWithTag("Player");
                ghost.IsFightStatus = true;
                transform.LookAt(player.transform.parent.parent);
                if (Vector3.Distance(player.transform.position, gameObject.transform.parent.transform.parent.transform.position) > ghost.FightRange && ghost.IsFightStatus)      //不在攻击范围
                {
                    //走
                    anim.SetBool(isDeadID, false);
                    anim.SetBool(isStandingID, false);
                    anim.SetBool(isRunID, true);
                    anim.SetBool(isFightID, false);

                    //超出活动范围
                    if (Vector3.Distance(gameObject.transform.parent.transform.parent.transform.position, ghost.DefoultPosition) >= ghost.ActivityRange)
                    {
                        ghost.IsFightStatus = false;
                        isInjured = false;
                    }
                    else
                    {
                        //朝玩家走
                        gameObject.transform.parent.transform.parent.transform.LookAt(player.transform);
                        Vector3 dir = (player.transform.position - gameObject.transform.parent.transform.parent.transform.position).normalized;
                        gameObject.transform.parent.transform.parent.transform.position += ghost.MoveSpeed * dir * Time.deltaTime;
                    }
                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.parent.transform.parent.transform.position) <= ghost.FightRange && ghost.IsFightStatus)
                {
                    //开始打
                    anim.SetBool(isStandingID, false);
                    anim.SetBool(isFightID, true);
                    anim.SetBool(isRunID, false);

                }
            }
            else
            {
                if (Vector3.Distance(gameObject.transform.parent.transform.parent.transform.position, ghost.DefoultPosition) != 0)
                {
                    gameObject.transform.parent.transform.parent.transform.LookAt(ghost.DefoultPosition);
                    if (Vector3.Distance(gameObject.transform.parent.transform.parent.transform.position, ghost.DefoultPosition) < 0.1f)
                    {
                        gameObject.transform.parent.transform.parent.transform.position = ghost.DefoultPosition;
                        //站立,血满
                        gameObject.transform.parent.transform.parent.transform.LookAt(player.transform);
                        anim.SetBool(isDeadID, false);
                        anim.SetBool(isStandingID, true);
                        anim.SetBool(isRunID, false);
                        anim.SetBool(isFightID, false);
                    }
                    else
                    {
                        anim.SetBool(isDeadID, false);
                        anim.SetBool(isStandingID, false);
                        anim.SetBool(isRunID, true);
                        anim.SetBool(isFightID, false);
                        Vector3 dir = (ghost.DefoultPosition - gameObject.transform.parent.transform.parent.transform.position).normalized;
                        gameObject.transform.parent.transform.parent.transform.position += ghost.MoveSpeed * dir * Time.deltaTime;
                        //回血
                        if (ghostBlood < maxBlood)
                        {
                            ghostBlood += (int)(maxBlood * 0.006f);
                           
                        }
                        else
                            ghostBlood = maxBlood;
                    }
                }
            }
        }

        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "weapon" && Vector3.Distance(gameObject.transform.parent.transform.parent.transform.position, ghost.DefoultPosition) == 0)
        {
            isInjured = true;
        }
    }
 
   void dead()
    {
        
        Destroy(gameObject.transform.parent.parent.gameObject);
    }

}
