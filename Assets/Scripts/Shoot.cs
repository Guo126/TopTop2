using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float attackRange = 2;
    public int damage = 0;
    public GameObject arrowPrefab;
    public Transform shootPoint;
    Vector3 mousePositionOnScreen;
    Vector3 screenPosition;
    Vector3 mousePositionInWorld;
    private GameObject arrow;
    [SerializeField]
    private float shootSpeed = 0.12f;
    private Player player;
    float dis = 0;
    public bool hasTarget = false;
    public GameObject enemys = null;
    private float timer = 2;
    public List<AudioClip> clips;

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        damage = PlayerMes.getInstance().Attack;
    }
	
	// Update is called once per frame
	void Update () {
        shootTo();
        startAttack();
	}

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    void ShootTo()
    {
        if (enemys == null) return;
        arrow = Instantiate(arrowPrefab, shootPoint.position, transform.rotation) as GameObject;
        if (clips.Count!= 0)
        {
            MusicManager.Instance.PlayMusic(clips[Random.Range(0, clips.Count)]);
        }
        Self se = arrow.gameObject.GetComponent<Self>();
        Weapon weapon = arrow.gameObject.GetComponent<Weapon>();
        weapon.damage = damage + (int)Random.Range(-damage * 0.1f, damage * 0.1f);
        se.point = enemys.transform.parent.parent.position;
        se.point.y += (enemys.gameObject.GetComponent<Collider>().bounds.size.y)/2.0f;
        se.shootSpeed = this.shootSpeed;

    }

    void shootTo()
    {
       
        if (hasTarget)
        {
            transform.LookAt(new Vector3(enemys.transform.parent.parent.position.x,transform.position.y, enemys.transform.parent.parent.position.z));
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
        Animator animator = transform.GetComponent<Animator>();
        if (dis <= attackRange && hasTarget)
        {    
            animator.SetBool("isAttacking", true);
            player.target = this.transform.position;
 
        }
        else if (dis > attackRange)
        {
            
            player.target = new Vector3(enemys.transform.position.x, player.transform.position.y, enemys.transform.position.z);
            animator.SetBool("isAttacking", false);
        }
    }

    
}
