
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemUseAndDestory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Material mat;
    [SerializeField] private GameObject equip;
    [SerializeField] private Button useButton;
    [SerializeField] private Button deleteButton;


    [SerializeField]
    private Weapon weapon;
    
    private GameObject g;
    public IUnityEvent op;
    private Transform trans;

    private Picture itemPic = new Picture();

    private bool isShowTip = false;
    private bool WindowShow = true;

    //内部picture类
    class Picture
    {
        //public GameObject image;
        public Vector3 offset;//差值（鼠标和图片中心）
        public Transform nowParent = null;//当前的所在的格子(父物体)
        public GameObject image;
        public Picture()
        {

        }
    }

    private void Reset()
    {
        Material material =(Material) Resources.Load("Hero/blue");
        mat = material;
        equip = GameObject.Find("equip_0x");
        useButton = GameObject.Find("Button_useItem (1)").GetComponent<Button>();
        deleteButton = GameObject.Find("Button_destoryitem (1)").GetComponent<Button>();
    }


    void Start()
    {
        trans = GameObject.Find("Main_Canvas").transform;
                useButton.onClick.AddListener(
                delegate ()
                {
                     // 这里添加你想要监听的事件
                    Use();
                }
            );

            deleteButton.onClick.AddListener(
                delegate ()
                {
                    // 这里添加你想要监听的事件
                    Destory();
                }
            );



        weapon = GameObject.FindObjectOfType<Weapon>();
    }

    public void OnMouseEnter()
    {
        g = this.gameObject;
        if (g.transform.childCount > 2)//如果说有道具就可以显示
        {
            isShowTip = true;
        }
    }
    public void OnMouseExit()
    {
        isShowTip = false;
    }
    public void OnGUI()
    {
        if (isShowTip)
        {
            GUI.Window(0, new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 150, 170), Window1, "物品信息");
        }

       

    }

    public void Window1(int WindowID)
    {
        Item item = ItemsRefresh.Instance.FindItem(g.transform.GetChild(2).name);
        GUIStyle style1 = new GUIStyle();
        style1.fontSize = 15;
        style1.normal.textColor = Color.yellow;
        GUIStyle style2 = new GUIStyle();
        style2.fontSize = 10;
        style2.normal.textColor = Color.green;
        GUILayout.Label(item.realName, style1);
        GUILayout.Label("  ", style1);
        GUILayout.Label("拥有:    " + item.number, style1);
        GUILayout.Label("价值:    " + item.worth, style1);
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
    //使用物品或者销毁物品
    public void Use()
    {
        //Debug.Log(1);
        if (gameObject.GetComponent<Toggle>().isOn && gameObject.transform.childCount > 2)//说明有道具
        {
            if (1 <= int.Parse(transform.GetChild(1).GetComponent<Text>().text))//道具数不为0
            {
                Item i = ItemsRefresh.Instance.FindItem(transform.GetChild(2).name);
                if (i.type == "Equipment")//属于装备类
                {
                    GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + i.name));//找到该预制体
                    temp.name = i.name;
                    if (equip.transform.Find(temp.tag).childCount == 2)
                    {
                        transform.GetChild(1).GetComponent<Text>().text = (i.number - 1).ToString();
                        temp.transform.position = equip.transform.Find(temp.tag).position;
                        temp.transform.SetParent(equip.transform.Find(temp.tag));
                        temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        ItemEffect(i, true);
                    }
                    else if (equip.transform.Find(temp.tag).childCount == 3)
                    {
                        Item g = ItemsRefresh.Instance.FindItem(equip.transform.Find(temp.tag).GetChild(2).name);
                        //特别处理
                        if (i.name != equip.transform.Find(temp.tag).GetChild(2).name)
                        {
                            //放入
                            transform.GetChild(1).GetComponent<Text>().text = (i.number - 1).ToString();
                            temp.transform.position = equip.transform.Find(temp.tag).position;
                            temp.transform.SetParent(equip.transform.Find(temp.tag));
                            temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            //放回

                            for (int j = 0; j < gameObject.transform.parent.childCount; j++)
                            {
                                if (gameObject.transform.parent.GetChild(j).childCount > 2 && gameObject.transform.parent.GetChild(j).GetChild(2).name == equip.transform.Find(temp.tag).GetChild(2).name)
                                {
                                    gameObject.transform.parent.GetChild(j).GetChild(1).GetComponent<Text>().text = (int.Parse(gameObject.transform.parent.GetChild(j).GetChild(1).GetComponent<Text>().text) + 1).ToString();
                                }
                            }
                            Destroy(equip.transform.Find(temp.tag).GetChild(2).gameObject);

                            ItemEffect(i, true);
                            ItemEffect(g, false);
                        }
                        else
                        {
                            Destroy(temp);
                        }
                    }
                }
                else
                {
                    if (PlayerMes.getInstance().BloodNum == PlayerMes.getInstance().BloodMax)
                        return;
                    i.number -= 1;
                    transform.GetChild(1).GetComponent<Text>().text = i.number.ToString();
                    //效果
                    ItemEffect(i, true);
                }
                ItemsRefresh.Instance.ReWrite();

            }
        }
    }

    public void Destory()
    {
        if (gameObject.GetComponent<Toggle>().isOn && gameObject.transform.childCount > 2)//说明有道具
        {
            if (1 <= int.Parse(transform.GetChild(1).GetComponent<Text>().text))//道具数不为0
            {
                Item i = ItemsRefresh.Instance.FindItem(transform.GetChild(2).name);
                i.number -= 1;
                transform.GetChild(1).GetComponent<Text>().text = (int.Parse(transform.GetChild(1).GetComponent<Text>().text) - 1).ToString();
                ItemsRefresh.Instance.ReWrite();

            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.tag == "equipment")//是装备才可拖动
        {
            if (eventData.pointerEnter.transform.name != "slot_down")//如果是slot的话就不处理
            {
                //给pic初始化
                itemPic.image = eventData.pointerEnter;//将被拖拽的物体付给itemPic.image使之被记录
                itemPic.image.transform.SetParent(trans);
                itemPic.offset = itemPic.image.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                itemPic.nowParent = trans;

                GameObject image = (GameObject)Instantiate(Resources.Load("Prefabs/equip/" + eventData.pointerEnter.transform.name));//找到该预制体
                image.name = eventData.pointerEnter.transform.name;
                image.transform.position = gameObject.transform.position;
                image.transform.SetParent(gameObject.transform);
                image.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                IsRaycastLocationValid(false);//射线可穿透点击物体
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(trans as RectTransform, Input.mousePosition, null, out pos))
        {
            itemPic.image.transform.position = pos + new Vector3(-30, -30, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;//记录射线获取到的物体
        if (go != null && go.transform.parent != null)
        {
            Item g = ItemsRefresh.Instance.FindItem(itemPic.image.name);

            //没放进去（不是指定区域），Destory
            if (itemPic.image.tag != go.transform.parent.name)
            {
                Destroy(itemPic.image);
            }//放在指定区域且为空槽，直接放入，装备数字减一，实际装备数不变
            else if (itemPic.image.tag == go.transform.parent.name && go.transform.parent.childCount == 2)
            {
                itemPic.image.transform.position = go.transform.parent.position;
                itemPic.image.transform.SetParent(go.transform.parent);
                gameObject.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(gameObject.transform.GetChild(1).GetComponent<Text>().text) - 1).ToString();
                ItemEffect(g, true);
            }//放在指定区域不是空槽，占位
            else if (itemPic.image.tag == go.transform.parent.name && go.transform.childCount != 2)
            {
                //装备一样，Destory
                if (itemPic.image.name == go.transform.parent.GetChild(2).name)
                {
                    Destroy(itemPic.image);
                }
                else
                {
                    //不一样,取下加一，换新减一，
                    Transform temp = go.transform;
                    Item backItem = ItemsRefresh.Instance.FindItem(temp.name);
                    for (int i = 0; i < gameObject.transform.parent.childCount; i++)
                    {
                        if (gameObject.transform.parent.GetChild(i).childCount > 2 && gameObject.transform.parent.GetChild(i).GetChild(2).name == temp.name)
                        {
                            gameObject.transform.parent.GetChild(i).GetChild(1).GetComponent<Text>().text = (int.Parse(gameObject.transform.parent.GetChild(i).GetChild(1).GetComponent<Text>().text) + 1).ToString();
                        }
                    }
                    itemPic.image.transform.position = go.transform.parent.position;
                    itemPic.image.transform.SetParent(go.transform.parent);
                    gameObject.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(gameObject.transform.GetChild(1).GetComponent<Text>().text) - 1).ToString();
                    Destroy(go);
                    ItemEffect(g, true);
                    ItemEffect(backItem, false);
                }
            }
            IsRaycastLocationValid(true);
        }
        else
        {
            Destroy(itemPic.image);
        }
    }

    private void IsRaycastLocationValid(bool flag)
    {
        itemPic.image.GetComponent<Image>().raycastTarget = flag;
    }

    public void ItemEffect(Item item, bool isAdd)
    {
        if (isAdd)
        {
            
            if (item.addBlood != 0)
            {
                
                if (PlayerMes.getInstance().BloodNum < PlayerMes.getInstance().BloodMax)
                {                     
                        PlayerMes.getInstance().BloodNum += item.addBlood;                      
                        op.Invoke(1);                   
                }
            }
            if (item.addBloodMax != 0)
            {
                PlayerMes.getInstance().BloodMax += item.addBloodMax;
                op.Invoke(2);
            }
            if (item.addMagicNum != 0)
            {
                if (PlayerMes.getInstance().MagicNum < PlayerMes.getInstance().MagicMax)
                {                  
                        PlayerMes.getInstance().MagicNum += item.addMagicNum;
                        op.Invoke(1);                  
                }
            }
            if (item.addMagicMax != 0)
            {
                PlayerMes.getInstance().MagicMax += item.addMagicMax;
                op.Invoke(2);
            }
            if (item.addAtk != 0)
            {
                PlayerMes.getInstance().Attack += item.addAtk;
                weapon.SetColor(new Color(70/255,  1, 1));
                op.Invoke(2);
            }
            if (item.addDef != 0)
            {
                Debug.Log("加防御");
                PlayerMes.getInstance().Defence += item.addDef;
                
                op.Invoke(2);
            }
            if (item.addEvd != 0)
            {
                Debug.Log("加避闪");
                PlayerMes.getInstance().HidePer += item.addEvd;
                op.Invoke(2);
            }
            if (item.addCrt != 0)
            {
                PlayerMes.getInstance().BigHit += item.addCrt;
                op.Invoke(2);
            }
        }
        else
        {
            if (item.addBloodMax != 0)
            {
                PlayerMes.getInstance().BloodMax -= item.addBloodMax;
                op.Invoke(2);
            }
            if (item.addMagicMax != 0)
            {
                PlayerMes.getInstance().MagicMax -= item.addMagicMax;
                op.Invoke(2);
            }
            if (item.addAtk != 0)
            {
                PlayerMes.getInstance().Attack -= item.addAtk;
                op.Invoke(2);
            }
            if (item.addDef != 0)
            {
                PlayerMes.getInstance().Defence -= item.addDef;
                op.Invoke(2);
            }
            if (item.addEvd != 0)
            {
                PlayerMes.getInstance().HidePer -= item.addEvd;
                op.Invoke(2);
            }
            if (item.addCrt != 0)
            {
                PlayerMes.getInstance().BigHit -= item.addCrt;
                op.Invoke(2);
            }
        }
    }

}














