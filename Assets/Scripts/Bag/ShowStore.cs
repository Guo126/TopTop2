using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStore : MonoBehaviour
{

    [SerializeField] private GameObject its1;
    [SerializeField] private GameObject its2;
    [SerializeField] private GameObject its3;
    //private int type;
    // Use this for initialization
    void Start()
    {
        LoadStoreItem(0, its1);
        LoadStoreItem(1, its2);
        LoadStoreItem(2, its3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //加载商店物体
    public void LoadStoreItem(int t, GameObject items)
    {
        GameObject gameObj;
        switch (t)
        {
            case 0:
                for (int j = ItemsRefresh.Instance.itemList[0].Count - 1; j >= 0; j--)
                {
                    if (null != ItemsRefresh.Instance.itemList[0][j])
                    {
                        gameObj = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + ItemsRefresh.Instance.itemList[0][j].name));
                        gameObj.name = ItemsRefresh.Instance.itemList[0][j].name;
                        gameObj.transform.position = items.transform.GetChild(j).position;
                        gameObj.transform.SetParent(items.transform.GetChild(j));
                        items.transform.GetChild(j).GetChild(1).GetComponent<Text>().text = "$"+ItemsRefresh.Instance.itemList[0][j].worth.ToString();
                        Debug.Log(gameObj.name);
                    }
                }
                break;
            case 1:
                for (int j = ItemsRefresh.Instance.itemList[1].Count - 1; j >= 0; j--)
                {
                    if (null != ItemsRefresh.Instance.itemList[1][j])
                    {
                        gameObj = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + ItemsRefresh.Instance.itemList[1][j].name));
                        gameObj.name = ItemsRefresh.Instance.itemList[1][j].name;
                        gameObj.transform.position = items.transform.GetChild(j).position;
                        gameObj.transform.SetParent(items.transform.GetChild(j));
                        items.transform.GetChild(j).GetChild(1).GetComponent<Text>().text = "$" + ItemsRefresh.Instance.itemList[1][j].worth.ToString();
                        Debug.Log(gameObj.name);
                    }
                }
                break;
            case 2:
                for (int j = ItemsRefresh.Instance.itemList[2].Count - 1; j >= 0; j--)
                {
                    if (null != ItemsRefresh.Instance.itemList[2][j])
                    {
                        gameObj = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + ItemsRefresh.Instance.itemList[2][j].name));
                        gameObj.name = ItemsRefresh.Instance.itemList[2][j].name;
                        gameObj.transform.position = items.transform.GetChild(j).position;
                        gameObj.transform.SetParent(items.transform.GetChild(j));
                        items.transform.GetChild(j).GetChild(1).GetComponent<Text>().text = "$" + ItemsRefresh.Instance.itemList[2][j].worth.ToString();
                        Debug.Log(gameObj.name);
                    }
                }
                break;
            default:
                break;
        }
    }
}
