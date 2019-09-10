using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
public class MultiTree
{
    string m_name;
    string m_text;
    List<MultiTree> m_children;
    MultiTree m_parent;
    public MultiTree parent
    {
        get
        {
            return m_parent;
        }
        set
        {
            m_parent = value;
        }
    }
    public List<MultiTree>child
    {
        get
        {
            return m_children;
        }
    }
    public string name
    {
        set
        {
            m_name = value;
        }
        get
        {
            return m_name;
        }
    }
    public string text
    {
        set
        {
            m_text = value;
        }
        get
        {
            return m_text;
        }
    }
    public MultiTree()
    {
        m_children = new List<MultiTree>();
    }
    public void BuildTree(XmlDocument xmlDoc)
    {
        BuildTree(xmlDoc.ChildNodes, this);
    }
    void BuildTree(XmlNodeList xmlRoot, MultiTree treeRoot)
    {
        for(int i=0;i<xmlRoot.Count;i++)
        {
            XmlNode xmlNode = xmlRoot[i];
            MultiTree treeNode = new MultiTree();
            treeNode.parent = treeRoot;
            if (xmlNode.NodeType==XmlNodeType.Element)
            {
                treeNode.name = xmlNode.Name;
                treeRoot.child.Add(treeNode);
            }
            else if(xmlNode.NodeType==XmlNodeType.Text)
            {
                treeNode.text = xmlNode.InnerText;
                treeRoot.child.Add(treeNode);
            }
            
            if (xmlNode.HasChildNodes)
            {
                BuildTree(xmlNode.ChildNodes, treeNode);
            }
        }
    }
}

