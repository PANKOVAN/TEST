using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MEDMCoreLibrary
{
    public class MEDM
    {
        //public static DateTime NULLDateTime = new DateTime(1753, 1, 1, 12, 0, 0);
        public static DateTime NULLDateTime = new DateTime(1953, 1, 1, 12, 0, 0);
        #region BaseDir
        private static string _BaseDir = "";
        public string BaseDir
        {
            get
            {
                return _BaseDir;
            }
            set
            {
                _BaseDir = value;
            }
        }
        #endregion
        #region Словарь
        private MEDMMainDic _MainDic = null;
        public MEDMMainDic MainDic
        {
            get
            {
                if (_MainDic == null) _MainDic = new MEDMMainDic();
                return _MainDic;
            }
        }
        #endregion
        public Type GetClassTypeByClassName(string t)
        {
            t = t.ToLower();
            foreach (Type type in MainDic.Keys)
            {
                if (type.Name.ToLower() == t) return type;
            }
            return null;
        }
        /*
        #region GetNewId
        public virtual object GetNewId( t)
        {
            if (t)
            {
                int _id = 0;
                if (AutoIncrementDic.ContainsKey(t)) _id = AutoIncrementDic[t];
                _id--;
                AutoIncrementDic[t] = _id;
                return _id;
            }
            else if (t is )
            throw new Exception($"Для типа {t.Name} не возможно вычислить уникальный идентификатор");
        }
        #endregion
    */
        #region IsChanged
        private bool _IsChanged = false;
        public bool IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set
            {
                if (_IsChanged != value)
                {
                    _IsChanged = value;
                }
            }
        }
        #endregion
        #region Update
        public virtual MEDMObj CreateObject(Type t)
        {
            return null;
        }
        public virtual void UpdateProperty(object sender, string propname, object oldvalue, object value, Type proptype, Type reftype, bool isVirtual)
        {

        }
        public virtual void DeleteObject(MEDMObj obj)
        {

        }
        public virtual void Save(ISession session)
        {

        }
        public virtual void UpdateFutureRef(Type dictype, string tablename, string idname, string idtype)
        {

        }
        public bool IsLockUpdates
        {
            get
            {
                return LockUpdates > 0;
            }
        }
        private int _LockUpdates = 0;
        public int LockUpdates
        {
            get
            {
                return _LockUpdates;
            }
            set
            {
                _LockUpdates = value;
            }
        }
        #endregion
        #region DisplayValue()
        public virtual string DisplayValue(object value, string format = "")
        {
            if (value != null)
            {
                if (value is DateTime)
                {
                    if (((DateTime)value) <= NULLDateTime) return "";
                    if (format == "") format = "dd.MM.yy";
                    return ((DateTime)value).ToString(format);
                }
                else
                {
                    return value.ToString();
                }
            }
            return "";
        }
        #endregion
        #region CurrentUser
        public virtual ICurrentUser GetCurrentUser(string name)
        {
            throw new Exception("Метот GetCurrentUser не переопределен");
        }
        public virtual ICurrentUser GetCurrentUser(System.Security.Claims.ClaimsPrincipal user)
        {
            throw new Exception("Метот GetCurrentUser не переопределен");
        }
        public virtual void AddUser(ICurrentUser user, string name)
        {
            throw new Exception("Метот AddUser не переопределен");
        }
        public virtual void RemoveUser(string name)
        {
            throw new Exception("Метот RemoveUser не переопределен");
        }
        #endregion
        #region Трасировщик запросов
        private EDMTrace _MainTrace = null;
        public EDMTrace MainTrace
        {
            get
            {
                if (_MainTrace == null) _MainTrace = new EDMTrace();
                return _MainTrace;
            }
        }
        #endregion

    }
    #region Классы для словаря
    public class MEDMObjectDic : Dictionary<object, MEDMObj>
    {
        public MEDM Model;
        public Type MainType = null;
        public bool IsCfgType = false;
        #region Конструктор
        public MEDMObjectDic(MEDM model, Type type)
        {
            Model = model;
            MainType = type;
            IsCfgType = MainType.GetTypeInfo().IsSubclassOf(typeof(MEDMCfgObj));
        }
        #endregion

        #region Id
        private MethodInfo _GetIdType = null;
        public MethodInfo GetIdType()
        {
            if (_GetIdType == null)
            {
                Type[] parms = { };
                _GetIdType = MainType.GetRuntimeMethod("GetIdType", parms);
            }
            return _GetIdType;
        }

        private MethodInfo _GetIdName = null;
        public MethodInfo GetIdName()
        {
            if (_GetIdName == null)
            {
                Type[] parms = { };
                _GetIdName = MainType.GetRuntimeMethod("GetIdName", parms);
            }
            return _GetIdName;
        }

        private MethodInfo _StringToIdType = null;
        public MethodInfo StringToIdType()
        {
            if (_StringToIdType == null)
            {
                Type[] parms = { typeof(string) };
                _StringToIdType = MainType.GetRuntimeMethod("StringToIdType", parms);
            }
            return _StringToIdType;
        }


        public object PrepareId(object id)
        {
            TypeInfo ti = MainType.GetTypeInfo();
            if (ti.IsSubclassOf(typeof(MEDMIdObj)))
            {
                if (id == null) id = 0;
                else if (!(id is int)) id = Convert.ToInt32(id);
            }
            else if (ti.IsSubclassOf(typeof(MEDMNameObj)))
            {
                if (id == null) id = "";
                else id = id.ToString();
            }
            else if (ti.IsSubclassOf(typeof(MEDMGuidObj)))
            {
                if (id == null) id = default(Guid);
                else if (!(id is Guid)) id = Convert.ToInt32(id);
            }
            return id;
        }

        #endregion
        #region Общие методы

        public T CreateObj<T>() where T : MEDMObj
        {
            return CreateObj(typeof(T)) as T;
        }
        public virtual MEDMObj CreateObj(Type t)
        {
            MEDMObj obj = Activator.CreateInstance(t) as MEDMObj;
            obj.Model = Model;
            return obj;
        }
        public MEDMObj AddObj(MEDMObj obj)
        {
            if (obj != null)
            {
                if (this.ContainsKey(obj.GetId()))
                {
                    MEDMObj obj1 = this[obj.GetId()];
                    Model.LockUpdates++;
                    obj1.CopyValues(obj);
                    Model.LockUpdates--;
                    obj = obj1;
                }
                else
                {
                    object id = obj.GetId();
                    if (id is string) id = ((string)id).ToUpper().Trim();
                    this[id] = obj;
                }
                obj.Model = Model;
                obj.Invalidate(false);
            }
            return obj;
        }
        public T NewObj<T>(object id) where T : MEDMObj
        {
            return NewObj(typeof(T), id) as T;
        }
        public MEDMObj NewObj(Type t, object id)
        {
            if (id == null) throw new Exception($"MEDM.NewObj: id is null ({t})");
            id = PrepareId(id);
            MEDMObj obj = FindObj(id);
            if (obj != null) throw new Exception($"MEDM.NewObj: not unique id ({t}:{id})");
            obj = CreateObj(t);
            Model.LockUpdates++;
            try
            {
                obj.SetId(id);
            }
            finally
            {
                Model.LockUpdates--;
            }
            AddObj(obj);
            return obj;
        }
        public MEDMObj GetObj(Type t, object id)
        {
            MEDMObj obj = null;
            if (id != null)
            {
                id = PrepareId(id);
                obj = FindObj(id);
            }
            if (obj == null)
            {
                obj = CreateObj(t);
                //obj = obj.Model.CreateObject(t);
                Model.LockUpdates++;
                try
                {
                    obj.SetId(id);
                }
                finally
                {
                    Model.LockUpdates--;
                }
                AddObj(obj);
            }
            return obj;
        }
        public T GetObj<T>(object id) where T : MEDMObj
        {
            return GetObj(typeof(T), id) as T;
        }
        public MEDMObj FindObj(object id)
        {
            if (id == null) return default(MEDMObj);
            id = PrepareId(id);
            if (id is string) id = ((string)id).ToUpper().Trim();
            if (this.ContainsKey(id)) return this[id] as MEDMObj;
            return default(MEDMObj);
        }
        public T FindObj<T>(object id) where T : MEDMObj
        {
            return FindObj(id) as T;
        }
        public void DelObj(MEDMObj obj)
        {
            object id = obj.GetId();
            if (id is string) id = ((string)id).ToUpper().Trim();
            if (this.ContainsKey(id)) this.Remove(id);
        }

        #endregion
        #region Нереализованные ссылки
        private HashSet<object> _FutureRefs = null;
        public HashSet<object> FutureRefs
        {
            get
            {
                if (_FutureRefs == null) _FutureRefs = new HashSet<object>();
                return _FutureRefs;
            }
        }
        public void AddFutureRef(object id)
        {
            if (!IsCfgType)
            {
                MEDMObj obj = FindObj(id);
                if ((obj == null || obj.IsInvalidate()) && !FutureRefs.Contains(id)) FutureRefs.Add(id);
            }
        }
        #endregion
    }
    public class MEDMMainDic : Dictionary<Type, MEDMObjectDic>
    {
        #region Общие методы
        public T CreateObj<T>() where T : MEDMObj
        {
            MEDMObjectDic odic = this[typeof(T)];
            return odic.CreateObj<T>();
        }
        public MEDMObj CreateObj(Type t)
        {
            MEDMObjectDic odic = this[t];
            return odic.CreateObj(t);
        }
        public T NewObj<T>(object id) where T : MEDMObj
        {
            MEDMObjectDic odic = this[typeof(T)];
            return odic.NewObj<T>(id);
        }
        public MEDMObj NewObj(Type t, object id)
        {
            MEDMObjectDic odic = this[t];
            return odic.NewObj(t, id);
        }
        public MEDMObj GetObj(Type t, object id)
        {
            MEDMObjectDic odic = this[t];
            return odic.GetObj(t, id);
        }
        public T GetObj<T>(object id) where T : MEDMObj
        {
            return GetObj(typeof(T), id) as T;
        }
        public delegate bool GetAllFilterHandler(object filter=null);
        public List<T> GetAll<T>(GetAllFilterHandler filter = null) where T : MEDMObj
        {
            List<T> l = new List<T>();
            GetAll(l, typeof(T), filter);
            return l;
        }
        public IList GetAll(IList l, Type t, GetAllFilterHandler filter=null)
        {
            MEDMObjectDic odic = this[t];
            if (l == null) l = new List<object>();
            foreach (object o in odic.Values)
            {
                if (filter==null || filter(o)) l.Add(o);
            }
            return l;
        }
        public MEDMObj FindObj(Type t, object id)
        {
            MEDMObjectDic odic = this[t];
            return odic.FindObj(id);
        }
        public T FindObj<T>(object id) where T : MEDMObj
        {
            return FindObj(typeof(T), id) as T;
        }
        public MEDMObj AddObj(MEDMObj obj)
        {
            if (obj != null)
            {
                MEDMObjectDic odic = this[obj.GetType()];
                return odic.AddObj(obj);
            }
            return null;
        }
        #endregion
        #region Нереализованные ссылки
        public void AddFutureRef(Type t, object id)
        {
            MEDMObjectDic odic = this[t];
            odic.AddFutureRef(id);
        }
        public IEnumerable<object> GetFutureRefs(Type t)
        {
            MEDMObjectDic odic = this[t];
            return odic.FutureRefs;
        }
        public bool HasFutureRefs(Type t)
        {
            MEDMObjectDic odic = this[t];
            return odic.FutureRefs.Count > 0;
        }
        public void ClearFutureRefs(Type t)
        {
            MEDMObjectDic odic = this[t];
            odic.FutureRefs.Clear();
        }
        #endregion
    }
    #endregion
    public class NotAuthException : Exception
    {
        public NotAuthException()
        {

        }
        public NotAuthException(string message) : base(message)
        {

        }
    }
    public interface ICurrentUser
    {
        object GetUserId();
        string GetUserName();
        string GetUserHeader();
        bool TestRole(string role);
        bool UserIsAdmin();
        bool UserIsReadOnly();
    }
    /*
    public class EDMTraceItem
    {
        public string Type { get; set; }
        public string Info { get; set; }
        public DateTime BegTime { get; set; }
        public DateTime EndTime { get; set; }
        public EDMTraceItem(string type, string info, DateTime begtime, DateTime endtime = default(DateTime))
        {
            Type = type;
            Info = info;
            BegTime = begtime;
            EndTime = (endtime == default(DateTime)) ? DateTime.Now : endtime;
        }
    }
    */
    public enum TraceType { Info, Error, Warning, Debug, Sql, Init, Cfg }
    public class EDMTrace
    {
        public static bool IsEnabled { get; set; }
        public void Add(TraceType type, string info)
        {
            if (IsEnabled)
            {
                switch (type)
                {
                    case TraceType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case TraceType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case TraceType.Sql:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case TraceType.Debug:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"{type.ToString().ToLower().PadRight(10)}: {info}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        /*
        public string GetAsHTML()
        {
            string html = "";
            if (IsEnabled && Count > 0)
            {
                html += @"<div class=""text-danger""><small><small><small><table class=""table-condensed table-bordered"">";
                long t = 0;
                long t1 = 0;
                DateTime d;
                foreach (EDMTraceItem item in this)
                {
                    t = item.EndTime.Ticks - item.BegTime.Ticks;
                    t1 += t;
                    d = new DateTime(t);
                    html += $@"<tr><td>{item.Type}</td><td>{item.Info.Replace("\n", "<br/>")}</td><td>{d.Second.ToString("d2")}:{d.Millisecond.ToString("d3")}</td><tr/>";
                }
                d = new DateTime(t1);
                html += $@"<tr><td></td><td></td><td>{d.Second.ToString("d2")}:{d.Millisecond.ToString("d3")}</td><tr/>";
                html += "</table></small></small></small></div>";
            }
            IsEnabled = false;
            Clear();
            return html;
        }
        */
    }
    public class IgnoreAttribute : Attribute
    {

    }
}
