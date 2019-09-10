using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XMLTool
{
    //单例模式
    private static XMLTool m_Instance = new XMLTool();
    public static XMLTool Instance
    {
        get
        {
            return m_Instance;
        }
    }
    public string setPath
    {
        set
        {
            m_path = value;
        }
    }
    private string m_path;
    private bool m_xml_exist;
    private XmlDocument m_xmlDocument;
    private string m_name;
    private bool m_flag_change;
    public bool change
    {
        get
        {
            return m_flag_change;
        }
        set
        {
            m_flag_change = value;
        }
    }
    public XMLTool()
    {
        m_xml_exist = false;
        m_xmlDocument = new XmlDocument();
    }
    
    public void TryRead()
    {
        try
        {
            m_xmlDocument = new XmlDocument();
            m_name = "";
            m_xmlDocument.Load(m_path);
            int len = m_path.Length;
            int i = len - 1;
            while(i>=0&&m_path[i]!='/')
            {
                i--;
            }
            i++;
            for(;i<len;i++)
            {
                m_name += m_path[i];
            }
            m_xml_exist = true;
        }
        catch
        {
            m_xml_exist = false;
        }
    }

    public bool ReadSuccess
    {
        get
        {
            return m_xml_exist;
        }
    }
    public string xmlName
    {
        get
        {
            return m_name;
        }
    }
    public XmlDocument xmlDoc
    {
        get
        {
            return m_xmlDocument;
        }
    }
}
