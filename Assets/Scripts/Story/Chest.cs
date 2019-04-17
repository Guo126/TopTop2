using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private GameObject player;
    public IUnityEvent reward;
    [SerializeField] private int level =1;
    private bool isShow = true;
    public GameObject changeBox;
    public AudioClip ac;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        float dis = (player.transform.position - transform.position).magnitude;
        if (dis <= 1&&isShow)
        {
            player.GetComponent<Player>().target = player.transform.position;
            Instantiate(changeBox, transform.position, transform.rotation);
            MusicManager.Instance.PlayMusic(ac);
            reward.Invoke(level);
            gameObject.SetActive(false);
            isShow = false;
        }
		
	}
}
