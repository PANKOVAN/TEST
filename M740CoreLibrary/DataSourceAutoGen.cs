using System;
using System.Collections.Generic;
using System.Xml;
using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Http;

namespace M740CoreLibrary
{
    public class DataSourceAutoGen : DataSource
    {
        public DataSourceAutoGen(ISession session, ICurrentUser currentuser) : base(session, currentuser)
        {

        }
        protected override void RunDefinition(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            Definition(name, model, xrequest, xresponse, "", false, false, CurrentUser.UserIsReadOnly());
        }
        private int GetLen(MEDMDefProperty dp)
        {
            return dp.Length;
            /*
            if (dp.Length > 0) return dp.Length;
            switch (dp.GetDataTypeFor740())
            {
                case "num":
                    return 10;
                case "date":
                    return 10;
            }
            return dp.Header.Length + 1;
            */
        }
        private char[] itemd = { ';', '\r', '\n' };
        protected XmlNode Definition(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse, string sectiontype = "", bool addExpand = false, bool addToString = false, bool isReadOnly = false)
        {
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (sectiontype != "")
            {
                dc = MEDMDefModel.MainDef.Find(sectiontype);
            }

            bool isdefultsection = false;
            if (name == "empty")
            {
                XmlNode xrowset = XFunc.AppendWithFind(xresponse, "rowset", "rowset", "empty", "datasource", "empty");
                XmlNode xsection = XFunc.Append(xrowset, "section", "row.type", sectiontype);
                XmlNode xrequests = XFunc.Append(xsection, "requests");
                XFunc.Append(xrequests, "request", "name", "refresh");
                return xsection;
            }
            else if (dc != null)
            {
                isReadOnly = isReadOnly || dc.IsReadOnly;
                if (sectiontype.ToLower() == "default")
                {
                    isdefultsection = true;
                    sectiontype = "";
                }
                XmlNode xrowset = XFunc.AppendWithFind(xresponse, "rowset", "rowset", name.ToLower(), "datasource", name.ToLower(), "readonly", isReadOnly ? "1" : "0");
                XmlNode xsection = XFunc.Append(xrowset, "section", "row.type", sectiontype);

                XmlNode xrequests = XFunc.Append(xsection, "requests");
                XFunc.Append(xrequests, "request", "name", "refresh");
                if (addExpand)
                {
                    XFunc.Append(xrequests, "request", "name", "expand");
                }
                if (isdefultsection) return xsection;
                if (dc != null && dc.BaseClass == "MEDMObj" && !isReadOnly)
                {
                    XFunc.Append(xrequests, "request", "name", "save");
                    XFunc.Append(xrequests, "request", "name", "append");
                    XFunc.Append(xrequests, "request", "name", "delete");
                }
                if (dc.IsMark)
                {
                    XFunc.Append(xrequests, "request", "name", "mark");
                    XFunc.Append(xrequests, "request", "name", "markclear");
                    XmlNode xparams = XFunc.AppendWithFind(xrequests, "params");
                    XFunc.Append(xparams, "param", "name", "markmode", "value", name.ToLower());
                    XFunc.Append(xparams, "param", "name", "markfield", "value", name.ToLower() + "list");
                }
                if (dc.IsJoin && !isReadOnly)
                {
                    XmlNode xr = XFunc.Append(xrequests, "request", "name", "join");
                    if (dc.CaptionJoin == "*") XFunc.SetAttr(xr, "caption", "Объединить отмеченные строки...");
                    else if (dc.CaptionJoin != "") XFunc.SetAttr(xr, "caption", dc.CaptionJoin);
                    if (dc.ConfirmJoin == "*") XFunc.SetAttr(xr, "confirm", "Отмеченные строки будут объединены с текущей строкой...");
                    else if (dc.ConfirmJoin != "") XFunc.SetAttr(xr, "confirm", dc.ConfirmJoin);
                }
                if (dc.IsCopy && !isReadOnly)
                {
                    XmlNode xr = XFunc.Append(xrequests, "request", "name", "copy");
                    if (dc.CaptionCopy == "*") XFunc.SetAttr(xr, "caption", "Копировать отмеченные строки...");
                    else if (dc.CaptionCopy != "") XFunc.SetAttr(xr, "caption", dc.CaptionCopy);
                    if (dc.ConfirmCopy == "*") XFunc.SetAttr(xr, "confirm", "Отмеченные строки будут скопированы...");
                    else if (dc.ConfirmCopy != "") XFunc.SetAttr(xr, "confirm", dc.ConfirmCopy);
                }
                if (dc.IsMove && !isReadOnly)
                {
                    XmlNode xr = XFunc.Append(xrequests, "request", "name", "move");
                    if (dc.CaptionMove == "*") XFunc.SetAttr(xr, "caption", "Перенести отмеченные строки...");
                    else if (dc.CaptionMove != "") XFunc.SetAttr(xr, "caption", dc.CaptionMove);
                    if (dc.ConfirmMove == "*") XFunc.SetAttr(xr, "confirm", "Отмеченные строки будут перенесены...");
                    else if (dc.ConfirmMove != "") XFunc.SetAttr(xr, "confirm", dc.ConfirmMove);
                }
                if (dc.IsLink && !isReadOnly)
                {
                    XmlNode xr = XFunc.Append(xrequests, "request", "name", "link");
                    if (dc.CaptionLink == "*") XFunc.SetAttr(xr, "caption", "Присоединить отмеченные строки...");
                    else if (dc.CaptionLink != "") XFunc.SetAttr(xr, "caption", dc.CaptionLink);
                    if (dc.ConfirmLink == "*") XFunc.SetAttr(xr, "confirm", "Отмеченные строки будут присоединены...");
                    else if (dc.ConfirmLink != "") XFunc.SetAttr(xr, "confirm", dc.ConfirmLink);
                }
                if (dc.IsClone && !isReadOnly)
                {
                    XmlNode xr = XFunc.Append(xrequests, "request", "name", "clone");
                    if (dc.CaptionClone == "*") XFunc.SetAttr(xr, "caption", "Клонировать отмеченные строки...");
                    else if (dc.CaptionClone != "") XFunc.SetAttr(xr, "caption", dc.CaptionClone);
                    if (dc.ConfirmClone == "*") XFunc.SetAttr(xr, "confirm", "Отмеченные строки будут клонированы...");
                    else if (dc.ConfirmClone != "") XFunc.SetAttr(xr, "confirm", dc.ConfirmClone);
                }
                XmlNode xfields = XFunc.Append(xsection, "fields");
                foreach (MEDMDefProperty dp in dc.Properties)
                {
                    if (dp.IsVisible)
                    {
                        if (dp.IsInterval)
                        {
                            XFunc.Append(xfields, "field", "name", (dp.Name + ".Min").ToLower(), "type", dp.GetDataTypeFor740(), "caption", dp.Header + " от", "len", GetLen(dp));
                            XFunc.Append(xfields, "field", "name", (dp.Name + ".Max").ToLower(), "type", dp.GetDataTypeFor740(), "caption", "до", "len", GetLen(dp));
                        }
                        else
                        {
                            string t = dp.GetDataTypeFor740();
                            string list = "";
                            if (!string.IsNullOrEmpty(dp.Items))
                            {
                                foreach (string item in dp.Items.Split(itemd, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string item1 = item.Replace(";", ",").Trim();
                                    if (!string.IsNullOrEmpty(item1))
                                    {
                                        if (!string.IsNullOrEmpty(list)) list += ";";
                                        list += item1;
                                    }
                                }
                            }
                            if (list != "") t = "list";
                            XmlNode xfield = XFunc.Append(xfields, "field", "name", dp.Name.ToLower(), "type", t, "caption", dp.Header, "len", GetLen(dp), "list", list);
                            if (t == "radio")
                            {
                                if (dp.DataType == "int" || dp.DataType == "long" || dp.DataType == "short" || dp.DataType == "byte")
                                {
                                    XFunc.SetAttr(xfield, "basetype", "num");
                                    XFunc.SetAttr(xfield, "default", MFunc.StringToInt(dp.DefValue, 1).ToString());
                                }
                                else
                                {

                                }
                            }
                            if (!MEDMObj.IsEmptyId(dp.RefClassId))
                            {
                                XFunc.Append(xfield, "ref", "datasource", dp.RefClass.Name.ToLower());
                                XFunc.SetAttr(xfield, "type", "ref");
                                XFunc.SetAttr(xfield, "visible", "0");
                                XmlNode xreffield = XFunc.Append(xfields, "field", "name", (dp.GetRefName() + "_tostring_").ToLower(), "type", "string", "refid", dp.Name.ToLower(), "refname", "_tostring_", "caption", dp.Header, "len", GetLen(dp));
                            }
                        }
                    }
                }
                if (addToString)
                {
                    XFunc.SetAttr(xfields, "name", "_tostring_");
                    XFunc.Append(xfields, "field", "name", "_tostring_", "type", "string", "caption", "", "len", "255");
                }
                return xsection;
            }
            return null;
        }
        protected XmlNode DefinitionForMarkList(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse, string sectiontype = "", bool addExpand = false)
        {
            bool isdefultsection = false;
            if (sectiontype.ToLower() == "default")
            {
                isdefultsection = true;
                sectiontype = "";
            }
            XmlNode xrowset = XFunc.AppendWithFind(xresponse, "rowset", "rowset", name.ToLower(), "datasource", name.ToLower());
            XmlNode xsection = XFunc.Append(xrowset, "section", "row.type", sectiontype);

            XmlNode xrequests = XFunc.Append(xsection, "requests");
            XFunc.Append(xrequests, "request", "name", "refresh");
            XFunc.Append(xrequests, "request", "name", "mark");
            if (addExpand)
            {
                XFunc.Append(xrequests, "request", "name", "expand");
            }
            if (isdefultsection) return xsection;
            XmlNode xfields = XFunc.Append(xsection, "fields");
            {
                XFunc.SetAttr(xfields, "name", "_tostring_");
                XFunc.Append(xfields, "field", "name", "_tostring_", "type", "string", "caption", "", "len", "255");
            }
            return xsection;
        }
        protected override void RunRefresh(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            Refresh(name, model, xrequest, xresponse);
        }
        protected override void PutRefreshResult(List<MObj> list, MEDMDefClass dc, string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            foreach (MObj obj in list)
            {
                XmlNode xrow = XFunc.Append(xresponse, "row");
                if (dc == null) dc = MEDMDefModel.MainDef.Find(obj.GetType());
                if (dc != null)
                {
                    foreach (MEDMDefProperty dp in dc.Properties)
                    {
                        if (dp.IsInterval)
                        {
                            XFunc.SetAttr(xrow, dp.Name.ToLower() + ".min", obj.GetValueAsString(dp.Name + ".Min"));
                            XFunc.SetAttr(xrow, dp.Name.ToLower() + ".max", obj.GetValueAsString(dp.Name + ".Max"));
                        }
                        else
                        {
                            XFunc.SetAttr(xrow, dp.Name.ToLower(), obj.GetValueAsString(dp.Name));
                        }
                        if (!MEDMObj.IsEmptyId(dp.RefClassId))
                        {
                            string n = dp.GetRefName().ToLower();
                            XFunc.SetAttr(xrow, n + "_tostring_", obj.GetValueAsString(n));
                        }
                    }
                }
                else if (obj is MEDMObj)
                {
                    XFunc.SetAttr(xrow, "id", (obj as MEDMObj).GetId().ToString());
                }
                XFunc.SetAttr(xrow, "_tostring_", obj.ToString());
                PutRowResult(obj, xrow, dc, name, model, xrequest, xresponse);
            }
        }
        protected void Refresh(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            if (name == "empty")
            {
                XmlNode xrow = XFunc.Append(xresponse, "row", "id", "0");
                return;
            }
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (dc != null)
            {
                if (dc.BaseClass == "MEDMObj")
                {
                    Type t = dc.GetClassType();
                    if (t != null)
                    {
                        List<MObj> list = new List<MObj>();
                        if (dc.ClassType == "edm")
                        {
                            RunRefreshSelect(list, dc, name, model, xrequest, xresponse);
                        }
                        else if (dc.ClassType == "session")
                        {
                            MObj o = model.CreateObject(t, Session.Id.ToString());
                            list.Add(o);
                            /* Если объекты хранятся в кеше то в сессии их запоминать не к чему
                            string id = XFunc.GetText(xrequest.SelectSingleNode("param[@name='id']"), Session.Id);
                            model.SelectFromXML(list, t, Session.GetString(name), id);
                            if (list.Count == 0)
                            {
                                MObj o = model.CreateObject(t, Session.Id.ToString());
                                list.Add(o);
                                model.Save(Session);
                            }
                            */
                        }
                        else
                        {
                            throw new Exception($"Для автоматической генерации Refresh тип источника данных {dc.ClassType} не определен или задан неправильно.");
                        }
                        PutRefreshResult(list, dc, name, model, xrequest, xresponse);
                    }
                }
                else
                {
                    throw new Exception($"Для автоматической генерации Refresh класс источника данных {name} должен быть порожден от MEDMObj");
                }
            }
            else
            {
                List<MObj> list = new List<MObj>();
                RunRefreshSelect(list, dc, name, model, xrequest, xresponse);
                PutRefreshResult(list, dc, name, model, xrequest, xresponse);
            }
        }
        class FilterParam
        {
            public string Name = "";
            public string Mode = "";
            public object Value = null;
            public FilterParam(string name, string mode, object value)
            {
                Name = name;
                Mode = mode;
                Value = value;
            }
        }
        class FilterParamList : List<FilterParam>
        {
            public int Top
            {
                get;
                set;
            }
            public FilterParamList(MEDMDefClass dc, XmlNode xrequest)
            {
                foreach (XmlNode xparam in xrequest.SelectNodes("descendant::param"))
                {
                    string[] nn = XFunc.GetAttr(xparam, "name", "").Split('.');
                    string n = nn[0];
                    string m = "";
                    if (nn.Length > 1)
                    {
                        if (nn[0].ToLower() == "filter")
                        {
                            n = nn[nn.Length - 1];
                            if (nn.Length == 3) m = nn[1].ToLower();
                        }
                        if (nn[0].ToLower() == "paginator")
                        {
                            n = nn[nn.Length - 1];
                            if (n.ToLower() == "top")
                            {
                                Top = MFunc.StringToInt(XFunc.GetText(xparam, ""), 0);
                                continue;
                            }
                        }
                    }
                    MEDMDefProperty dp = dc.Properties[n];
                    if (dp != null)
                    {
                        object v = dp.ConvertToPropertyType(XFunc.GetText(xparam, ""));
                        Add(new FilterParam(n, m, v));
                    }
                }
            }
        }
        protected override void RunRefreshSelect(List<MObj> list, MEDMDefClass dc, string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            FilterParamList l = new FilterParamList(dc, xrequest);
            object[] parms = new object[l.Count * 2 + 2];
            string t= l.Top > 0 ? "top " + l.Top.ToString():"";
            string sql = $"select {t} * from [{dc.Name}] (nolock) where id!=@p0";
            parms[0] = "p0";
            parms[1] = dc.GetIdPropery().GetDefaultValue();
            for (int i = 0; i < l.Count; i++)
            {
                switch (l[i].Mode)
                {
                    case "":
                    case "eq":
                        sql += $" and ( [{l[i].Name}]=@p{i + 1})";
                        break;
                    case "ne":
                        sql += $" and ( [{l[i].Name}]!=@p{i + 1})";
                        break;
                    case "eqornone":
                        sql += $" and ( [{l[i].Name}]=@p{i + 1} or  [{l[i].Name}]=@p0)";
                        break;
                }
                parms[(i + 1) * 2] = $"p{i + 1}";
                parms[(i + 1) * 2 + 1] = l[i].Value;
            }
            {
                MEDMDefProperty dp = dc.Properties.Find(p => p.Name.ToLower() == "name");
                if (dp == null) dp = dc.Properties.Find(p => p.Name.ToLower().EndsWith("name"));
                if (dp == null) dp = dc.Properties.Find(p => p.Name.ToLower().Contains("name"));
                if (dp != null) sql += $" order by [{dp.Name}]";
            }
            model.Select(list, dc.GetClassType(), sql, parms);
        }
        protected XmlNode RefreshRow(string name, object id, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            XmlNode xrow = null;
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (dc != null)
            {
                if (dc.BaseClass == "MEDMObj")
                {
                    Type t = dc.GetClassType();
                    if (t != null)
                    {
                        List<MObj> list = new List<MObj>();
                        if (dc.ClassType == "edm")
                        {
                            model.Select(list, t, $"select * from [{dc.Name}] (nolock) where id!=@p1 and id=@p2", "p1", dc.GetIdPropery().GetDefaultValue(), "p2", id);
                        }
                        else if (dc.ClassType == "session")
                        {
                            MObj o = model.CreateObject(t, Session.Id.ToString());
                            list.Add(o);
                            /* Если объекты хранятся в кеше то в сессии их запоминать не к чему
                            model.SelectFromXML(list, t, Session.GetString(name), id);
                            */
                        }
                        else
                        {
                            throw new Exception($"Для автоматической генерации RefreshRow тип источника данных {dc.ClassType} не определен или задан неправильно.");
                        }
                        PutRefreshResult(list, dc, name, model, xrequest, xresponse);
                    }
                }
                else
                {
                    throw new Exception($"Для автоматической генерации RefreshRow класс источника данных {name} должен быть порожден от MEDMObj");
                }
            }
            return xrow;
        }
        protected override void RunSave(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (dc != null)
            {
                if (dc.BaseClass == "MEDMObj")
                {
                    Type t = dc.GetClassType();
                    if (t != null)
                    {
                        string id = XFunc.GetAttr(xrequest, "id", "");
                        if (id == "") throw new Exception("В параметрах запроса отсутствует id");
                        MEDMObj obj = model.MainDic.GetObj(t, id);
                        if (obj == null) throw new Exception($"Не удалось создать объект типа {t}. Сохранить изменения невозможно.");
                        foreach (XmlNode xparam in xrequest.ChildNodes)
                        {
                            if (xparam.Name == "param")
                            {
                                obj.SetStringValue(XFunc.GetAttr(xparam, "name", ""), xparam.InnerText);
                            }
                        }
                        model.Save(Session);
                        RefreshRow(name, id, model, xrequest, xresponse);
                    }
                }
                else
                {
                    throw new Exception($"Для автоматической генерации Save класс источника данных {name} должен быть порожден от MEDMObj");
                }

            }
        }
        protected override void RunAppend(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (dc != null)
            {
                if (dc.BaseClass == "MEDMObj")
                {
                    Type t = dc.GetClassType();
                    if (t != null)
                    {
                        FilterParamList l = new FilterParamList(dc, xrequest);
                        MEDMObj obj = model.CreateObject(t, null);
                        model.Save(Session);
                        foreach (FilterParam fp in l)
                        {
                            obj.SetValue(fp.Name, fp.Value);
                        }
                        model.Save(Session);
                        if (obj != null)
                        {
                            XmlNode xrow = RefreshRow(name, obj.GetId(), model, xrequest, xresponse);
                            if (xrow != null)
                            {
                                XFunc.SetAttr(xrow, "row.destmode", "after");
                                XFunc.SetAttr(xrow, "row.destid", XFunc.GetAttr(xrequest, "id", ""));
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception($"Для автоматической генерации Append класс источника данных {name} должен быть порожден от MEDMObj");
                }

            }
        }
        protected override void RunDelete(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (dc != null)
            {
                if (dc.BaseClass == "MEDMObj")
                {
                    Type t = dc.GetClassType();
                    if (t != null)
                    {
                        string id = XFunc.GetAttr(xrequest, "id", "");
                        if (id == "") throw new Exception("В параметрах запроса отсутствует id");
                        MEDMObj obj = model.MainDic.GetObj(t, id);
                        if (obj == null) throw new Exception($"Не удалось создать объект типа {t}. Удаление невозможно.");
                        model.DeleteObject(obj);
                        model.Save(Session);
                    }
                }
                else
                {
                    throw new Exception($"Для автоматической генерации Delete класс источника данных {name} должен быть порожден от MEDMObj");
                }

            }
        }
        protected override void RunJoin(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            MEDMDefClass dc = MEDMDefModel.MainDef.Find(name);
            if (dc == null) throw new Exception($"Нет описателя класса соответстующго имени {name}");
            MEDMDefProperty dp = dc.GetIdPropery();
            List<Guid> l = MarkId<Guid>(Session.GetString(MarkKey(xrequest)), name.ToLower());
            if (l.Count > 0)
            {
                object id = M740.GetParam(dp.GetPropertyType(), xrequest, "id", "");
                if (id != dp.GetDefaultValue())
                {
                    string sql = "";
                    foreach (MEDMDefClass dc1 in MEDMDefModel.MainDef.AllClasses)
                    {
                        foreach (MEDMDefProperty dp1 in dc1.Properties)
                        {
                            if (dp1.RefClass == dc)
                            {
                                sql += $"update [{dc1.Name}] set [{dp1.Name}]=@p0 where CountryPresentingDemandId=@p1\r\n";
                            }
                        }
                    }
                    sql += $"delete [{name}] where Id=@p1\r\n";

                    foreach (object joinid in l)
                    {
                        if (joinid != dp.GetDefaultValue() && joinid != id)
                        {
                            model.Exec(sql, "p0", id, "p1", joinid);
                        }
                    }
                    RunMarkClear(name, model, xrequest, xresponse);
                    XFunc.SetAttr(xresponse, "exec", "refresh");
                }
            }
            else
            {
                throw new Exception($"Нет омеченных элементов");
            }
        }
    }
}
