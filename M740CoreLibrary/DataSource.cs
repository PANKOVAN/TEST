using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace M740CoreLibrary
{
    public class DataSource
    {
        #region Session
        public ISession Session { get; set; }
        #endregion
        #region CurrentUser
        public ICurrentUser CurrentUser { get; set; }
        #endregion
        public DataSource(ISession session, ICurrentUser currentuser)
        {
            Session = session;
            CurrentUser = currentuser;
        }

        public XmlNode XRequest { get; set; }
        public virtual void Run(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            string requestname = XFunc.GetAttr(xrequest, "name", "").ToLower();
            switch (requestname)
            {
                case "definition":
                    RunDefinition(name, model, xrequest, xresponse);
                    break;
                case "expand":
                case "refresh":
                    RunRefresh(name, model, xrequest, xresponse);
                    RunRefreshMark(name, model, xrequest, xresponse);
                    break;
                case "save":
                    RunSave(name, model, xrequest, xresponse);
                    break;
                case "append":
                    RunAppend(name, model, xrequest, xresponse);
                    break;
                case "delete":
                    RunDelete(name, model, xrequest, xresponse);
                    break;
                case "change":
                    RunChange(name, model, xrequest, xresponse);
                    break;
                case "mark":
                    RunMark(name, model, xrequest, xresponse);
                    break;
                case "markclear":
                    RunMarkClear(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    RunRefreshMark(name, model, xrequest, xresponse);
                    break;
                case "markset":
                    RunMarkSet(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    break;
                case "join":
                    RunJoin(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    break;
                case "copy":
                    RunCopy(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    break;
                case "move":
                    RunMove(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    break;
                case "link":
                    RunLink(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    break;
                case "clone":
                    RunClone(name, model, xrequest, xresponse);
                    RunRefresh(name, model, xrequest, xresponse);
                    break;
                default:
                    RunRequest(name, requestname, model, xrequest, xresponse);
                    break;
            }
        }
        protected virtual void RunDefinition(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunDefinition"" для {name} не переопределен.");
        }
        protected virtual void RunRefresh(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunRefresh"" для {name} не переопределен.");
        }
        protected virtual void RunRefreshSelect(List<MObj> list, MEDMDefClass dc, string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
        }
        protected virtual void PutRefreshResult(List<MObj> list, MEDMDefClass dc, string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
        }
        protected virtual void PutRowResult(MObj obj, XmlNode xrow, MEDMDefClass dc, string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
        }
        protected virtual void RunSave(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunSave"" для {name} не переопределен.");
        }
        protected virtual void RunAppend(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunAppend"" для {name} не переопределен.");
        }
        protected virtual void RunDelete(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunDelete"" для {name} не переопределен.");
        }
        protected virtual void RunChange(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunChange"" для {name} не переопределен.");
        }
        protected virtual void RunJoin(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunJoin"" для {name} не переопределен.");
        }
        protected virtual void RunCopy(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunCopy"" для {name} не переопределен.");
        }
        protected virtual void RunMove(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunMove"" для {name} не переопределен.");
        }
        protected virtual void RunLink(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunLink"" для {name} не переопределен.");
        }
        protected virtual void RunClone(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new NotImplementedException($@"Медод ""RunClone"" для {name} не переопределен.");
        }
        protected virtual void RunRequest(string name, string requestname, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            throw new Exception($"Источник данных {this.GetType().Name} не поддерживает запрос {requestname}");
        }
        public static string MarkKey(XmlNode xrequest, string datasource = "", string mode = "")
        {
            if (mode == "") mode = M740.GetParam(xrequest, "markmode");
            if (datasource == "") datasource = M740.GetParam(xrequest, "markfield");
            if (datasource == "") datasource = M740.GetParam(xrequest, "rowset");
            if (datasource == "") M740.GetParam(xrequest, "datasource");
            if (datasource == "") datasource = XFunc.GetAttr(xrequest, "datasource", "");
            return $"mark.{datasource}.{mode}";
        }
        private static char[] _markdelim = { ';' };
        public static string MarkId(string idlist, string section, string preffix = "'", string suffix = "'", string delim = ",")
        {
            string result = "";
            if (!string.IsNullOrEmpty(idlist))
            {
                section = section.ToLower();
                foreach (string s in idlist.Split(_markdelim, StringSplitOptions.RemoveEmptyEntries))
                {
                    int i = s.IndexOf('.');

                    if (i >= 0)
                    {
                        if (section == "" || s.Substring(0, i).ToLower() == section)
                        {
                            if (result != "") result += delim;
                            result += $"{preffix}{s.Substring(i + 1)}{suffix}";
                        }
                    }
                    else
                    {
                        if (result != "") result += delim;
                        result += $"{preffix}{s}{suffix}";
                    }
                }
            }
            return result;
        }
        public static List<T> MarkId<T>(string idlist, string section)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrEmpty(idlist))
            {
                section = section.ToLower();
                string[] ss = idlist.Split(_markdelim, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ss.Length; i++)
                {
                    string s = ss[i];
                    int j = s.IndexOf('.');

                    if (j >= 0)
                    {
                        if (section == "" || s.Substring(0, j).ToLower() == section) s = s.Substring(j + 1);
                        else s = "";
                    }
                    if (s != "")
                    {
                        object o = null;
                        Type t = typeof(T);
                        if (t == typeof(Guid)) o = new Guid(s);
                        else if (t == typeof(string)) o = s;
                        else if (t == typeof(int)) o = int.Parse(s);
                        else if (t == typeof(long)) o = long.Parse(s);
                        else throw new Exception($"В MarkId тип {t} не определен.");
                        result.Add((T)o);
                    }
                }
            }
            return result;
        }
        public static string MarkNames(string idlist, Type objtype, string section, MEDMSql model)
        {
            string result = "";
            string idlist1 = MarkId(idlist, section);
            if (!string.IsNullOrEmpty(idlist1))
            {
                List<MObj> list = new List<MObj>();
                string sql = $"select * from [{MEDMDefModel.MainDef.GetTableNameByType(objtype)}] (nolock) where id!='{default(Guid)}' and id in ({idlist1})";
                model.Select(list, objtype, sql);
                foreach (MObj o in list)
                {
                    result += o.ToString() + "\r\n";
                }
            }
            return result;
        }
        protected void MarkSetCount(string idlist, XmlNode xresponse)
        {
            int count = 0;
            char[] d = { ';' };
            if (!string.IsNullOrEmpty(idlist)) count = idlist.Split(d, StringSplitOptions.RemoveEmptyEntries).Length;
            XFunc.SetAttr(xresponse, "markcount", count.ToString());
        }
        protected virtual void RunRefreshMark(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            string key = MarkKey(xrequest);
            string s = Session.GetString(key);
            if (s == null) s = "";
            if (s != "")
            {
                foreach (XmlNode xrow in xresponse.SelectNodes("descendant::row"))
                {
                    string id = XFunc.GetAttr(xrow, "id", "");
                    string rowtype = XFunc.GetAttr(xrow, "row.type", "");
                    if (rowtype != "") id = rowtype + "." + id;
                    XFunc.SetAttr(xrow, "row.mark", s.Contains(id) ? "1" : "0");
                }
            }
            else
            {
                foreach (XmlNode xrow in xresponse.SelectNodes("descendant::row"))
                {
                    XFunc.SetAttr(xrow, "row.mark", "0");
                }
            }
            MarkSetCount(s, xresponse);
        }
        protected virtual void RunMark(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            string rowtype = XFunc.GetAttr(xrequest, "row.type", "");
            string marktypes = M740.GetParam(xrequest, "marktypes");
            if (marktypes == "" || marktypes.Contains(rowtype))
            {
                string id = XFunc.GetAttr(xrequest, "id", "");

                string key = MarkKey(xrequest);
                string s = Session.GetString(key);
                if (s == null) s = "";

                if (rowtype != "") id = rowtype + "." + id;
                if (s.Contains(id))
                {
                    s = s.Replace(id + ";", "");
                    XFunc.Append(xresponse, "row", "row.mark", "0");
                }
                else
                {
                    s += id + ";";
                    XFunc.Append(xresponse, "row", "row.mark", "1");
                }

                Session.SetString(key, s);
                MarkSetCount(s, xresponse);
            }
        }
        protected virtual void RunMarkClear(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            string key = MarkKey(xrequest);
            Session.Remove(key);
            MarkSetCount("", xresponse);
        }
        protected virtual void RunMarkSet(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            string key = MarkKey(xrequest);
            string result = M740.GetParam(xrequest, "result");
            if (result == "OK")
            {
                string r = Session.GetString(key);
                if (string.IsNullOrEmpty(r)) Session.Remove("~" + key);
                else Session.SetString("~" + key, r);
            }
            else
            {
                string r = Session.GetString("~" + key);
                if (string.IsNullOrEmpty(r)) Session.Remove(key);
                else Session.SetString(key, r);
            }
        }
        public static string GetIdListFromString(string source, string type = "", string delim = ",", string formatstring = "")
        {
            char[] d = { '\n', '\r' };
            string result = "";
            if (!string.IsNullOrEmpty(source))
            {
                foreach (string s in source.Split(d, StringSplitOptions.RemoveEmptyEntries))
                {
                    string s1 = s.Split('=')[0].Trim();
                    if (!string.IsNullOrEmpty(s1))
                    {
                        string id = "";
                        string[] s2 = s1.Split('.');
                        if (s2.Length == 1)
                        {
                            id = s2[0];
                        }
                        else if (s2.Length > 1)
                        {
                            if (string.IsNullOrEmpty(type) || type.ToLower() == s2[0].ToLower())
                            {
                                id = s2[1];
                            }
                        }
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (!string.IsNullOrEmpty(result)) result += delim;
                            if (formatstring == "") result += id;
                            else result += string.Format(formatstring, id);
                        }
                    }
                }
            }
            return result;
        }
    }
}
