using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAction : MonoBehaviour {

    public Transform point;
    public GameObject darkEvn,mainEvn;
    [SerializeField]
    private Camera camera1, camera2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            darkEvn.SetActive(true);
            mainEvn.SetActive(false);
            
            other.transform.position = point.position;
        }
    }
}
