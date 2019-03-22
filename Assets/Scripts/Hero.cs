using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public GameObject bag;
    private bool isMenu = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        heroPro();
	}

    void heroPro()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray,out rayHit))
            {
                if (isMenu)
                {
                    bag.SetActive(false);
                    isMenu = false;
                }
                if (rayHit.collider.gameObject.tag == "hero"&&!isMenu)
                {
                    
                    bag.SetActive(true);
                    isMenu = true;
                }
            }
        }
    }
}
