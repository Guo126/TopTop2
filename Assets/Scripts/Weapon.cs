using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage;


    private MeshRenderer mr;
    private TrailColor trailColor;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        trailColor = GetComponentInChildren<TrailColor>();
        SetColor(new Color(71 / 255, 58 / 255, 39 / 255));
    }

    public void SetColor(Color c)
    {
        mr.material.SetColor("_Color", c);
        trailColor.SetColor(c);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            other.GetComponent<Hurtable>().GetHurt(damage);
        }
    }

}
