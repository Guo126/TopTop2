using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public GameObject hero;
	
	void Update () {

        gameObject.transform.localPosition = hero.transform.localPosition + new Vector3(0,5,-4);
    }
}
