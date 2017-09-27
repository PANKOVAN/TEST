using MFuncCoreLibrary;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Claims;


namespace MEDMCoreLibrary
{
    public class MEDMSql : MEDM
    {
        #region ConnectionPool
        private static MEDMDataConnectionPool _CommonPool = null;
        public static MEDMDataConnectionPool ConnectionPool
        {
            get
            {
                if (_CommonPool == null) _CommonPool = new MEDMDataConnectionPool();
                return _CommonPool;
            }
        }
        #endregion
        #region MUpdateAdapterList
        private MUpdateAdapterList _Updates = null;
        public MUpdateAdapterList Updates
        {
            get
            {
                if (_Updates == null) _Updates = new MUpdateAdapterList();
                return _Updates;
            }
        }
        #endregion
        #region Запросы
        private MParamValues _ParamValues = null;
        public MParamValues ParamValues
        {
            get
            {
                if (_ParamValues == null) _ParamValues = new MParamValues();
                return _ParamValues;
            }
        }
        public string AddParam(object value)
        {
            return ParamValues.AddParam(value);
        }
        public object[] GetParamList(object[] parms)
        {
            if (parms.Length == 0) parms = ParamValues.GetParamList();
            ParamValues.Clear();
            return parms;
        }


        private object PrepareParam(object param)
        {
            if (param is MEDMObj)
            {
                param = (param as MEDMObj).GetId();
            }
            return param;
        }

