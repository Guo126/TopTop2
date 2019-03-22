using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class menuOut : MonoBehaviour {

    private bool isOut = false;
    public Sprite go ;
    public Sprite back ;

  


    public void menuGo()
    {
        if (!isOut)
        {
            transform.parent.Translate(new Vector3(0, -80, 0));
            gameObject.GetComponent<Image>().overrideSprite = go;
        }
        
        isOut = true;

       
    }
      public void menuBack()
    {
        if (isOut)
        {
            transform.parent.Translate(new Vector3(0, 80, 0));
            gameObject.GetComponent<Image>().overrideSprite = go;
        }

        isOut = false;
    }
}
