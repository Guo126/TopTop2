using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoPool : MonoBehaviour {

	// Use this for initialization


    private void OnEnable()
    {
        Invoke("ToPool", 0.4f);
    }

    void ToPool()
    {
        objectPool.GetInstance().RecycleObj(gameObject);
    }
}
