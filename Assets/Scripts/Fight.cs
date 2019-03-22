using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour {

    private Player player;
    Vector3 mousePositionOnScreen;
    Vector3 screenPosition;
    Vector3 mousePositionInWorld;
    GameObject enemys = null;
    public List<AudioClip> ac;
    public  bool hasTarget;
    float dis = 0;
    public float range = 1;

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        shootTo();
        startAttack();
	}

    

    void shootTo()
    {
        if (Input.GetMouseButtonDown(1))
        {
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePositionOnScreen = Input.mousePosition;
            //让场景中的Z=鼠标坐标的Z
            mousePositionOnScreen.z = screenPosition.z;
            //将相机中的坐标转化为世界坐标
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                enemys = hit.collider.gameObject;

                if (enemys.tag == "enemy")
                {
                    hasTarget = true;
                   
                }
                else
                {
                    hasTarget = false;
                    enemys = null;
                    Animator animator = transform.GetComponent<Animator>();
                    animator.SetBool("isAttacking", false);
                }

            }
        }

    }

    void startAttack()
    {
        if (enemys == null) return;
        dis = (gameObject.transform.position - enemys.transform.position).magnitude;
        Animator animator = transform.GetComponent<Animator>();
        if (dis <= range && hasTarget)
        {
            animator.SetBool("isAttacking", true);
            player.target = this.transform.position;
        }
        else if (dis > range)
        {
            // hasTarget = false;
            //enemys = null;
            animator.SetBool("isAttacking", false);
        }
    }
     void FightTo()
    {
        enemys.gameObject.GetComponent<GhostBehaviour>().ghostBlood -= PlayerMes.getInstance().Attack;
        MusicManager.Instance.PlayMusic(ac[Random.Range(0,2)]);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);      
    }
}
