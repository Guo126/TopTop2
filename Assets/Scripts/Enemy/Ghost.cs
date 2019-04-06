using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private int blood=1000;
    private float activityRange = 50f;
    private Vector3 defoultPosition=Vector3.zero;
    private float fightRange = 10f;
    private float moveSpeed = 2f;
    private bool isFightStatus = false;

    public float ActivityRange
    {
        get
        {
            return activityRange;
        }

        set
        {
            activityRange = value;
        }
    }

    public Vector3 DefoultPosition
    {
        get
        {
            return defoultPosition;
        }

        set
        {
            defoultPosition = value;
        }
    }

    public float FightRange
    {
        get
        {
            return fightRange;
        }

        set
        {
            fightRange = value;
        }
    }

    public int Blood
    {
        get
        {
            return blood;
        }

        set
        {
            blood = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public bool IsFightStatus
    {
        get
        {
            return isFightStatus;
        }

        set
        {
            isFightStatus = value;
        }
    }

    public Ghost(int b,float ar,Vector3 dp,float fr,float ms,bool ifs)
    {
        blood = b;
        activityRange = ar;
        defoultPosition = dp;
        fightRange = fr;
        moveSpeed = ms;
        isFightStatus = ifs;
    }
    
}
