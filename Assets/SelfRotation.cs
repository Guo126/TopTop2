using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotation : MonoBehaviour {

    public GameObject point;
    public int speed;
    public bool isStop = false;

   
	void Update () {
        // transform.Rotate(Vector3.up, speed * Time.deltaTime);
        if (!isStop)
        {
            transform.RotateAround(point.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            // transform.localPosition = new Vector3(21, 10, -27);
            
          
            
            if (transform.localEulerAngles.y > 278 && transform.localEulerAngles.y < 280){                
            }
            else
            {
                transform.RotateAround(point.transform.position, Vector3.up, 3*speed * Time.deltaTime);
            }
          

        }
       
      
       
        
	}
}
