using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goto : MonoBehaviour {

    public GameObject des01 ;

    
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "Weapon")
        {
            gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.transform.parent.position = des01.transform.position;
            other.gameObject.transform.position = des01.transform.position;
            other.gameObject.GetComponent<Player>().target = des01.transform.position;
        }
    }
}
