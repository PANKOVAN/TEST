using MEDMCoreLibrary;
using MFuncCoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace M740CoreLibrary
{
    public class M740
    {
        public static void ParmsToObj(XmlNode xparms, MObj obj)
        {
            foreach (XmlNode xparm in xparms.SelectNodes("descendant::param"))
            {
                string name = XFunc.GetAttr(xparm, "name", "");
                string value = XFunc.GetText(xparm, "");
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    obj.SetValue(name, value);
                }
            }
        }
        public static string GetParam(XmlNode xnode, string paramname, string defvalue="")
        {
            string r = defvalue;            
            XmlNode xparam = xnode.SelectSingleNode($"descendant::param[@name='{paramname}']");
            if (xparam==null) xparam = xnode.SelectSingleNode($"descendant::param[contains(@name,'.{paramname}')]");
            if (xparam != null) r = XFunc.GetText(xparam, defvalue);
            else r = XFunc.GetAttr(xnode, paramname, defvalue);
            return r;
        }
        public static T GetParam<T>(XmlNode xnode, string paramname, T defvalue = default(T))
        {
            string s = GetParam(xnode, paramname, "");
            if (s!="")
            {
                Type t = typeof(T);
                object o = null;
                if (t == typeof(Guid)) o = new Guid(s);
                else if (t == typeof(string)) o = s;
                else if (t == typeof(int)) o = int.Parse(s);
                else if (t == typeof(long)) o = long.Parse(s);
                else throw new Exception($"В GetParam тип {t} не определен.");
                return (T)o;
            }
            return defvalue;
        }
        public static object GetParam(Type t, XmlNode xnode, string paramname, string defvalue = "")
        {
            string s = GetParam(xnode, paramname, defvalue);
            object o = null;
            if (s != "")
            {
                if (t == typeof(Guid)) o = new Guid(s);
                else if (t == typeof(string)) o = s;
                else if (t == typeof(int)) o = int.Parse(s);
                else if (t == typeof(long)) o = long.Parse(s);
                else throw new Exception($"В GetParam тип {t} не определен.");
            }
            else
            {
                if (t == typeof(Guid)) o = default(Guid);
                else if (t == typeof(string)) o = "";
                else if (t == typeof(int)) o = default(int);
                else if (t == typeof(long)) o = default(long);
                else throw new Exception($"В GetParam тип {t} не определен.");
            }
            return o;
        }
    }
    public class ParamValues : List<object>
    {
        public string AddParam(object value)
        {
            this.Add(value);
            return $"@p{this.Count - 1}";
        }
        public object[] GetParamList()
        {
            object[] list = new object[this.Count * 2];
            for (int i = 0; i < this.Count; i++)
            {
                list[i * 2] = $"p{i}";
                list[i * 2 + 1] = this[i];
            }
            return list;
        }
    }

}
