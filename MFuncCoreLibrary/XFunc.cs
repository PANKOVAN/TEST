using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;


namespace MFuncCoreLibrary
{
    public class XFunc
    {
        #region Документ
        public static XmlDocument Load(string filename)
        {
            XmlDocument xdoc = new XmlDocument();
            FileStream xstream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            xdoc.Load(xstream);
            return xdoc;
        }

        #endregion
        #region Добавить узел

        public static XmlNode Append(XmlNode owner, string name, params object[] parms)
        {
            XmlNode newNode = owner.OwnerDocument.CreateElement(name, owner.NamespaceURI);
            owner.AppendChild(newNode);
            return SetAttrs(newNode, parms);
        }
        public static XmlNode AppendWithFind(XmlNode owner, string name, params object[] parms)
        {
            XmlNode newNode = null;
            foreach(XmlNode xnode in owner.ChildNodes)
            {
                if (xnode.Name.ToLower()==name.ToLower())
                {
                    newNode = xnode;
                    break;
                }
            }
            if (newNode == null)
            {
                newNode = owner.OwnerDocument.CreateElement(name, owner.NamespaceURI);
                owner.AppendChild(newNode);
            }
            return SetAttrs(newNode, parms);
        }
        public static XmlNode Append(XmlDocument owner, string name, params object[] parms)
        {
            XmlElement newNode = owner.CreateElement(name, owner.NamespaceURI);
            owner.AppendChild(newNode);
            return SetAttrs(newNode, parms);
        }
        public static XmlNode SetAttrs(XmlNode node, params object[] parms)
        {
            for (int i=0; i<parms.Length-1; i+=2)
            {
                if (parms[i]!=null && parms[i+1]!=null)
                {
                    XFunc.SetAttr(node, parms[i].ToString(), parms[i + 1].ToString());
                }
            }
            return node;
        }
        #endregion
        #region Прочитать и установить атрибут в узле
        public static string GetAttr(XmlNode node, string name, string def)
        {
            string s = def;
            name = name.ToLower();
            if (node != null)
            {
                if (node.Attributes != null)
                {
                    foreach (XmlNode atr in node.Attributes)
                    {
                        if (atr.Name.ToLower() == name)
                        {
                            if (atr.Value != "") s = atr.Value;
                            return s;
                        }
                    }
                }
                foreach(XmlNode child in node.ChildNodes)
                {
                    if (child.Name.ToLower()==name)
                    {
                        return XFunc.GetText(child, s);
                    }
                 }
            }
            return s;
        }
        public static Guid GetAttr(XmlNode node, string name, Guid def)
        {
            Guid guid = def;
            string s = XFunc.GetAttr(node, name, "");
            if (s != "")
            {
                guid = new Guid(s);
            }
            return guid;
        }
        public static int GetAttr(XmlNode node, string name, int def)
        {
            int i = def;
            string s = XFunc.GetAttr(node, name, "");
            if (s != "")
            {
                try
                {
                    i = Convert.ToInt32(s);
                }
                catch { }
            }
            return i;
        }
        public static long GetAttr(XmlNode node, string name, long def)
        {
            long i = def;
            string s = XFunc.GetAttr(node, name, "");
            if (s != "")
            {
                try
                {
                    i = Convert.ToInt64(s);
                }
                catch { }
            }
            return i;
        }
        public static bool GetAttr(XmlNode node, string name, bool def)
        {
            bool r = def;
            string s = XFunc.GetAttr(node, name, "");
            if (s != "")
            {
                s = s.ToLower();
                r = (s == "1") || (s == "true");
            }
            return r;
        }
        public static DateTime GetAttr(XmlNode node, string name, DateTime def)
        {
            DateTime i = def;
            string s = XFunc.GetAttr(node, name, "");
            if (s != "")
            {
                try
                {
                    i = Convert.ToDateTime(s);
                }
                catch { }
            }
            return i;
        }
        public static void SetAttr(XmlNode node, string name, string val, string def)
        {
            if (node != null && node is XmlElement)
            {
                if (val != def)
                {
                    ((XmlElement)node).SetAttribute(name, val);
                }
                else
                {
                    ((XmlElement)node).RemoveAttribute(name);
                }
            }
        }
        public static void SetAttr(XmlNode node, string name, string val)
        {
            if (val == null) val = "";
            if (node != null && node is XmlElement /* && val != ""*/)
            {
                ((XmlElement)node).SetAttribute(name, val);
            }
        }
        #endregion
        #region Прочитать и HttpContext context
        /*
        public static string GetAttr(HttpContext context, string name, string def)
        {
            string s = def;
            name = name.ToLower();
            //context.
            return s;
        }
        */
        #endregion
        #region Прочитать и установить текст в узле
        public static XmlNode GetText(XmlNode node)
        {
            XmlNode r = null;
            if (node != null)
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.NodeType == XmlNodeType.Text)
                    {
                        r = n;
                        break;
                    }
                }
            }
            return r;
        }
        public static string GetText(XmlNode node, string def)
        {
            string r = def;
            XmlNode n = XFunc.GetText(node);
            if (n != null) r = n.Value;
            return r;
        }
        public static string GetChildText(XmlNode node, string child, string def)
        {
            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.NodeType == XmlNodeType.Element && n.Name == child)
                {
                    return XFunc.GetText(n, def);
                }
            }
            return def;
        }
        public static void SetText(XmlNode node, string val)
        {
            XmlNode n = XFunc.GetText(node);
            if (val == "")
            {
                if (n != null)
                {
                    node.RemoveChild(n);
                }
            }
            else
            {
                if (n == null)
                {
                    n = node.OwnerDocument.CreateTextNode(val);
                    node.AppendChild(n);
                }
                else
                {
                    n.Value = val;
                }

            }
        }
        #endregion
    }
}
