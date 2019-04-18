using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAction : MonoBehaviour {

    public Transform point;
    public AudioClip clip;
    
	// Use this for initialization
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {   
            other.transform.position = point.position;
            other.GetComponent<Player>().target = point.position;
            MusicManager.Instance.PlayMusic(clip);
        }
    }
}
