using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage;
    public Color defaultColor;
    private MeshRenderer mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>();
        //SetColor(defaultColor);
    }

    public void SetColor(Color color)
    {
        mat.material.SetColor("_Color", color);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            other.GetComponent<Hurtable>().GetHurt(damage);
        }
    }

}
