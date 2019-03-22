using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GhostBehaviour))]
public class Hurtable : MonoBehaviour {

    private GhostBehaviour ghostBehaviour;

	// Use this for initialization
	void Start () {
        ghostBehaviour = GetComponent<GhostBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHurt(int damage)
    {
        ghostBehaviour.ghostBlood -= damage;
    }
}
