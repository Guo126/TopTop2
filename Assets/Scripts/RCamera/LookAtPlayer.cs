using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private GameObject target;
    
    private Vector3 offset,rot;
    private bool isTurn;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.transform.position - this.transform.position;
         rot = transform.eulerAngles;
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
        dis -= Input.GetAxis("Mouse ScrollWheel") * 5;
       
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
            //Vector3 pos1 = transform.position;
            Vector3 rot1 = transform.eulerAngles;


            //围绕原点旋转，也可以将Vector3.zero改为 target.position,就是围绕观察对象旋转
            transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
           // transform.RotateAround(transform.position, Vector3.right, Input.GetAxis("Mouse Y") * 10);


            float x = transform.eulerAngles.x;
            
            //控制移动范围
            if (x < 20 || x > 52)
            {
              //  transform.position = pos1;
                transform.eulerAngles = rot1;
            }

            //  更新相对差值 
            // offset = target.transform.position - this.transform.position;
        }

      Vector3 tr;
       // if (!Input.GetMouseButtonDown(0))
       if(Input.GetMouseButtonUp(0))
        {
            // tr = transform.eulerAngles;
            //if ((tr - rot).magnitude > 1)
            //{
            //    transform.RotateAround(transform.position, Vector3.up, 3);
            //}
             transform.eulerAngles = rot;


        }
       
       



    }
}
