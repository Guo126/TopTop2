using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodToCamera : MonoBehaviour {

    Transform mainCamera_Transform;
	// Use this for initialization
	void Start () {
        mainCamera_Transform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.LookAt(mainCamera_Transform);
	}
}
