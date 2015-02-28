using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public static class TreeNodeCollectionExtend
{
    public static bool CompareIgnoreCase(this string str, string value)
    {
        return (string.Compare(str, value, StringComparison.OrdinalIgnoreCase) == 0);
    }

    public static TreeNode CompareText(this TreeNodeCollection treeNodeCollection, string value)
    {
        for (int i = 0; i < treeNodeCollection.Count; i++)
        {
            if (treeNodeCollection[i].Text.CompareIgnoreCase(value))
            {
                return treeNodeCollection[i];
            }
        }
        return null;
    }

    public static void RemoveNode(this TreeNodeCollection treeNodeCollection, string value)
    {
        TreeNode node = treeNodeCollection.CompareText(value);
        if (node != null)
        {
            treeNodeCollection.Remove(node);
        }
    }

    public static bool smethod_3(this string path, string path2)
    {
        string str = Path.Combine(path, "FastDBEngine").ToLower();
        string str2 = Path.Combine(path2, "FastDBEngine").ToLower();
        return (str == str2);
    }

    public static void SetSelectNode(this TreeView treeView)
    {
        if (treeView.SelectedNode != null)
        {
            TreeNode prevNode = treeView.SelectedNode.PrevNode;
            if (prevNode == null)
            {
                prevNode = treeView.SelectedNode.NextNode;
            }
            treeView.Nodes.Remove(treeView.SelectedNode);
            if (prevNode != null)
            {
                treeView.SelectedNode = prevNode;
            }
        }
    }
}

