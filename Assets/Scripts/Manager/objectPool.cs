﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPool {

    
    private static objectPool instance;

    private Dictionary<string, List<GameObject>> pool;
    private Dictionary<string, GameObject> prefabs;

    public GameObject heroPos;

    private objectPool()
    {
        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
    }

    public static objectPool GetInstance()
    {
        if (instance == null)
        {
            instance = new objectPool();
        }
        return instance;
    }

    public void DeleteAll()
    {
        pool.Clear();
        prefabs.Clear();
    }
  
    public GameObject GetObj(string objName)
    {
        
        GameObject result = null;
        //判断是否有该名字的对象池
        if (pool.ContainsKey(objName))
        {
            //对象池里有对象
            if (pool[objName].Count > 0)
            {
                //获取结果
                result = pool[objName][0];
                //激活对象
                result.SetActive(true);
                //从池中移除该对象
                pool[objName].Remove(result);
                //返回结果
                return result;
            }
        }
        

        GameObject prefab = null;
        //如果已经加载过该预设体
        if (prefabs.ContainsKey(objName))
        {
            prefab = prefabs[objName];
        }
        else     //如果没有加载过该预设体
        {
            //加载预设体

            prefab = Resources.Load<GameObject>( "Pool/" + objName);
            
            //更新字典
            prefabs.Add(objName, prefab);
            
        }

      

        //生成
        result = GameObject.Instantiate(prefab);
        
        result.transform.position = new Vector3(-6.2f, 0.3f, -20);
        //改名（去除 Clone）
        result.name = objName;

 

        //返回
        return result;
    }



    public void RecycleObj(GameObject obj)
    {
        //设置为非激活
        obj.SetActive(false);
        //判断是否有该对象的对象池
        if (pool.ContainsKey(obj.name))
        {          
            pool[obj.name].Add(obj);
        }
        else
        {
            //创建该类型的池子，并将对象放入
            pool.Add(obj.name, new List<GameObject>() { obj });
        }

    }

}
