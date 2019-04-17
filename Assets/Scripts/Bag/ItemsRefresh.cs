using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsRefresh : MonoBehaviour {
    [SerializeField]private GameObject items1;
    [SerializeField]private GameObject items2;
    [SerializeField]private GameObject items3;
    public List<List<Item>> bagList;
    public List<List<Item>> itemList;

    private string filename;
    private string filename1;
    public static ItemsRefresh instance;
    public static ItemsRefresh Instance
    {
        get
        {
            return instance;
        }

    }
    void Awake()
    {
        filename = Application.streamingAssetsPath+"/Save/GameBagData.sav";
        filename1 = Application.streamingAssetsPath + "/Save/GameItemData.sav";
        bagList=new List<List<Item>>();
        bagList = new List<List<Item>>();
        bagList=(List<List<Item>>)IOHelper.GetData(filename,typeof(List<List<Item>>));
        itemList= (List<List<Item>>)IOHelper.GetData(filename1, typeof(List<List<Item>>));
        instance = this;
    }

    void Start()
    {
        LoadItems();
    }



    //返回保存的所有物品
    public List<List<Item>> GetAllItems()
    {
        return (List<List<Item>>)IOHelper.GetData(filename,typeof(List<List<Item>>));
    }

    //查询给定名字的物品，若有则返回；没有则返回NULL
    public Item FindItem(string strName)
    {
        
        for(int i=0;i<bagList.Count;i++)
        {
            for(int j=0;j<bagList[i].Count;j++)
            {
                if(strName==bagList[i][j].name)
                {
                    return bagList[i][j];
                }
            }
        }
        return null;
    }

    public Item FindStoreItem(string strName)
    {

        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < itemList[i].Count; j++)
            {
                if (strName == itemList[i][j].name)
                {
                    return itemList[i][j];
                }
            }
        }
        return null;
    }
    //加载所有物品
    public void LoadItems()
    {
        GameObject temp;
        NoItemSelect();
        for(int i=0;i<bagList.Count;i++)
        {
            for(int j=bagList[i].Count-1;j>=0;j--)
            {
                if(null!=bagList[i][j])
                {
                    temp = (GameObject)Instantiate(Resources.Load("Prefabs/equip/"+bagList[i][j].name));
                    //Debug.Log(temp.name);
                    temp.name=bagList[i][j].name;
                    if(0==i)
                    {
                        if(bagList[i][j].number>0)
                        {
                            temp.transform.position=items1.transform.GetChild(j).position;
                            temp.transform.SetParent(items1.transform.GetChild(j));
                            items1.transform.GetChild(j).GetChild(1).GetComponent<Text>().text=bagList[i][j].number.ToString();
                        }
                    }
                    if(1==i)
                    { 
                        if(bagList[i][j].number>0)
                        {
                            temp.transform.position=items2.transform.GetChild(j).position;
                            temp.transform.SetParent(items2.transform.GetChild(j));
                            items2.transform.GetChild(j).GetChild(1).GetComponent<Text>().text=bagList[i][j].number.ToString();
                        }  
                    }
                    if(2==i)
                    {
                        if(bagList[i][j].number>0)
                        {
                            temp.transform.position=items3.transform.GetChild(j).position;
                            temp.transform.SetParent(items3.transform.GetChild(j));
                            items3.transform.GetChild(j).GetChild(1).GetComponent<Text>().text=bagList[i][j].number.ToString();
                        }
                    }
                }
            
            }
        }
    }

    public void ReWrite()
    {
        IOHelper.SetData(filename,bagList);
    }
    public void NoItemSelect()
    {
        for(int i=0;i<bagList.Count;i++)
        {
            for(int j=bagList[i].Count-1;j>=0;j--)
            {
                if(bagList[i][j].number<=0)
                {
                    bagList[i].RemoveAt(j);
                }
            }
        }
    }
}