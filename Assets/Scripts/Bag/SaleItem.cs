using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;

public class SaleItem : MonoBehaviour
{

    [SerializeField] private Button button_sale;
    [SerializeField] private Button button_to_sale;
    [SerializeField] private GameObject sale_ui;
    [SerializeField] private Slider slider;
    [SerializeField] private InputField input;

    private bool showMessage = false;
    // Use this for initialization
    void Start()
    {

        button_sale.onClick.AddListener(
        delegate ()
            {
                // 这里添加你想要监听的事件
                Sale();
            }
        );
        button_to_sale.onClick.AddListener(
        delegate ()
            {
                // 这里添加你想要监听的事件
                ToSale();
            }
        );
        slider.onValueChanged.AddListener((float value) => SliderItemChange((int)value));
    }

    private void ToSale()
    {
        if (gameObject.GetComponent<Toggle>().isOn&& gameObject.transform.childCount > 2)
        {
            if (transform.GetChild(2).name == sale_ui.transform.GetChild(0).GetChild(0).name)
            {
                sale_ui.SetActive(false);
                Item i = ItemsRefresh.Instance.FindItem(transform.GetChild(2).name);//获取到要卖的物体的信息
                Debug.Log(int.Parse(sale_ui.transform.GetChild(8).GetComponent<Text>().text));
                i.number -= int.Parse(sale_ui.transform.GetChild(8).GetComponent<Text>().text);
                gameObject.transform.GetChild(1).GetComponent<Text>().text = i.number.ToString();
                ItemsRefresh.Instance.ReWrite();
                //加钱
                PlayerMes.getInstance().Gold += int.Parse(sale_ui.transform.GetChild(8).GetComponent<Text>().text) * i.worth;
                Debug.Log("加钱后" + PlayerMes.getInstance().Gold);





            }
        }
    }

    private void Sale()
    {
        if (gameObject.GetComponent<Toggle>().isOn && gameObject.transform.childCount > 2)//说明要买的时候有东西可卖
        {
            if (1 <= int.Parse(transform.GetChild(1).GetComponent<Text>().text))//还有足够的数量可卖
            {
                sale_ui.SetActive(true);
                Item i = ItemsRefresh.Instance.FindItem(transform.GetChild(2).name);//获取到要卖的物体的信息
                GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + i.name));
                temp.name = i.name;
                temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                temp.transform.position = sale_ui.transform.GetChild(0).position + new Vector3(-30, -30, 0);
                if (sale_ui.transform.GetChild(0).childCount > 0)
                {
                    Destroy(sale_ui.transform.GetChild(0).GetChild(0).gameObject);
                }
                temp.transform.SetParent(sale_ui.transform.GetChild(0));
                sale_ui.transform.GetChild(1).GetComponent<Text>().text = i.realName;
                //说明
                StringBuilder str = new StringBuilder();
                str.AppendFormat("<color=red>{0}</color>\n", i.realName);
                str.AppendFormat("拥有:{0}\n", i.number);
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
                sale_ui.transform.GetChild(2).GetComponent<Text>().text = str.ToString();
                //控制数量
                slider.GetComponent<Slider>().maxValue = i.number;//设置总数
                slider.GetComponent<Slider>().value = i.number / 2;
                slider.transform.GetChild(4).GetComponent<Text>().text = i.number.ToString();
            }
        }
    }

    private void SliderItemChange(int val)
    {
        sale_ui.transform.GetChild(8).GetComponent<Text>().text = val.ToString();
    }

    public void OnMouseEnter()
    {
        if (gameObject.transform.childCount > 2)//如果说有道具就可以显示
        {

            showMessage = true;
        }
    }
    public void OnMouseExit()
    {
        showMessage = false;
    }
    public void OnGUI()
    {
        if (showMessage)
        {
            GUI.Window(0, new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 150, 170), Window2, "物品信息");
        }
    }
    public void Window2(int WindowID)
    {
        Item item = ItemsRefresh.Instance.FindItem(gameObject.transform.GetChild(2).name);
        GUIStyle style1 = new GUIStyle();
        style1.fontSize = 15;
        style1.normal.textColor = Color.yellow;
        GUIStyle style2 = new GUIStyle();
        style2.fontSize = 10;
        style2.normal.textColor = Color.green;
        GUILayout.Label(item.realName, style1);
        GUILayout.Label("  ", style1);
        GUILayout.Label("拥有:    " + item.number, style1);
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
