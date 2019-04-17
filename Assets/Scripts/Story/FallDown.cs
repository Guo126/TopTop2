using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, other.GetComponent<Rigidbody>().velocity.y, 0);

            

        }
    }

    
    
}
