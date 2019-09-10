using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    public Animation ani;

    //读取文件夹中的文件
    private string m_path;
    private int m_file_cnt;
    private List<string> xmlList = new List<string>();

    //显示到dropdown
    public Dropdown dropdown;
    private List<string> dropdownList = new List<string>();

    //选中读取的文件
    int chosenIndex;
    XMLTool xmlTool = XMLTool.Instance;
    public Text txXMLStatus;
    // Start is called before the first frame update
    void Start()
    {
        m_path = Application.streamingAssetsPath+"/XMLS";
        InitFiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowMainScene()
    {
        AniExchangeScene("切换至主界面");
    }

    public void ShowWelcomeScene()
    {
        txXMLStatus.text = "";
        AniExchangeScene("切换至欢迎界面");
    }
    void AniExchangeScene(string mode)
    {
        string aniName = "ExchangeScene";
        if (mode == "切换至主界面")
        {
            ani[aniName].time = 0;
            ani[aniName].speed = 0.3f;
        }
        else if (mode == "切换至欢迎界面")
        {
            ani[aniName].time = ani[aniName].clip.length;
            ani[aniName].speed = -0.3f;
        }
        ani.Play(aniName);
    }
    public void ProGramClose()
    {
        Application.Quit();
    }

    void InitFiles()
    {
        string[] fileALL = Directory.GetFiles(m_path);
        CheckType(fileALL, "xml");
        PutInToDropDown();
    }
    void CheckType(string[] filename,string type)
    {
        int len = filename.Length;
        for(int i=0;i<len;i++)
        {
            int j = filename[i].Length-1;
            if (j < 4)
                continue;
            if (filename[i][j] == 'l' && filename[i][j-1] == 'm' && filename[i][j-2] == 'x')
            {
                xmlList.Add(filename[i]);
                //print(filename[i]);
                string xmlName="";
                for(int k=j-4;k>=0;k--)
                {
                    if(filename[i][k]=='\\')
                    {
                        for(int t=k+1;t<j-3;t++)
                        {
                            xmlName+=(filename[i][t]);
                        }
                        break;
                    }
                }
                dropdownList.Add(xmlName);
            }
                
        }
        m_file_cnt = xmlList.Count;
    }
    void PutInToDropDown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(dropdownList);
    }

    public void ReadChosenXML()
    {
        chosenIndex = dropdown.value;
        xmlTool.setPath = xmlList[chosenIndex];
        xmlTool.TryRead();
        if(xmlTool.ReadSuccess)
        {
            txXMLStatus.text = "读取成功，正在读取中···";
            xmlTool.change = true;
            Invoke("ShowMainScene", 3f);

        }
        else
        {
            txXMLStatus.text = "读取失败，请确认文件是否存在";
        }
    }
}
