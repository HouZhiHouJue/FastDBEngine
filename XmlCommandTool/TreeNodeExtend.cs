using FastDBEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public static class TreeNodeExtend
{
    public static bool IsTagEmpty(this TreeNode treeNode)
    {
        return (treeNode.Tag == null);
    }

    public static bool HasTagValue(this TreeNode treeNode)
    {
        return (treeNode.Tag != null);
    }

    public static XmlCommand GetTagValue(this TreeNode treeNode)
    {
        if (!treeNode.HasTagValue())
        {
            return null;
        }
        return (XmlCommand) treeNode.Tag;
    }

    public static void SetTagValue(this TreeNode treeNode, XmlCommand xmlCommand)
    {
        treeNode.Tag = xmlCommand;
    }

    public static List<XmlCommand> GetTagList(this TreeNode treeNode)
    {
        if (treeNode.HasTagValue())
        {
            return null;
        }
        List<XmlCommand> list = new List<XmlCommand>(treeNode.Nodes.Count);
        foreach (TreeNode node in treeNode.Nodes)
        {
            list.Add((XmlCommand) node.Tag);
        }
        return list;
    }
}

