using MFuncCoreLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace MEDMCoreLibrary
{
    /// <summary>
    /// Базовый класс для всех объектов
    /// </summary>
    public abstract class MObj
    {
        #region Model

        private MEDM _Model_;
        [JsonIgnore]
        public MEDM Model
        {
            get
            {
                return _Model_;
            }
            set
            {
                _Model_ = value;
            }
        }
        public bool ShouldSerializeModel()
        {
            return false;
        }

        protected string _ParentName = "";
        protected MEDMObj _ParentObj = null;
        public void SetParent(MEDMObj parentobj, string parentname)
        {
            _ParentName = parentname;
            _ParentObj = parentobj;
        }
        #endregion

        #region GetValue
        public object GetValue(string name)
        {
            name = name.ToLower();
            string lastname = "";
            int i = name.IndexOf('.');
            if (i > 0)
            {
                lastname = name.Substring(i + 1);
                name = name.Substring(0, i);
            }
            foreach (PropertyInfo pi in GetType().GetRuntimeProperties())
            {
                if (pi.Name.ToLower() == name && pi.CanRead)
                {
                    object o = pi.GetValue(this, null);
                    if (lastname != "" && o is MObj) o = (o as MObj).GetValue(lastname);
                    return o;
                }
            }
            return null;
        }
        public string GetValueAsString(string name)
        {
            object o = GetValue(name);
            if (o != null)
            {
                if (o is bool)
                {
                    return ((bool)o) ? "1" : "0";
                }
                else if (o is DateTime)
                {
                    if (((DateTime)o) < NULLDateTime1) return "";
                    return ((DateTime)o).ToString("yyyy-MM-dd hh:mm:ss");
                }
                else return o.ToString();
            }
            return "";
        }
        #endregion
        #region SetValue
        public static DateTime NULLDateTime = new DateTime(1753, 1, 1, 12, 0, 0);
        public static DateTime NULLDateTime1 = new DateTime(1800, 1, 1, 0, 0, 0);
        private static object GetDefautValueForType(Type t)
        {
            if (t.Equals(typeof(bool))) return default(bool);
            if (t.Equals(typeof(short))) return default(short);
            if (t.Equals(typeof(int))) return default(int);
            if (t.Equals(typeof(long))) return default(long);
            if (t.Equals(typeof(byte))) return default(byte);
            if (t.Equals(typeof(double))) return default(double);
            if (t.Equals(typeof(decimal))) return default(decimal);
            if (t.Equals(typeof(DateTime))) return NULLDateTime;
            if (t.Equals(typeof(string))) return default(string);
            if (t.Equals(typeof(Guid))) return default(Guid);
            return null;
        }
        public static object GetTypeValue(Type pt, object value)
        {
            TypeInfo ti = pt.GetTypeInfo();
            if (pt == typeof(bool))
            {
                if (!(value is bool))
                {
                    value = (value.ToString() == "1" || value.ToString().ToLower() == "true");
                }
            }
            else if (pt == typeof(short))
            {
                if (!(value is short))
                {
                    try
                    {
                        value = Convert.ToInt16(value);
                    }
                    catch
                    {
                        value = default(short);
                    }
                }
            }
            else if (pt == typeof(int))
            {
                if (!(value is int))
                {
                    try
                    {
                        value = Convert.ToInt32(value);
                    }
                    catch
                    {
                        value = default(int);
                    }
                }
            }
            else if (pt == typeof(long))
            {
                if (!(value is long))
                {
                    try
                    {
                        value = Convert.ToInt64(value);
                    }
                    catch
                    {
                        value = default(long);
                    }
                }
            }
            else if (pt == typeof(byte))
            {
                if (!(value is byte))
                {
                    try
                    {
                        value = Convert.ToByte(value);
                    }
                    catch
                    {
                        value = default(byte);
                    }
                }
            }
            else if (pt == typeof(double))
            {
                if (!(value is double))
                {
                    try
                    {
                        value = MFunc.StringToDouble(value.ToString());
                    }
                    catch
                    {
                        value = default(double);
                    }
                }
            }
            else if (pt == typeof(decimal))
            {
                if (!(value is decimal))
                {
                    try
                    {
                        value = MFunc.StringToDecimal(value.ToString());
                    }
                    catch
                    {
                        value = default(decimal);
                    }
                }
            }
            else if (pt == typeof(DateTime))
            {
                if (!(value is DateTime))
                {
                    try
                    {
                        value = Convert.ToDateTime(value);
                    }
                    catch
                    {
                        value = default(DateTime);
                    }
                }
                if ((DateTime)value < NULLDateTime1) value = NULLDateTime;
            }
            else if (pt == typeof(string))
            {
                if (!(value is string)) value = value.ToString();
            }
            else if (pt == typeof(Guid))
            {
                if (!(value is Guid))
                {
                    try
                    {
                        value = new Guid(value.ToString());
                    }
                    catch
                    {
                        value = default(Guid);
                    }
                }
            }
            else if (ti.IsEnum)
            {
                value = Enum.Parse(pt, value.ToString(), true);
            }
            else
            {
                value = null;
            }
            return value;
        }
        public static string GetStringValue(object value)
        {
            if (value != null)
            {
                if (value is bool)
                {
                    return ((bool)value) ? "1" : "0";
                }
                if (value is double)
                {
                    return MFunc.DoubleToString((double)value);
                }
                else if (value is decimal)
                {
                    return MFunc.DecimalToString((decimal)value);
                }
                else if (value is DateTime)
                {
                    return MFunc.DateToString((DateTime)value);
                }
                else
                {
                    return value.ToString();
                }
            }
            return null;
        }
        public virtual void SetValue(string name, object value, object self = null, bool ignoreObjects = false)
        {
            if (self == null) self = this;
            name = name.ToLower();
            string lastname = "";
            int i = name.IndexOf('.');
            if (i > 0)
            {
                lastname = name.Substring(i + 1);
                name = name.Substring(0, i);
            }
            foreach (PropertyInfo pi in self.GetType().GetRuntimeProperties())
            {
                if (pi.Name.ToLower() == name)
                {
                    SetValue(pi, value, self, lastname,  ignoreObjects);
                    break;
                }
            }
        }
        public virtual void SetValue(PropertyInfo pi, object value, object self = null, string lastname = "", bool ignoreObjects = false)
        {
            if (value != null)
            {
                if (self == null) self = this;
                Type pt = pi.PropertyType;
                if ((pi.CanWrite && lastname == ""))
                {
                    pi.SetValue(self, GetTypeValue(pt, value), null);
                }
                else
                {

                    object o = pi.GetValue(self);
                    if (o != null)
                    {
                        if (lastname != "")
                        {
                            SetValue(lastname, value, o);
                        }
                        else if (pi.PropertyType.GetTypeInfo().IsSubclassOf(typeof(MEDMObj)))
                        {
                            if (!ignoreObjects)
                            {
                                JObject jo = JObject.Parse(value.ToString());
                                object id = jo["id"];
                                if (id != null)
                                {
                                    PropertyInfo pi1 = self.GetType().GetRuntimeProperty(pi.Name + "Id");
                                    if (pi1 != null)
                                    {
                                        pi1.SetValue(self, GetTypeValue(pi1.PropertyType, id), null);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                if (pi.PropertyType.GetTypeInfo().IsSubclassOf(typeof(MEDMObj)))
                {
                    if (!ignoreObjects)
                    {
                        PropertyInfo pi1 = self.GetType().GetRuntimeProperty(pi.Name + "Id");
                        if (pi1 != null)
                        {
                            pi1.SetValue(self, GetDefautValueForType(pi1.PropertyType), null);
                        }
                    }
                }
                else
                {
                    pi.SetValue(self, GetDefautValueForType(pi.PropertyType), null);
                }
            }
        }
        public virtual void SetStringValue(string name, string value, bool ignoreObjects)
        {
            SetValue(name, value, null, ignoreObjects);
        }
        public virtual void SetStringValue(PropertyInfo pi, string value, bool ignoreObjects)
        {
            SetValue(pi, value, null, "", ignoreObjects);
        }
        public virtual void SetValues(XmlNode xnode)
        {
            foreach (PropertyInfo pi in GetType().GetRuntimeProperties())
            {
                EDMPropertyAttribute pa = pi.GetCustomAttribute<EDMPropertyAttribute>();
                string pt = "";
                if (pa != null) pt = pa.PropType.ToUpper();

                switch (pt)
                {
                    case "LIST":
                        object l = pi.GetValue(this);
                        if (l is IList)
                        {
                            string n = pi.Name;
                            string[] v = XFunc.GetAttr(xnode, n, "").Split(';');
                            Type t = pa.ItemType;
                            foreach (string id in v)
                            {
                                if (!string.IsNullOrEmpty(id))
                                {
                                    (l as IList).Add(Model.MainDic.GetObj(t, id));
                                }
                            }

                        }
                        break;
                    default:
                        {
                            if (pi.CanRead && pi.CanWrite)
                            {
                                string n = pi.Name;
                                string v = XFunc.GetAttr(xnode, n, "");
                                // Значение задано в текущем узле
                                if (v != "")
                                {
                                    SetStringValue(pi, v, false);
                                }
                                // Попробуем поискать значение в родительском узле
                                else
                                {
                                    XmlNode xnode1 = xnode.ParentNode;
                                    while (xnode1 != null)
                                    {
                                        v = XFunc.GetAttr(xnode1, $"default.{n}", "");
                                        if (v != "")
                                        {
                                            SetStringValue(pi, v, false);
                                            break;
                                        }
                                        xnode1 = xnode1.ParentNode;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
        #endregion
        #region CopyValues
        public void CopyValues(MObj source)
        {
            if (source != null)
            {
                foreach (PropertyInfo pi in this.GetType().GetRuntimeProperties())
                {
                    if (pi.CanWrite && pi.Name != "Model" && pi.Name != "LockUpdates")
                    {
                        SetValue(pi.Name, source.GetValue(pi.Name));
                    }
                }
            }
        }
        #endregion
        #region ToString()
        public virtual string GetHeader()
        {
            return "";
        }
        public override string ToString()
        {
            return GetHeader();
        }
        #endregion
        #region DisplayValue()
        public virtual string DisplayValue(object value, string format = "")
        {
            return Model.DisplayValue(value, format);
        }
        #endregion

        #region Invalidate
        private bool _IsInvalidate = false;
        public virtual bool IsInvalidate()
        {
            return _IsInvalidate;
        }
        public virtual void Invalidate(bool invalidate = true)
        {
            _IsInvalidate = invalidate;
        }
        #endregion

        #region SetDefaultValues
        public virtual void SetDefaultValues()
        {

        }
        #endregion
    }
    /// <summary>
    /// Базовый класс для всех объектов, которые могут хранится в хранилищи
    /// </summary>
    public abstract class MEDMObj : MObj
    {
        #region Id
        public static Type GetIdType()
        {
            throw new NotImplementedException();
        }
        public static string GetIdName()
        {
            return "Id";
        }
        public static object StringToIdType(string id)
        {
            throw new NotImplementedException();
        }
        public virtual object GetId()
        {
            throw new NotImplementedException();
        }
        public virtual void SetId(object id)
        {
            throw new NotImplementedException();
        }
        [JsonIgnore]
        public virtual bool IdIsEmpty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static bool IsEmptyId(object id)
        {
            if (id == null) return true;
            if (id is Guid && id.Equals(default(Guid))) return true;
            if (id is short && id.Equals(default(short))) return true;
            if (id is int && id.Equals(default(int))) return true;
            if (id is long && id.Equals(default(long))) return true;
            if (id is string && string.IsNullOrEmpty(id.ToString())) return true;
            return false;
        }
        #endregion
        #region IsDel
        public virtual bool GetIsDel()
        {
            throw new NotImplementedException();
        }
        public virtual void SetIsDel(bool isdelete)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Destroy
        public virtual bool DestroyObj()
        {
            return true;
        }
        public virtual bool RestrictDestroy()
        {
            return true;
        }
        public virtual void CascadeDestroy()
        {
        }
        #endregion
    }
    /// <summary>
    /// Базовый класс для всех объектов с id типа string
    /// </summary>
    public abstract class MEDMNameObj : MEDMObj
    {
        #region Общие функции
        public new static Type GetIdType()
        {
            return typeof(string);
        }
        public new static string GetIdName()
        {
            return "Id";
        }
        public new static object StringToIdType(string id)
        {
            return id;
        }
        public override object GetId()
        {
            return Id;
        }
        public override void SetId(object id)
        {
            if (id != null) Id = id.ToString();
            else Id = null;
        }
        [Ignore]
        public override bool IdIsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Id);
            }
        }
        public override string GetHeader()
        {
            return Id;
        }
        #endregion
        #region [Id] Id
        private string _Id_ = default(string);
        public string Id
        {
            get
            {
                return _Id_;
            }
            set
            {
                Model.UpdateProperty(this, "Id", _Id_, value, typeof(Guid), null, false);
                _Id_ = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// Базовый класс для всех объектов с id типа int
    /// </summary>
    public abstract class MEDMIdObj : MEDMObj
    {
        private static Dictionary<Type, int> _AutoIncrementDic = null;
        private static Dictionary<Type, int> AutoIncrementDic
        {
            get
            {
                if (_AutoIncrementDic == null) _AutoIncrementDic = new Dictionary<Type, int>();
                return _AutoIncrementDic;
            }
        }

        #region Общие функции
        public new static Type GetIdType()
        {
            return typeof(int);
        }
        public new static string GetIdName()
        {
            return "Id";
        }
        public new static object StringToIdType(string id)
        {
            return MFunc.StringToInt(id);
        }
        public override object GetId()
        {
            return Id;
        }
        public override void SetId(object id)
        {
            if (id != null) Id = Convert.ToInt32(id);
            else
            {
                Type t = this.GetType();
                int _id = 0;
                if (AutoIncrementDic.ContainsKey(t)) _id = AutoIncrementDic[t];
                AutoIncrementDic[t] = _id--;
                Id = _id;
            }
        }
        [Ignore]
        public override bool IdIsEmpty
        {
            get
            {
                return Id == 0;
            }
        }
        public override string GetHeader()
        {
            return Id.ToString();
        }
        #endregion
        #region [Id] Id
        private int _Id_ = default(int);
        public int Id
        {
            get
            {
                return _Id_;
            }
            set
            {
                Model.UpdateProperty(this, "Id", _Id_, value, typeof(Guid), null, false);
                _Id_ = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// Базовый класс для всех объектов с id типа guid
    /// </summary>
    public abstract class MEDMGuidObj : MEDMObj
    {
        #region Общие функции
        public new static Type GetIdType()
        {
            return typeof(Guid);
        }
        public new static string GetIdName()
        {
            return "Id";
        }
        public new static object StringToIdType(string id)
        {
            return new Guid(id);
        }
        public override object GetId()
        {
            return Id;
        }
        public override void SetId(object id)
        {
            if (id == null) Id = Guid.NewGuid();
            else if (id is Guid) Id = (Guid)id;
            else Id = new Guid(id.ToString());
        }
        [Ignore]
        public override bool IdIsEmpty
        {
            get
            {
                return Id == default(Guid);
            }
        }
        public override string GetHeader()
        {
            return Id.ToString();
        }
        #endregion
        #region [Id] Id
        private Guid _Id_ = default(Guid);
        public Guid Id
        {
            get
            {
                return _Id_;
            }
            set
            {
                Model.UpdateProperty(this, "Id", _Id_, value, typeof(Guid), null, false);
                _Id_ = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// Базовый класс для всех объектов конфигурации
    /// </summary>
    public abstract class MEDMCfgObj : MEDMNameObj
    {
        #region Name
        private string _Name = default(string);
        public string Name
        {
            get
            {
                string[] n = Id.Split('.');
                return n[n.Length - 1];
            }
        }
        #endregion
    }
    /// <summary>
    /// Базовый класс для расширений
    /// </summary>
    /*
    public abstract class MEDMExt<T> where T: MObj
    {
        public static Type GetExtType()
        {
            return typeof(T);
        }
        public T Obj { get; set; }
    }
    */

    public class MDateTimeInterval : MObj
    {
        #region [Min] Минимальная дата
        private DateTime _Min_ = default(DateTime);
        public DateTime Min
        {
            get
            {
                return _Min_;
            }
            set
            {
                //Model.UpdateProperty(_ParentObj, _ParentName + ".min", _Min_, value, typeof(DateTime), null);
                _Min_ = value;
            }
        }
        #endregion
        #region [Max] Максимальная дата
        private DateTime _Max_ = default(DateTime);
        public DateTime Max
        {
            get
            {
                return _Max_;
            }
            set
            {
                //Model.UpdateProperty(_ParentObj, _ParentName + ".max", _Max_, value, typeof(DateTime), null);
                _Max_ = value;
            }
        }
        #endregion
        public override string ToString()
        {
            return $"{MFunc.DateToString(Min)};{MFunc.DateToString(Max)}";
        }
    }
    public class EDMClassAttribute : Attribute
    {
        public string Header { get; set; }
        public string ToolTip { get; set; }
    }
    public class EDMPropertyAttribute : Attribute
    {
        public string PropType { get; set; }
        public Type ItemType { get; set; }
        public string Header { get; set; }
        public string ToolTip { get; set; }
        public bool Virtual { get; set; }
    }
    /// <summary>
    /// Класс для поддерки свойств сущности или любого другого объекта
    /// </summary>
    public class MProps : Dictionary<object, string>
    {
        /// <summary>
        /// Констуктор
        /// </summary>
        /// <param name="owner" объект - сущность имеющий свойства></param>
        /// <param name="name" имя поля (строки) которое использунтся для хранения свойств></param>
        public MProps(object owner, string name)
        {

        }
    }
}