        public void SetObjValues(MObj obj, SqlDataReader rdr)
        {
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                string name = rdr.GetName(i).TrimEnd();
                object value = rdr.GetValue(i);
                if (value is DBNull) value = null;
                LockUpdates++;
                obj.SetValue(name, value);
                LockUpdates--;
            }
        }
        public void SetObjValues(MObj obj, XmlNode xrow)
        {
            if (xrow != null)
            {
                foreach (XmlNode xnode in xrow.ChildNodes)
                {
                    LockUpdates++;
                    obj.SetStringValue(xnode.Name, xnode.InnerText, false);
                    LockUpdates--;
                }
            }
        }
        public O SelectFirst<O>(string sql, params object[] parms) where O : MObj
        {
            IList list = Select<O>(sql, parms);
            if (list.Count > 0) return list[0] as O;
            else return null;
        }
        public IList Select<O>(string sql, params object[] parms) where O : MObj
        {
            List<O> list = new List<O>();
            return Select(list, typeof(O), sql, parms);
        }
        public IList Select<O>(IList list, string sql, params object[] parms) where O : MObj
        {
            return Select(list, typeof(O), sql, parms);
        }
        public IList Select(IList list, Type t, string sql, params object[] parms)
        {
            SqlConnection con = ConnectionPool.GetConnection();
            try
            {
                parms = GetParamList(parms);
                int allCount = -1;
                IPaginationList plist = null;
                if (list is IPaginationList) plist = (IPaginationList)(list);

                DateTime begtime = DateTime.Now;
                SqlCommand com = new SqlCommand(sql, con);
                for (int i = 0; i < parms.Length - 1; i += 2)
                {
                    if (parms[i] != null) com.Parameters.Add(new SqlParameter(parms[i].ToString(), PrepareParam(parms[i + 1])));
                }
                // Для PaginationList select выполняем в 2 этапа сначала считаем кол-во а потом сам select c добавленным top
                if (plist != null)
                {
                    if (plist.Top != "")
                    {
                        string sql1 = com.CommandText;
                        int i = sql1.IndexOf('*');
                        int j = sql1.ToLower().IndexOf("order by");
                        if (i > 0 && (j < 0 || i < j))
                        {
                            com.CommandText = sql1.Remove(j).Remove(i, 1).Insert(i, "count(*)");
                            allCount = Convert.ToInt32(com.ExecuteScalar());
                            com.CommandText = sql1.Insert(i, plist.Top + " ");
                        }
                    }

                }
                bool f = MainDic.ContainsKey(t);
                using (SqlDataReader rdr = com.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (plist != null)
                        {
                            if ((plist.AllCount >= plist.StartPos || plist.StartPos == -1) && plist.AllCount < plist.StartPos + plist.PageSize || plist.StartPos == -1 || plist.PageSize == -1)
                            {
                                MObj obj = null;
                                if (f)
                                {
                                    obj = MainDic.CreateObj(t);
                                }
                                else
                                {
                                    obj = Activator.CreateInstance(t) as MObj;
                                    if (obj == null) throw new Exception($@"Тип ""{t}"" не наследует от MObj");
                                    obj.Model = this;
                                }
                                SetObjValues(obj, rdr);
                                if (obj is MEDMObj) MainDic.AddObj(obj as MEDMObj);
                                if (list != null) list.Add(obj);
                            }
                            plist.AllCount++;
                        }
                        else
                        {
                            MObj obj = null;
                            if (f)
                            {
                                obj = MainDic.CreateObj(t);
                            }
                            else
                            {
                                obj = Activator.CreateInstance(t) as MObj;
                                if (obj == null) throw new Exception($@"Тип ""{t}"" не наследует от MObj");
                                obj.Model = this;
                            }
                            SetObjValues(obj, rdr);
                            if (obj is MEDMObj) MainDic.AddObj(obj as MEDMObj);
                            if (list != null) list.Add(obj);
                        }

                    }
                }
                if (plist != null && allCount >= 0)
                {
                    plist.AllCount = allCount;
                }
                MainTrace.Add(TraceType.Sql, $"SELECT => {com.CommandText}");
            }
            finally
            {
                ConnectionPool.FreeConnection(con);
            }
            return list;
        }

        public MObj Load(MObj obj, string sql, params object[] parms)
        {
            SqlConnection con = ConnectionPool.GetConnection();
            try
            {
                parms = GetParamList(parms);
                DateTime begtime = DateTime.Now;
                SqlCommand com = new SqlCommand(sql, con);
                for (int i = 0; i < parms.Length - 1; i += 2)
                {
                    if (parms[i] != null) com.Parameters.Add(new SqlParameter(parms[i].ToString(), PrepareParam(parms[i + 1])));
                }
                using (SqlDataReader rdr = com.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        SetObjValues(obj, rdr);
                        break;
                    }
                }
                MainTrace.Add(TraceType.Sql, $"QUERY => {com.CommandText}");
            }
            finally
            {
                ConnectionPool.FreeConnection(con);
            }
            return obj;
        }

        public object Query(string sql, params object[] parms)
        {
            object result = null;
            SqlConnection con = ConnectionPool.GetConnection();
            try
            {
                parms = GetParamList(parms);
                DateTime begtime = DateTime.Now;
                SqlCommand com = new SqlCommand(sql, con);
                for (int i = 0; i < parms.Length - 1; i += 2)
                {
                    if (parms[i] != null) com.Parameters.Add(new SqlParameter(parms[i].ToString(), PrepareParam(parms[i + 1])));
                }
                using (SqlDataReader rdr = com.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result = rdr.GetValue(0);
                        break;
                    }
                }
                MainTrace.Add(TraceType.Sql, $"QUERY => {com.CommandText}");
            }
            finally
            {
                ConnectionPool.FreeConnection(con);
            }
            return result;
        }
        public T Query<T>(string sql, params object[] parms)
        {
            object r = Query(sql, parms);
            if (r == null) return default(T);
            return (T)MObj.GetTypeValue(typeof(T), r);
        }
        public void Exec(string sql, params object[] parms)
        {
            SqlConnection con = ConnectionPool.GetConnection();
            try
            {
                parms = GetParamList(parms);
                DateTime begtime = DateTime.Now;
                SqlCommand com = new SqlCommand(sql, con);
                for (int i = 0; i < parms.Length - 1; i += 2)
                {
                    if (parms[i] != null) com.Parameters.Add(new SqlParameter(parms[i].ToString(), PrepareParam(parms[i + 1])));
                }
                com.ExecuteNonQuery();
                MainTrace.Add(TraceType.Sql, $"EXEC => {com.CommandText}");
            }
            finally
            {
                ConnectionPool.FreeConnection(con);
            }
        }
        public O SelectFromXML<O>(string s, object id) where O : MObj
        {
            List<O> list = new List<O>();
            SelectFromXML(list, typeof(O), s, id);
            if (list.Count > 0) return list[0];
            return null;
        }
        public void SelectFromXML<O>(IList list, string s, object id) where O : MObj
        {
            SelectFromXML(list, typeof(O), s, id);
        }
        public void SelectFromXML(IList list, Type t, XmlNode xroot, object id)
        {
            if (xroot != null)
            {
                try
                {
                    bool f = MainDic.ContainsKey(t);
                    {
                        foreach (XmlNode xrow in xroot.ChildNodes)
                        {
                            if (xrow.Name == "row")
                            {
                                if (id == null || id.ToString() == "" || id.ToString().ToLower() == XFunc.GetAttr(xrow, "id", ""))
                                {
                                    MObj obj = null;
                                    if (f)
                                    {
                                        obj = MainDic.CreateObj(t);
                                    }
                                    else
                                    {
                                        obj = Activator.CreateInstance(t) as MObj;
                                        if (obj == null) throw new Exception($@"Тип ""{t}"" не наследует от MObj");
                                        obj.Model = this;
                                    }
                                    SetObjValues(obj, xrow);
                                    if (list != null) list.Add(obj);
                                    if (obj is MEDMObj) MainDic.AddObj(obj as MEDMObj);
                                }
                            }
                        }
                    }
                }
                finally
                {
                }
            }
        }
        public void SelectFromXML(IList list, Type t, string s, object id)
        {
            if (!string.IsNullOrEmpty(s))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(s);
                SelectFromXML(list, t, xdoc.DocumentElement, id);
            }
        }


        public override MEDMObj CreateObject(Type t)
        {
            /*
            if (!IsLockUpdates)
            {
                MUpdateAdapter ua = new MUpdateAdapter(MUpdateOperation.Create, obj);
                Updates.Add(ua);
            }
            */

            MEDMObj obj = null;
            obj = MainDic.CreateObj(t);

            MUpdateAdapter ua = new MUpdateAdapter(MUpdateOperation.Create, obj);
            Updates.Add(ua);
            //Save(null);

            LockUpdates++;
            try
            {
                //obj.SetId(GetNewId(t));
                MainDic.AddObj(obj);
            }
            finally
            {
                LockUpdates--;
            }

            return obj;
        }

        public override void UpdateProperty(object sender, string propname, object oldvalue, object value, Type proptype, Type reftype, bool isVirtual)
        {
            base.UpdateProperty(sender, propname, oldvalue, value, proptype, reftype, isVirtual);
            if (sender is MObj && IsLockUpdates)
            {
                if (reftype != null && MainDic.ContainsKey(reftype))
                {
                    MainDic.AddFutureRef(reftype, value);
                }
            }
            else if (!isVirtual)
            {
                if (Updates.LastAdapter != null && Updates.LastAdapter.Obj == sender)
                {
                    if (!Updates.LastAdapter.PropNames.Contains(propname)) Updates.LastAdapter.PropNames.Add(propname);
                }
                else if (sender is MEDMObj)
                {
                    MUpdateAdapter ua = new MUpdateAdapter(MUpdateOperation.Update, (sender as MEDMObj));
                    ua.PropNames.Add(propname);
                    Updates.Add(ua);
                }
                if (sender is MObj)
                {
                    (sender as MObj).Invalidate();
                }
            }
        }
        public override void DeleteObject(MEDMObj obj)
        {
            MUpdateAdapter ua = new MUpdateAdapter(MUpdateOperation.Delete, obj);
            Updates.Add(ua);
            obj.Invalidate();
        }
        public override void UpdateFutureRef(Type dictype, string tablename, string idname, string idtype)
        {
            if (MainDic.ContainsKey(dictype) && MainDic.HasFutureRefs(dictype))
            {
                string idlist = "";
                bool f = false;
                foreach (object r in MainDic.GetFutureRefs(dictype))
                {
                    if (r != null)
                    {
                        if (f) idlist += ',';
                        else f = true;
                        switch (idtype.ToUpper())
                        {
                            case "GUID":
                            case "STRING":
                                idlist += "'" + r.ToString() + "'";
                                break;
                            default:
                                idlist += r.ToString();
                                break;
                        }
                    }
                }
                MainDic.ClearFutureRefs(dictype);
                string sql = $"select * from [{tablename}] (nolock) where [{idname}] in ({idlist})";
                Select(null, dictype, sql);
            }
        }
        public override void Save(ISession session)
        {
            try
            {
                int n = 0;
                foreach (MUpdateAdapter ua in Updates)
                {
                    switch (ua.Operation)
                    {
                        case MUpdateOperation.Create:
                            n += 2 + ua.PropNames.Count * 2;
                            break;
                        case MUpdateOperation.Delete:
                            n += 2;
                            break;
                        case MUpdateOperation.Update:
                            n += 2 + ua.PropNames.Count * 2;
                            break;
                    }
                }
                object[] parms = new object[n];
                string sql = "";
                n = 0;
                foreach (MUpdateAdapter ua in Updates)
                {
                    //MEDMDefClass dc = MEDMDefModel.MainDef.Find(ua.Obj.GetType());
                    string name = ua.Table; //MEDMDefModel.MainDef.GetTableNameByType(ua.Obj.GetType()).ToLower();
                    string idname = "Id"; // MainDic[ua.Obj.GetType()].GetIdName().Invoke(null, null).ToString();
                    //if (dc.ClassType == "edm")
                    {
                        switch (ua.Operation)
                        {
                            case MUpdateOperation.Create:
                                {
                                    if (ua.Obj.GetType().GetTypeInfo().IsSubclassOf(typeof(MEDMIdObj)))
                                    {
                                        string p1 = "";
                                        string p2 = "";
                                        string d = "";
                                        foreach (string pn in ua.PropNames)
                                        {
                                            p1 += $"{d}[{pn}]";
                                            p2 += $"{d}@p{n}";
                                            parms[n] = $"p{n}";
                                            parms[n + 1] = ua.Obj.GetValue(pn);
                                            n += 2;
                                            d = ",";
                                        }
                                        sql += $"\r\ninsert [{name}]({p1}) values({p2})\r\nselect * from [{name}] where id=@@identity";
                                        //Query<int>(sql, parms);
                                        try
                                        {
                                            LockUpdates++;
                                            Load(ua.Obj, sql, parms);
                                        }
                                        finally
                                        {
                                            LockUpdates--;
                                        }
                                        sql = "";
                                        n = 0;
                                        for (int i = 0; i < parms.Length; i++) parms[i] = null;
                                    }
                                    else
                                    {
                                        string p1 = "";
                                        string p2 = "";
                                        string d = "";
                                        p1 += $"{d}[{idname}]";
                                        p2 += $"{d}@p{n}";
                                        parms[n] = $"p{n}";
                                        parms[n + 1] = ua.Obj.GetId();
                                        n += 2;
                                        d = ",";
                                        foreach (string pn in ua.PropNames)
                                        {
                                            p1 += $"{d}[{pn}]";
                                            p2 += $"{d}@p{n}";
                                            parms[n] = $"p{n}";
                                            parms[n + 1] = ua.Obj.GetValue(pn);
                                            n += 2;
                                            d = ",";
                                        }
                                        sql += $"\r\ninsert [{name}]({p1}) values({p2})";
                                    }
                                }
                                break;
                            case MUpdateOperation.Delete:
                                {
                                    sql += $"\r\ndelete [{name}] where [{idname}] = @p{n}";
                                    parms[n] = $"p{n}";
                                    parms[n + 1] = ua.Obj.GetId();
                                    n += 2;
                                }
                                break;
                            case MUpdateOperation.Update:
                                {
                                    string p = "";
                                    string d = "";
                                    foreach (string pn in ua.PropNames)
                                    {
                                        p += $"{d}[{pn}]=@p{n}";
                                        parms[n] = $"p{n}";
                                        parms[n + 1] = ua.Obj.GetValue(pn);
                                        n += 2;
                                        d = ",";
                                    }
                                    sql += $"\r\nupdate [{name}] set {p} where [{idname}] = @p{n}";
                                    parms[n] = $"p{n}";
                                    parms[n + 1] = ua.Obj.GetId();
                                    n += 2;
                                }
                                break;
                        }
                    }
                }
                if (sql != "") Exec(sql, parms);
            }
            finally
            {
                Updates.Clear();
            }
        }
        //public override object GetNewId(Type t)
        //{
        //    if (MainDic[t].GetIdType().Invoke(null, null).Equals(typeof(Guid))) return Guid.NewGuid();
        //    return base.GetNewId(t);
        //}
        #endregion

        #region CurrentUser
        private static Dictionary<string, ICurrentUser> _UserList = null;
        public static Dictionary<string, ICurrentUser> UserList
        {
            get
            {
                if (_UserList == null) _UserList = new Dictionary<string, ICurrentUser>();
                return _UserList;
            }
        }
        public override ICurrentUser GetCurrentUser(string name)
        {
            return UserList[name.ToLower()];
        }
        public override ICurrentUser GetCurrentUser(ClaimsPrincipal user)
        {
            if (user != null && user.Identity != null)
            {
                return GetCurrentUser(user.Identity.Name);
            }
            throw new Exception($"Пользователь {user.Identity.Name} не найден...");
        }
        public override void AddUser(ICurrentUser user, string name)
        {
            UserList[name.ToLower()] = user;
        }
        public override void RemoveUser(string name)
        {
            if (UserList.ContainsKey(name.ToLower())) UserList.Remove(name.ToLower());
        }
        #endregion
        #region Init
        public virtual void Init()
        {
            try
            {
                MainTrace.Add(TraceType.Init, $"Init Model => {this.GetType().Name}");
                InitTables();
                InitCfgs();
                InitUsers();
            }
            catch (Exception e)
            {
                MainTrace.Add(TraceType.Error, $"{e.Message}");
                throw new Exception("Init errors");
            }
        }
        public virtual void InitTables()
        {
            foreach (Type t in MainDic.Keys)
            {
                InitTable(t);
            }
        }
        public virtual void InitTable(Type t)
        {
            MainTrace.Add(TraceType.Init, $"{t.Name} ");
            // Создать таблицу если ее нет
            if (!Query<bool>($"select 1 from sysobjects where type='U' and name='{t.Name}'"))
            {
                if (t.GetTypeInfo().IsSubclassOf(typeof(MEDMCfgObj)))
                {
                    return;
                }
                else if (t.GetTypeInfo().IsSubclassOf(typeof(MEDMIdObj)))
                {
                    MainTrace.Add(TraceType.Init, $"create table [{t.Name}]");
                    Exec($"CREATE TABLE [dbo].[{t.Name}]([Id] int identity not null)");
                    Exec($"ALTER TABLE [dbo].[{t.Name}] ADD CONSTRAINT PK_{t.Name} PRIMARY KEY CLUSTERED (Id) WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]");

                }
                else if (t.GetTypeInfo().IsSubclassOf(typeof(MEDMNameObj)))
                {
                    MainTrace.Add(TraceType.Init, $"create table [{t.Name}]");
                    Exec($"CREATE TABLE [dbo].[{t.Name}]([Id] nvarchar(255) not null default(''))");
                    Exec($"ALTER TABLE [dbo].[{t.Name}] ADD CONSTRAINT PK_{t.Name} PRIMARY KEY CLUSTERED (Id) WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]");
                }
                else if (t.GetTypeInfo().IsSubclassOf(typeof(MEDMGuidObj)))
                {
                    MainTrace.Add(TraceType.Init, $"create table [{t.Name}]");
                    Exec($"CREATE TABLE [dbo].[{t.Name}]([Id] uniqueidentifier not null default (newid()) )");
                    Exec($"ALTER TABLE [dbo].[{t.Name}] ADD CONSTRAINT PK_{t.Name} PRIMARY KEY CLUSTERED (Id) WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]");
                }
                else
                {
                    return;
                }
            }
            // Создать поля таблицы
            Dictionary<string, PropertyInfo> classColumns = GetClassColumns(t);
            Dictionary<string, MColumnScheme> schemeColumns = GetSchemeColumns(t);

            // Проходим по описателям
            foreach (string n in classColumns.Keys)
            {
                PropertyInfo pi = classColumns[n];
                DbColumnAttribute ca = pi.GetCustomAttribute<DbColumnAttribute>();
                // Есть в схеме
                if (schemeColumns.ContainsKey(n))
                {
                    MColumnScheme cs = schemeColumns[n];
                    if (ca.Type != cs.Type)
                    {
                        MainTrace.Add(TraceType.Init, $"alter column [{pi.Name}] {ca.Type}");
                        Exec($"ALTER TABLE [dbo].[{t.Name}] ALTER COLUMN [{pi.Name}] {ca.Type} not null");
                    }
                    if (ca.Def.Replace("(", "").Replace(")", "") != cs.Def.Replace("(", "").Replace(")", ""))
                    {
                        MainTrace.Add(TraceType.Init, $"alter default [{pi.Name}] {ca.Def}");
                        try
                        {
                            Exec($"ALTER TABLE [dbo].[{t.Name}] DROP CONSTRAINT DF_{t.Name}_{pi.Name}");
                        }
                        catch { }
                        Exec($"ALTER TABLE [dbo].[{t.Name}] ADD CONSTRAINT DF_{t.Name}_{pi.Name} DEFAULT {ca.Def} FOR [{pi.Name}]");
                    }
                }
                // Нет в схеме
                else
                {
                    MainTrace.Add(TraceType.Init, $"create column [{pi.Name}] {ca.Type} default {ca.Def}");
                    Exec($"ALTER TABLE [dbo].[{t.Name}] ADD [{pi.Name}] {ca.Type}");
                    Exec($"ALTER TABLE [dbo].[{t.Name}] ADD CONSTRAINT DF_{t.Name}_{pi.Name} DEFAULT {ca.Def} FOR [{pi.Name}]");
                }
            }
            // Проходим по колонкам
            foreach (string n in schemeColumns.Keys)
            {
                // Нет в классах
                if (!classColumns.ContainsKey(n))
                {
                    MColumnScheme cs = schemeColumns[n];
                    MainTrace.Add(TraceType.Init, $"drop column [{cs.column_name}]");
                    try
                    {
                        Exec($"ALTER TABLE [dbo].[{t.Name}] DROP CONSTRAINT DF_{t.Name}_{cs.column_name}");
                    }
                    catch { }
                    Exec($"ALTER TABLE [dbo].[{t.Name}] DROP COLUMN [{cs.column_name}]");
                }
            }

        }
        // Список свойств(колонок) класса для формирования таблицы
        // Не видим свойство id (оно должно быть всегда) и свойства с незаданным атрибутом DBColumn или если Virtual=true
        private Dictionary<string, PropertyInfo> GetClassColumns(Type t)
        {
            Dictionary<string, PropertyInfo> classColumns = new Dictionary<string, PropertyInfo>();
            foreach (PropertyInfo pi in t.GetTypeInfo().GetProperties())
            {
                DbColumnAttribute ca = pi.GetCustomAttribute<DbColumnAttribute>();
                if (pi.Name.ToLower() != "id" &&  ca!= null && !ca.Virtual)
                {
                    classColumns[pi.Name.ToLower()] = pi;
                }
            }
            return classColumns;
        }
        private Dictionary<string, MColumnScheme> GetSchemeColumns(Type t)
        {
            Dictionary<string, MColumnScheme> schemeColumns = new Dictionary<string, MColumnScheme>();
            List<MColumnScheme> l = new List<MColumnScheme>();
            Select<MColumnScheme>(l, $"Select * from information_schema.COLUMNS where TABLE_NAME='{t.Name}'");
            foreach (MColumnScheme cs in l)
            {
                if (cs.column_name.ToLower() != "id")
                {
                    schemeColumns[cs.column_name.ToLower()] = cs;
                }
            }
            return schemeColumns;
        }
        public virtual void InitUsers()
        {
        }
        public virtual void InitUser(string login, string name, string password)
        {
        }
        public virtual void InitCfgs()
        {

        }
        public virtual void InitCfg(string filename)
        {

        }
        #endregion

    }
    #region MUpdateAdapter
    public enum MUpdateOperation { Create, Update, Delete }
    public class MUpdateAdapter
    {
        public MUpdateOperation Operation { get; set; }
        public MEDMObj Obj { get; set; }
        public List<string> PropNames;
        public MUpdateAdapter(MUpdateOperation operation, MEDMObj obj)
        {
            Operation = operation;
            Obj = obj;
            PropNames = new List<string>();
        }
        public string Table
        {
            get
            {
                return Obj.GetType().Name;
            }
        }
    }
    public class MUpdateAdapterList : List<MUpdateAdapter>
    {
        public MUpdateAdapter LastAdapter
        {
            get
            {
                if (Count > 0) return this[Count - 1];
                return null;
            }
        }

    }
    #endregion
    #region ParamValues
    public class MParamValues : List<object>
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
    #endregion
    public class MColumnScheme : MObj
    {
        public string column_name { get; set; }
        public string data_type { get; set; }
        public bool is_nullable { get; set; }
        public string column_default { get; set; }
        public int character_maximum_length { get; set; }
        public int numeric_precision { get; set; }
        public string character_set_name { get; set; }
        public string collation_name { get; set; }
        public string Type
        {
            get
            {
                string v = data_type;
                switch (data_type.ToLower())
                {
                    case "nvarchar":
                        if (character_maximum_length == -1) v = $"nvarchar(MAX)";
                        else v = $"nvarchar({character_maximum_length})";
                        break;
                    case "decimal":
                        v = $"decimal({character_maximum_length}, {numeric_precision})";
                        break;
                }
                return v;
            }
        }
        public string Def
        {
            get
            {
                char[] d = { '(', ')' };
                string v = column_default;
                if (v == null) return "";
                v = v.Trim(d);
                return v;
            }
        }
    }

    public class DbTableAttribute : Attribute
    {
        public string Type { get; set; }
    }
    public class DbColumnAttribute : Attribute
    {
        public string Type { get; set; }
        public string Def { get; set; }
        public bool Virtual { get; set; }
    }

}
