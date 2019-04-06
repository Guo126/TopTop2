using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage;
    public Color defaultColor;
    private MeshRenderer mat;

 

    public void SetColor(Color color)
    {
        mat.material.SetColor("_Color", color);
    }

    private MeshRenderer mr;
    private TrailColor trailColor;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        trailColor = GetComponentInChildren<TrailColor>();
        SetColor(new Color(71 / 255, 58 / 255, 39 / 255));
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            other.GetComponent<Hurtable>().GetHurt(damage);
        }
    }

}
