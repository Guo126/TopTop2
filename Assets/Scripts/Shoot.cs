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
    GameObject enemys = null;
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
                    transform.LookAt(enemys.transform.parent.parent);
                    //Debug.Log(enemys.name);
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
        if (dis <= attackRange && hasTarget)
        {    
            animator.SetBool("isAttacking", true);
            player.target = this.transform.position;
 
        }
        else if (dis > attackRange)
        {
            // hasTarget = false;
            //enemys = null;
            animator.SetBool("isAttacking", false);
        }
    }

    
}
