using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFall : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMes.getInstance().BloodNum = -1;
        }
    }
}
