using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour {

    private Player player;
    Vector3 mousePositionOnScreen;
    Vector3 screenPosition;
    Vector3 mousePositionInWorld;
    public GameObject enemys = null;
    public List<AudioClip> ac;
    public  bool hasTarget;
    float dis = 0;
    public float range = 1.4f;

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
        if (hasTarget)
        {
            transform.LookAt(new Vector3(enemys.transform.parent.parent.position.x, transform.position.y, enemys.transform.parent.parent.position.z));
        }
        else
        {
            enemys = null;
            Animator animator = transform.GetComponent<Animator>();
            animator.SetBool("isAttacking", false);
        }
      
    }

    void startAttack()
    {

        if (enemys == null) return;
        
        dis = (gameObject.transform.position - enemys.transform.position).magnitude;
        print(dis + "   " + range);
        Animator animator = transform.GetComponent<Animator>();
        if (dis <= range && hasTarget)
        {
            print("startFight");
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
