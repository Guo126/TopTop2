using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColor : MonoBehaviour {

    private TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {
        trailRenderer = GetComponent<TrailRenderer>();
	}
	

    public void SetColor(Color c)
    {
        trailRenderer.materials[0].SetColor("_Color", c);
    }
}
