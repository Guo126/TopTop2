using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ShowWords : MonoBehaviour {

    private List<string> chatList;
    private int chatCount,index=0;
    private GameObject npc, me, text,player;
    public string xmlName=null;
    private bool hasXml =false;

    // Use this for initialization
    void Start () {
        //获取三个物体
        npc = gameObject.transform.GetChild(0).gameObject;
        text = gameObject.transform.GetChild(1).gameObject;
        me = gameObject.transform.GetChild(2).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        //xml编辑器
        xmlName = "none";
        //储存句子
        chatList = new List<string>();
        //string data = Resources.Load("Txt/shop").ToString();
        //xmlDocument.Load(data);
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasXml && xmlName!="none")
        {

            
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/Resources/Txt/" + xmlName + ".xml");
           // print(Application.dataPath + "/Resources/Txt/" + xmlName + ".xml");
            //找到dialogues的所有子节点
            XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("dialogues").ChildNodes;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                chatList.Add(xmlElement.ChildNodes.Item(0).InnerText + "\n" + xmlElement.ChildNodes.Item(1).InnerText);
            }
            chatCount = chatList.Count;
            chat_handle(0);
            hasXml = true;
        }
        if (Input.GetMouseButtonDown(0))//如果点击了鼠标左键
        {
            index++;//对话跳到一下个
            if (index < chatCount)//如果对话还没有完
            {
                chat_handle(index);//那就载入下一条对话
            }
            else
            {
                //对话完了              
                player.GetComponent<ChatWith>().isChat = false;
                gameObject.SetActive(false);
                
            }
        }

       
	}
    
    private void chat_handle(int index)
    {
        //切割数组
        string[] role_detail_array = chatList[index].Split('\n');//list中每一个对话格式就是“角色名\n对话”
        string role = role_detail_array[0];
        string role_detail = role_detail_array[1];
        if (role == "A")
        {
            npc.SetActive(true);
            me.SetActive(false);
        }
        else
        {
            me.SetActive(true);
            npc.SetActive(false);
        }
        text.transform.GetChild(0).GetComponent<Text>().text = role_detail;

    }


}
