using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private GameObject target;

    private Vector3 offset;
    
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.transform.position - this.transform.position;
        Debug.Log(this.name);
	}

    void LateUpdate()
    {     
        this.transform.position = target.transform.position - offset;
        Rotate();
        Scale(); 
    }

    //缩放
    private void Scale()
    {
        float dis = offset.magnitude;
        dis += Input.GetAxis("Mouse ScrollWheel") * -5;
       
        if (dis < 1.5f || dis > 5)
        {
            return;
        }
        offset = offset.normalized * dis;
    }
    //左右上下移动
    private void Rotate()
    {
        if ( Input.GetKey(KeyCode.LeftControl)&&Input.GetMouseButton(0) )
        {
            Vector3 pos = transform.position;
            Vector3 rot = transform.eulerAngles;

            //围绕原点旋转，也可以将Vector3.zero改为 target.position,就是围绕观察对象旋转
            transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
          //  transform.RotateAround(target.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 10);
            float x = transform.eulerAngles.x;
      //      float y = transform.eulerAngles.y;
            
//Debug.Log("y=" + y);
            //控制移动范围
            if (x < 20 || x > 45)
            {
                transform.position = pos;
                transform.eulerAngles = rot;
            }
            //  更新相对差值 
            offset = target.transform.position - this.transform.position;
        }
    }
}
