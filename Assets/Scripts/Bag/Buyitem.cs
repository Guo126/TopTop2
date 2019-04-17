using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Buyitem : MonoBehaviour
{

    [SerializeField] private Button button_buy;
    [SerializeField] private Button button_to_buy;
    [SerializeField] private GameObject buy_ui;
    [SerializeField] private InputField input;

    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;

    private bool isShowWin = false;

    // Use this for initialization
    void Start()
    {
        input.onEndEdit.AddListener(
            delegate { ValueChangeCheck(); }
            );
        button_buy.onClick.AddListener(
        delegate ()
        {
            // 这里添加你想要监听的事件
            Buy();
        }
        );
        button_to_buy.onClick.AddListener(
        delegate ()
        {
            // 这里添加你想要监听的事件
            ToBuy();
        }
        );
    }

    private void ValueChangeCheck()
    {
        if (buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text.Length >0)
        {
            buy_ui.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = (20 * int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text)).ToString();
        }
    }

    private void ToBuy()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            buy_ui.SetActive(false);
            if (buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text.Length > 0)
            {
                //背包添加
                Item exist = ItemsRefresh.Instance.FindItem(buy_ui.transform.GetChild(1).GetChild(0).name);//找到bag里的
                Item temp = ItemsRefresh.Instance.FindStoreItem(transform.GetChild(2).name);//找到store里的
                if (exist != null)
                {
                    //bag里有
                    exist.number += int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text);//数据更新
                    //列表更新
                    if (exist.type == "Medition")
                    {
                        for (int i = 0; i < panel1.transform.GetChild(0).childCount; i++)
                        {
                            if (exist.name == panel1.transform.GetChild(0).GetChild(i).GetChild(2).name)
                            {
                                panel1.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = exist.number.ToString();
                                break;
                            }
                        }
                    }
                    if (exist.type == "Equipment")
                    {
                        for (int i = 0; i < panel2.transform.GetChild(0).childCount; i++)
                        {
                            if (exist.name == panel2.transform.GetChild(0).GetChild(i).GetChild(2).name)
                            {
                                panel2.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = exist.number.ToString();
                                break;
                            }
                        }
                    }
                    if (exist.type == "OtherItem")
                    {
                        for (int i = 0; i < panel3.transform.GetChild(0).childCount; i++)
                        {
                            if (exist.name == panel3.transform.GetChild(0).GetChild(i).GetChild(2).name)
                            {
                                panel3.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = exist.number.ToString();
                                break;
                            }
                        }
                    }
                    //扣钱
                    PlayerMes.getInstance().Gold -= int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text) * exist.worth;
                }
                else
                {
                    //没有
                    GameObject g = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + temp.name));
                    g.name = temp.name;
                    g.transform.localScale=new Vector3(0.5f, 0.5f, 0.5f);
                    if (temp.type == "Medition")
                    {
                        temp.number = 0;
                        temp.number += int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text);
                        ItemsRefresh.Instance.bagList[0].Add(temp);
                        for (int i = 0; i < panel1.transform.GetChild(0).childCount; i++)
                        {
                            if (panel1.transform.GetChild(0).GetChild(i).childCount<3)
                            {
                                g.transform.position = panel1.transform.GetChild(0).GetChild(i).position;
                                g.transform.SetParent(panel1.transform.GetChild(0).GetChild(i));

                                panel1.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = temp.number.ToString();
                                break;
                            }
                        }
                    }
                    if (temp.type == "Equipment")
                    {
                        temp.number = 0;
                        temp.number += int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text);
                        ItemsRefresh.Instance.bagList[0].Add(temp);
                        for (int i = 0; i < panel2.transform.GetChild(0).childCount; i++)
                        {
                            if (panel2.transform.GetChild(0).GetChild(i).childCount < 3)
                            {
                                g.transform.position = panel2.transform.GetChild(0).GetChild(i).position;
                                g.transform.SetParent(panel2.transform.GetChild(0).GetChild(i));
                                panel2.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = temp.number.ToString();
                                break;
                            }
                        }
                    }
                    if (temp.type == "OtherItem")
                    {
                        temp.number = 0;
                        temp.number += int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text);
                        ItemsRefresh.Instance.bagList[0].Add(temp);
                        for (int i = 0; i < panel2.transform.GetChild(0).childCount; i++)
                        {
                            if (panel2.transform.GetChild(0).GetChild(i).childCount < 3)
                            {
                                g.transform.position = panel2.transform.GetChild(0).GetChild(i).position;
                                g.transform.SetParent(panel2.transform.GetChild(0).GetChild(i));
                                panel2.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = temp.number.ToString();
                                break;
                            }
                        }
                    }
                    //扣钱
                    PlayerMes.getInstance().Gold -= int.Parse(buy_ui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text) * temp.worth;

                }
                ItemsRefresh.Instance.ReWrite();
                Debug.Log("扣钱后" + PlayerMes.getInstance().Gold);




            }
        }
    }

    private void Buy()
    {
        if (gameObject.GetComponent<Toggle>().isOn && gameObject.transform.childCount > 2)//说明要买的时候有东西可卖
        {
            buy_ui.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = "";
            input.text = "";
            buy_ui.SetActive(true);
            if (buy_ui.transform.GetChild(1).childCount > 0)
            {
                Destroy(buy_ui.transform.GetChild(1).GetChild(0).gameObject);
            }
            Item i = ItemsRefresh.Instance.FindStoreItem(transform.GetChild(2).name);//获取到要卖的物体的信息
            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + i.name));
            temp.name = i.name;
            temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            temp.transform.position = buy_ui.transform.GetChild(1).position + new Vector3(-30, -30, 0);
            temp.transform.SetParent(buy_ui.transform.GetChild(1));
            buy_ui.transform.GetChild(4).GetComponent<Text>().text = i.realName;
            //说明
            StringBuilder str = new StringBuilder();
            str.AppendFormat("<color=red>{0}</color>\n", i.realName);
            if (i.addBlood != 0)
            {
                str.AppendFormat("恢复血量:{0}\n", i.addBlood);
            }
            if (i.addBloodMax != 0)
            {
                str.AppendFormat("增加血量上限:{0}\n", i.addBlood);
            }
            if (i.addMagicNum != 0)
            {
                str.AppendFormat("增加法量:{0}\n", i.addMagicNum);
            }
            if (i.addMagicMax != 0)
            {
                str.AppendFormat("增加法量上限:{0}\n", i.addMagicMax);
            }
            if (i.addAtk != 0)
            {
                str.AppendFormat("增加攻击力:{0}\n", i.addAtk);
            }
            if (i.addDef != 0)
            {
                str.AppendFormat("增加防御:{0}\n", i.addDef);
            }
            if (i.addEvd != 0)
            {
                str.AppendFormat("增加避闪率:{0}\n", i.addEvd);
            }
            if (i.addCrt != 0)
            {
                str.AppendFormat("增加暴击:{0}\n", i.addCrt);
            }
            buy_ui.transform.GetChild(2).GetComponent<Text>().text = str.ToString();


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseEnter()
    {
        if (gameObject.transform.childCount > 2)//如果说有道具就可以显示
        {

            isShowWin = true;
        }
    }
    public void OnMouseExit()
    {
        isShowWin = false;
    }
    public void OnGUI()
    {
        if (isShowWin)
        {
            GUI.Window(0, new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 150, 170), Window3, "物品信息");
        }
    }
    public void Window3(int WindowID)
    {
        Item item = ItemsRefresh.Instance.FindStoreItem(gameObject.transform.GetChild(2).name);
        GUIStyle style1 = new GUIStyle();
        style1.fontSize = 15;
        style1.normal.textColor = Color.yellow;
        GUIStyle style2 = new GUIStyle();
        style2.fontSize = 10;
        style2.normal.textColor = Color.green;
        GUILayout.Label(item.realName, style1);
        GUILayout.Label("  ", style1);
        if (item.addBlood != 0)
        {
            GUILayout.Label("增加血量:    " + item.addBlood, style1);
        }
        if (item.addBloodMax != 0)
        {
            GUILayout.Label("增加血量上限:    " + item.addBlood, style1);
        }
        if (item.addMagicNum != 0)
        {
            GUILayout.Label("增加法量:    " + item.addMagicNum, style1);
        }
        if (item.addMagicMax != 0)
        {
            GUILayout.Label("增加法量上限:    " + item.addMagicMax, style1);
        }
        if (item.addAtk != 0)
        {
            GUILayout.Label("增加攻击力:    " + item.addAtk, style1);
        }
        if (item.addDef != 0)
        {
            GUILayout.Label("增加防御:    " + item.addDef, style1);
        }
        if (item.addEvd != 0)
        {
            GUILayout.Label("增加避闪率:    " + item.addEvd, style1);
        }
        if (item.addCrt != 0)
        {
            GUILayout.Label("增加暴击:    " + item.addCrt, style1);
        }
        //物品信息排版

    }


}
