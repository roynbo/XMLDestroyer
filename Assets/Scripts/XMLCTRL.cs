using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System;

public class XMLCTRL : MonoBehaviour
{
    XMLTool xmlTool = XMLTool.Instance;
    XmlDocument xmlDoc;
    public Text txXMLName;
    public GameObject BasicCell_ele;
    public GameObject BasicCell_tx;
    public GameObject xianShi;
    public GameObject xuanxiang;
    GameObject[] options = new GameObject[9];

    MultiTree root = new MultiTree();
    MultiTree curNode = new MultiTree();
    int curIndex = 0;

    Vector3[] cellPos = new Vector3[10];
    List<GameObject> showCells = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            options[i] = xuanxiang.transform.Find("选项" + i.ToString()).gameObject;
        }
        cellPos[0] = new Vector3(0, 540, 0);
        cellPos[1] = new Vector3(0, 420, 0);
        cellPos[2] = new Vector3(0, 300, 0);
        cellPos[3] = new Vector3(0, 180, 0);
        cellPos[4] = new Vector3(0, 60, 0);
        cellPos[5] = new Vector3(0, -60, 0);
        cellPos[6] = new Vector3(0, -180, 0);
        cellPos[7] = new Vector3(0, -300, 0);
        cellPos[8] = new Vector3(0, -420, 0);
        cellPos[9] = new Vector3(0, -540, 0);
        //CeShiInit();
    }

    // Update is called once per frame
    void Update()
    {
        txXMLName.text = xmlTool.xmlName;
        if(xmlTool.change)
        {
            xmlTool.change = false;
            Init();
        }
        if(curNode.parent==null)
        {
            options[0].transform.Find("btn选项").GetComponent<Button>().interactable = false;
        }
        else
        {
            options[0].transform.Find("btn选项").GetComponent<Button>().interactable = true;
        }
    }

    void CeShiInit()
    {
        xmlTool.setPath = "D:/XMLDestroyer/Assets/StreamingAssets/XMLS/Boxian.xml";
        xmlTool.TryRead();
        root.BuildTree(xmlTool.xmlDoc);
        curNode = root;
        InitCell(root.child);
    }
    void Init()
    {
        root = new MultiTree();
        root.BuildTree(xmlTool.xmlDoc);
        curNode = root;
        InitCell(root.child);
    }
    void InitCell(List<MultiTree> nodes)
    {
        for (int i = 0; i < showCells.Count; i++)
        {
            Destroy(showCells[i]);
        }
        showCells.Clear();
        for (int i = 0; i < 10 && i < nodes.Count; i++)
        {
            if (nodes[i].text == null)
            {
                GameObject cell = GameObject.Instantiate(BasicCell_ele);
                cell.name = "基础格子-元素" + i.ToString();
                cell.transform.parent = xianShi.transform;
                cell.transform.localPosition = cellPos[i];
                cell.transform.localScale = new Vector3(1, 1, 1);
                GameObject btn = cell.transform.Find("Button").gameObject;
                btn.name = i.ToString();
                Text tx = btn.transform.Find("Text").gameObject.GetComponent<Text>();
                tx.text = nodes[i].name;
                Button button = btn.GetComponent<Button>();
                button.onClick.AddListener(NextNode);
                showCells.Add(cell);
            }
            else
            {
                GameObject cell = GameObject.Instantiate(BasicCell_tx);
                cell.name = "基础格子-文本" + i.ToString();
                cell.transform.parent = xianShi.transform;
                cell.transform.localPosition = cellPos[i];
                cell.transform.localScale = new Vector3(1, 1, 1);
                Text tx = cell.transform.Find("Text").gameObject.GetComponent<Text>();
                tx.text = nodes[i].text;
                showCells.Add(cell);
            }
        }
    }
    void NextNode()
    {
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        curIndex = Convert.ToInt32(buttonSelf.name);
        curNode = curNode.child[curIndex];
        InitCell(curNode.child);
    }

    public void BackNode()
    {
        if (curNode.parent != null)
        {
            curNode = curNode.parent;
            InitCell(curNode.child);
        }
    }
}
