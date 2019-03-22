using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetName : MonoBehaviour {

    
    public void OnEndValue()
    {
        PlayerMes.getInstance().MyName = gameObject.GetComponent<InputField>().text;
        
    }
}
