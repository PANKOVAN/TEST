#if WPF 
using MFunction;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;
#else
using MFuncCoreLibrary;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.InteropServices;

namespace MEDMCoreLibrary
{
    /*
    #region Базовый класс для описателей 
    public class MEDMDef : INotifyPropertyChanged
    {
        public static DateTime NULLDateTime = new DateTime(1753, 1, 1, 12, 0, 0);
        public static DateTime NULLDateTime1 = new DateTime(1800, 1, 1, 0, 0, 0);
#if WPF
        #region Position
        private Point _Position = default(Point);
        public Point Position
        {
            get
            {
                return _Position;
            }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
        #endregion
        #region EditProperties
        private MEDMDefEditPropertyList _EditProperties = null;
        public MEDMDefEditPropertyList EditProperties
        {
            get
            {
                if (_EditProperties == null)
                {
                    _EditProperties = new MEDMDefEditPropertyList();
                    SetEditProperties(_EditProperties);
                }
                return _EditProperties;
            }
        }
        public virtual void SetEditProperties(MEDMDefEditPropertyList list)
        {
            list.Add(new MEDMDefEditProperty(this, "Name", "Код", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "Header", "Наименование", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ShortName", "Сокращение", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ToolTip", "Подсказка", MEDMDefEditType.REM));
            list.Add(new MEDMDefEditProperty(this, "Definition", "Описание", MEDMDefEditType.REM));
        }
        #endregion
        #region Clone
        public virtual MEDMDef Clone(MEDMDef parent)
        {
            Type t = GetType();
            MEDMDef clone = Activator.CreateInstance(t) as MEDMDef;
            foreach (PropertyInfo pi in t.GetRuntimeProperties())
            {
                if (pi.CanRead && pi.CanWrite)
                {
                    object v = pi.GetValue(this);
                    if (v != null) pi.SetValue(clone, v);
                }
            }
            return clone;
        }
        #endregion
#endif
        #region Id
        private Guid _Id = default(Guid);
        public Guid Id
        {
            get
            {
                if (_Id == default(Guid)) _Id = Guid.NewGuid();
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
            }
        }
        #endregion
        #region Name
        private string _Name = default(string);
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        #endregion
        #region Header
        private string _Header = default(string);
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                if (_Header != value)
                {
                    _Header = value;
                    OnPropertyChanged("Header");
                }
            }
        }
        #endregion
        #region ShortName
        private string _ShortName = default(string);
        public string ShortName
        {
            get
            {
                return _ShortName;
            }
            set
            {
                if (_ShortName != value)
                {
                    _ShortName = value;
                    OnPropertyChanged("ShortName");
                }
            }
        }
        #endregion
        #region ToolTip
        private string _ToolTip = default(string);
        public string ToolTip
        {
            get
            {
                return _ToolTip;
            }
            set
            {
                if (_ToolTip != value)
                {
                    _ToolTip = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }
        #endregion
        #region Definition
        private string _Definition = default(string);
        public string Definition
        {
            get
            {
                return _Definition;
            }
            set
            {
                if (_Definition != value)
                {
                    _Definition = value;
                    OnPropertyChanged("Definition");
                }
            }
        }
        #endregion
        #region Load/Save
        public virtual void LoadObjFromXml(XmlNode xnode)
        {
            foreach (PropertyInfo pi in GetType().GetRuntimeProperties())
            {
                if (pi.CanRead && pi.CanWrite)
                {
                    string v = XFunc.GetAttr(xnode, pi.Name.ToLower(), "");
                    if (v != "")
                    {
                        try
                        {
                            switch (pi.PropertyType.Name.ToLower())
                            {
                                case "string":
                                    pi.SetValue(this, v);
                                    break;
                                case "int":
                                case "int32":
                                    pi.SetValue(this, int.Parse(v));
                                    break;
                                case "short":
                                case "int16":
                                    pi.SetValue(this, short.Parse(v));
                                    break;
                                case "long":
                                case "int64":
                                    pi.SetValue(this, long.Parse(v));
                                    break;
                                case "boolean":
                                    pi.SetValue(this, v.ToLower() == "true" || v == "1");
                                    break;
#if WPF
                                case "point":
                                    try
                                    {
                                        pi.SetValue(this, Point.Parse(v));
                                    }
                                    catch
                                    {
                                        pi.SetValue(this, Point.Parse(v.Replace(';',',')));
                                    }
                                    break;
#endif
                                case "guid":
                                    pi.SetValue(this, new Guid(v));
                                    break;
                                    //default:
                                    //    throw new Exception($"LoadObjFromXml свойство {pi.Name} тип {pi.PropertyType} преобразование не определено");
                                    //    break;
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        public virtual void SaveObjToXml(XmlNode xnode)
        {
            foreach (PropertyInfo pi in GetType().GetRuntimeProperties())
            {
                if (pi.CanRead && pi.CanWrite)
                {
                    object v = pi.GetValue(this, null);
                    if (v != null)
                    {
                        try
                        {
                            switch (pi.PropertyType.Name.ToLower())
                            {
                                case "string":
                                case "int":
                                case "int32":
                                case "short":
                                case "int16":
                                case "long":
                                case "int64":
                                case "boolean":
                                case "point":
                                case "guid":
                                    XFunc.SetAttr(xnode, pi.Name.ToLower(), v.ToString());
                                    break;
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public override string ToString()
        {
            return $"[{Name}] {Header}";
        }
    }
    #endregion
    #region Описатель модели
    public class MEDMDefModel : MEDMDef
    {
        #region Описатель модели
        private static MEDMDefModel _MainDef_ = null;
        public static MEDMDefModel MainDef
        {
            get
            {
                if (_MainDef_ == null) _MainDef_ = new MEDMDefModel();
                return _MainDef_;
            }
        }
        #endregion
        #region Конструктор
        public MEDMDefModel()
        {
        }
        #endregion
        #region Preffix
        private string _Preffix = default(string);
        public string Preffix
        {
            get
            {
                return _Preffix;
            }
            set
            {
                if (_Preffix != value)
                {
                    _Preffix = value;
                    OnPropertyChanged("Preffix");
                }
            }
        }
        #endregion

        #region Список областей
        private MEDMDefList<MEDMDefRegion> _Regions_ = null;
        public MEDMDefList<MEDMDefRegion> Regions
        {
            get
            {
                if (_Regions_ == null) _Regions_ = new MEDMDefList<MEDMDefRegion>();
                return _Regions_;
            }
        }
        #endregion
        #region Load/Save
        public void Load(string path)
        {
            XmlDocument xdoc = new XmlDocument();
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                xdoc.Load(stream);
                XmlNode xmodel = xdoc.DocumentElement;
                if (xmodel != null && xmodel.Name == "model")
                {
#if WPF
                    FileName = path;
#endif
                    LoadObjFromXml(xmodel);
                    foreach (XmlNode xregion in xmodel.ChildNodes)
                    {
                        if (xregion.Name == "region")
                        {
                            MEDMDefRegion r = new MEDMDefRegion(this);
                            r.LoadObjFromXml(xregion);
                            this.Regions.Add(r);
                            foreach (XmlNode xclass in xregion.ChildNodes)
                            {
                                if (xclass.Name == "class")
                                {
                                    MEDMDefClass c = new MEDMDefClass(r);
                                    c.LoadObjFromXml(xclass);
                                    r.Classes.Add(c);
                                    foreach (XmlNode xproperty in xclass.ChildNodes)
                                    {
                                        if (xproperty.Name == "property")
                                        {
                                            MEDMDefProperty p = new MEDMDefProperty(c);
                                            p.LoadObjFromXml(xproperty);
                                            c.Properties.Add(p);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                stream.Dispose();
            }
#if WPF
            IsChanged = false;
#endif
        }
        public void Save(string path)
        {
#if WPF 
            FileName = path;
#endif
            XmlDocument xdoc = new XmlDocument();
#if WPF 
            XmlNode xmodel = XFunc.CreateElement(xdoc, "model");
#else
            XmlNode xmodel = XFunc.Append(xdoc, "model");
#endif
            SaveObjToXml(xmodel);
            foreach (MEDMDefRegion dr in Regions)
            {
#if WPF 
                XmlNode xregion = XFunc.CreateElement(xmodel, null, "region");
#else
                XmlNode xregion = XFunc.Append(xmodel, "region");
#endif
                dr.SaveObjToXml(xregion);
                foreach (MEDMDefClass dc in dr.Classes)
                {
#if WPF 
                    XmlNode xclass = XFunc.CreateElement(xregion, null, "class");
#else
                    XmlNode xclass = XFunc.Append(xregion, "class");
#endif
                    dc.SaveObjToXml(xclass);
                    foreach (MEDMDefProperty dp in dc.Properties)
                    {
#if WPF 
                        XmlNode xproperty = XFunc.CreateElement(xclass, null, "property");
#else
                        XmlNode xproperty = XFunc.Append(xclass,  "property");
#endif
                        dp.SaveObjToXml(xproperty);
                    }
                }
            }
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                xdoc.Save(stream);
                stream.Dispose();
            }
#if WPF
            IsChanged = false;
#endif
        }
        #endregion
        #region Find
        public MEDMDefClass Find(string name)
        {
            name = name.ToUpper();
            foreach (MEDMDefRegion dr in Regions)
            {
                foreach (MEDMDefClass ds in dr.Classes)
                {
                    if (ds.Name.ToUpper() == name) return ds;
                }
            }
            return null;
        }
        public MEDMDefClass Find(Type t)
        {
            string name = t.Name.ToUpper();
            foreach (MEDMDefRegion dr in Regions)
            {
                foreach (MEDMDefClass ds in dr.Classes)
                {
                    if ((Preffix + ds.Name).ToUpper() == name) return ds;
                }
            }
            return null;
        }
        public MEDMDefClass Find(Guid id)
        {
            foreach (MEDMDefRegion dr in Regions)
            {
                foreach (MEDMDefClass ds in dr.Classes)
                {
                    if (ds.Id == id) return ds;
                }
            }
            return null;
        }
        #endregion
        #region GetClassTypeByName
        public Type GetClassTypeByName(string name)
        {
            name = (Preffix + name).ToUpper();
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.Name.ToUpper() == name) return t;
            }
            return null;
        }
        #endregion
        #region GetClassTypeByClassName
        public Type GetClassTypeByClassName(string name)
        {
            name = name.ToUpper();
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.Name.ToUpper() == name) return t;
            }
            return null;
        }
        #endregion
        #region GetTableNameByType
        public string GetTableNameByType(Type t)
        {
            string name = t.Name;
            if (Preffix != "")
            {
                if (name.ToLower().StartsWith(Preffix.ToLower())) name = name.Substring(Preffix.Length);
                else throw new Exception($@"Для дипа ""{t}"" невозможно вычислить имя таблицы.");
            }
            return name;
        }
        #endregion
        #region AllClasses
        public List<MEDMDefClass> AllClasses
        {
            get
            {
                List<MEDMDefClass> l = new List<MEDMDefClass>();
                foreach (MEDMDefRegion dr in Regions)
                {
                    foreach (MEDMDefClass dc in dr.Classes)
                    {
                        l.Add(dc);
                    }
                }
                return l;
            }
        }
        #endregion
#if WPF

        #region IsChanged
        private bool _IsChanged = default(bool);
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
                    OnPropertyChanged("IsChanged");
                }
            }
        }
        #endregion
        #region FileName
        private string _FileName = default(string);
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }
        #endregion
        #region EditProperties
        public override void SetEditProperties(MEDMDefEditPropertyList list)
        {
            base.SetEditProperties(list);
            list.Add(new MEDMDefEditProperty(this, "Preffix", "Преффикс", MEDMDefEditType.STR));
        }
        #endregion
        #region IsChanged
        public override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            if (name != "IsChanged")
            {
                IsChanged = true;
            }
        }
        #endregion
        #region Clone
        public override MEDMDef Clone(MEDMDef parent)
        {
            throw new Exception("Для модели клонирование не работает");
        }
        #endregion
#endif
    }
    #endregion
    #region Описатель региона
    public class MEDMDefRegion : MEDMDef
    {
        #region Конструктор
        public MEDMDefModel Parent = null;
        public MEDMDefRegion(MEDMDefModel parent)
        {
            Parent = parent;
        }
        public MEDMDefRegion()
        {
        }
        #endregion
        #region Список классов
        private MEDMDefList<MEDMDefClass> _Classes_ = null;
        public MEDMDefList<MEDMDefClass> Classes
        {
            get
            {
                if (_Classes_ == null) _Classes_ = new MEDMDefList<MEDMDefClass>();
                return _Classes_;
            }
        }
        #endregion
#if WPF
        #region IsChanged
        public override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            if (Parent != null) Parent.IsChanged = true;
        }
        #endregion
        #region Clone
        public override MEDMDef Clone(MEDMDef parent)
        {
            MEDMDefRegion clone = base.Clone(parent) as MEDMDefRegion;
            clone.Parent = parent as MEDMDefModel;
            foreach (MEDMDefClass dc in Classes)
            {
                clone.Classes.Add(dc.Clone(clone) as MEDMDefClass);
            }
            return clone;
        }
        #endregion
#endif
    }
    #endregion
    #region Описатель класса
    public class MEDMDefClass : MEDMDef
    {
        public static string[] CLASSTYPELIST = { "edm", "table", "session" };
        public static string[] BASECLASSLIST = { "MEDMObj", "MObj" };
        #region Конструктор
        public MEDMDefRegion Parent = null;
        public MEDMDefClass(MEDMDefRegion parent)
        {
            Parent = parent;
        }
        public MEDMDefClass()
        {
        }
        #endregion
        #region ClassType
        private string _ClassType = default(string);
        public string ClassType
        {
            get
            {
                if (string.IsNullOrEmpty(_ClassType)) _ClassType = "edm";
                return _ClassType;
            }
            set
            {
                if (_ClassType != value)
                {
                    _ClassType = value;
                    OnPropertyChanged("ClassType");
                }
            }
        }
        #endregion
        #region BaseClass
        private string _BaseClass = default(string);
        public string BaseClass
        {
            get
            {
                if (string.IsNullOrEmpty(_BaseClass)) _BaseClass = "MEDMObj";
                return _BaseClass;
            }
            set
            {
                if (_BaseClass != value)
                {
                    _BaseClass = value;
                    OnPropertyChanged("BaseClass");
                }
            }
        }
        #endregion
        #region IsReadOnly
        private bool _IsReadOnly = false;
        public bool IsReadOnly
        {
            get
            {
                return _IsReadOnly;
            }
            set
            {
                if (_IsReadOnly != value)
                {
                    _IsReadOnly = value;
                    OnPropertyChanged("IsReadOnly");
                }
            }
        }
        #endregion
        #region IsMark
        private bool _IsMark = false;
        public bool IsMark
        {
            get
            {
                return _IsMark;
            }
            set
            {
                if (_IsMark != value)
                {
                    _IsMark = value;
                    OnPropertyChanged("IsMark");
                }
            }
        }
        #endregion
        #region IsJoin
        private bool _IsJoin = false;
        public bool IsJoin
        {
            get
            {
                return _IsJoin;
            }
            set
            {
                if (_IsJoin != value)
                {
                    _IsJoin = value;
                    OnPropertyChanged("IsJoin");
                }
            }
        }
        #endregion
        #region IsCopy
        private bool _IsCopy = false;
        public bool IsCopy
        {
            get
            {
                return _IsCopy;
            }
            set
            {
                if (_IsCopy != value)
                {
                    _IsCopy = value;
                    OnPropertyChanged("IsCopy");
                }
            }
        }
        #endregion
        #region IsMove
        private bool _IsMove = false;
        public bool IsMove
        {
            get
            {
                return _IsMove;
            }
            set
            {
                if (_IsMove != value)
                {
                    _IsMove = value;
                    OnPropertyChanged("IsMove");
                }
            }
        }
        #endregion
        #region IsLink
        private bool _IsLink = false;
        public bool IsLink
        {
            get
            {
                return _IsLink;
            }
            set
            {
                if (_IsLink != value)
                {
                    _IsLink = value;
                    OnPropertyChanged("IsLink");
                }
            }
        }
        #endregion
        #region IsClone
        private bool _IsClone = false;
        public bool IsClone
        {
            get
            {
                return _IsClone;
            }
            set
            {
                if (_IsClone != value)
                {
                    _IsClone = value;
                    OnPropertyChanged("IsClone");
                }
            }
        }
        #endregion
        #region CaptionJoin
        private string _CaptionJoin = "";
        public string CaptionJoin
        {
            get
            {
                return _CaptionJoin;
            }
            set
            {
                if (_CaptionJoin != value)
                {
                    _CaptionJoin = value;
                    OnPropertyChanged("CaptionJoin");
                }
            }
        }
        #endregion
        #region CaptionCopy
        private string _CaptionCopy = "";
        public string CaptionCopy
        {
            get
            {
                return _CaptionCopy;
            }
            set
            {
                if (_CaptionCopy != value)
                {
                    _CaptionCopy = value;
                    OnPropertyChanged("CaptionCopy");
                }
            }
        }
        #endregion
        #region CaptionMove
        private string _CaptionMove = "";
        public string CaptionMove
        {
            get
            {
                return _CaptionMove;
            }
            set
            {
                if (_CaptionMove != value)
                {
                    _CaptionMove = value;
                    OnPropertyChanged("CaptionMove");
                }
            }
        }
        #endregion
        #region CaptionLink
        private string _CaptionLink = "";
        public string CaptionLink
        {
            get
            {
                return _CaptionLink;
            }
            set
            {
                if (_CaptionLink != value)
                {
                    _CaptionLink = value;
                    OnPropertyChanged("CaptionLink");
                }
            }
        }
        #endregion
        #region CaptionClone
        private string _CaptionClone = "";
        public string CaptionClone
        {
            get
            {
                return _CaptionClone;
            }
            set
            {
                if (_CaptionClone != value)
                {
                    _CaptionClone = value;
                    OnPropertyChanged("CaptionClone");
                }
            }
        }
        #endregion
        #region ConfirmJoin
        private string _ConfirmJoin = "";
        public string ConfirmJoin
        {
            get
            {
                return _ConfirmJoin;
            }
            set
            {
                if (_ConfirmJoin != value)
                {
                    _ConfirmJoin = value;
                    OnPropertyChanged("ConfirmJoin");
                }
            }
        }
        #endregion
        #region ConfirmCopy
        private string _ConfirmCopy = "";
        public string ConfirmCopy
        {
            get
            {
                return _ConfirmCopy;
            }
            set
            {
                if (_ConfirmCopy != value)
                {
                    _ConfirmCopy = value;
                    OnPropertyChanged("ConfirmCopy");
                }
            }
        }
        #endregion
        #region ConfirmMove
        private string _ConfirmMove = "";
        public string ConfirmMove
        {
            get
            {
                return _ConfirmMove;
            }
            set
            {
                if (_ConfirmMove != value)
                {
                    _ConfirmMove = value;
                    OnPropertyChanged("ConfirmMove");
                }
            }
        }
        #endregion
        #region ConfirmLink
        private string _ConfirmLink = "";
        public string ConfirmLink
        {
            get
            {
                return _ConfirmLink;
            }
            set
            {
                if (_ConfirmLink != value)
                {
                    _ConfirmLink = value;
                    OnPropertyChanged("ConfirmLink");
                }
            }
        }
        #endregion
        #region ConfirmClone
        private string _ConfirmClone = "";
        public string ConfirmClone
        {
            get
            {
                return _ConfirmClone;
            }
            set
            {
                if (_ConfirmClone != value)
                {
                    _ConfirmClone = value;
                    OnPropertyChanged("ConfirmClone");
                }
            }
        }
        #endregion


        #region Список полей
        private MEDMDefList<MEDMDefProperty> _Properties_ = null;
        public MEDMDefList<MEDMDefProperty> Properties
        {
            get
            {
                if (_Properties_ == null) _Properties_ = new MEDMDefList<MEDMDefProperty>();
                return _Properties_;
            }
        }
        #endregion
        #region GetClassType
        private Type _GetClassType = null;
        public Type GetClassType()
        {
            if (_GetClassType == null) _GetClassType = Parent.Parent.GetClassTypeByName(Name);
            return _GetClassType;
        }
        #endregion
        #region GetIdProperty
        public MEDMDefProperty GetIdPropery()
        {
            foreach (MEDMDefProperty dp in Properties)
            {
                if (dp.IsPrimary) return dp;
            }
            return null;
        }
        #endregion
#if WPF
        #region EditProperties

        public override void SetEditProperties(MEDMDefEditPropertyList list)
        {
            List<MEDMDefClass> cl = MEDMDefModel.MainDef.AllClasses;
            string[] bl = new string[BASECLASSLIST.Length+cl.Count];
            int i=0;
            foreach(string n in BASECLASSLIST) 
            {
                bl[i]=n;
                i++;
            }
            foreach(MEDMDefClass c in cl) 
            {
                bl[i]=c.Name;
                i++;
            }

            base.SetEditProperties(list);
            list.Add(new MEDMDefEditProperty(this, "ClassType", "Тип", MEDMDefEditType.ENUM, CLASSTYPELIST));
            list.Add(new MEDMDefEditProperty(this, "BaseClass", "Базовый класс", MEDMDefEditType.ENUM, bl));
            list.Add(new MEDMDefEditProperty(this, "IsReadOnly", "Только чтение", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "IsMark", "Команда Mark", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "IsCopy", "Команда Copy", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "ConfirmCopy", "Запрос для Copy", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ConfirmCopy", "Запрос для Copy", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "IsMove", "Команда Move", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "CaptionMove", "Заголовок для Move", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ConfirmMove", "Запрос для Move", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "IsLink", "Команда Link", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "CaptionLink", "Заголовок для Link", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ConfirmLink", "Запрос для Link", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "IsClone", "Команда Clone", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "CaptionClone", "Заголовок для Clone", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ConfirmClone", "Запрос для Clone", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "IsJoin", "Команда Join", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "CaptionJoin", "Заголовок для Join", MEDMDefEditType.STR));
            list.Add(new MEDMDefEditProperty(this, "ConfirmJoin", "Запрос для Join", MEDMDefEditType.STR));
        }
        #endregion
        #region IsChanged
        public override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            if (Parent != null && Parent.Parent != null) Parent.Parent.IsChanged = true;
        }
        #endregion
        #region Clone
        public override MEDMDef Clone(MEDMDef parent)
        {
            MEDMDefClass clone = base.Clone(parent) as MEDMDefClass;
            clone.Parent = parent as MEDMDefRegion;
            clone.Position = new Point(Position.X + 20, Position.Y + 20);
            foreach (MEDMDefProperty dp in Properties)
            {
                clone.Properties.Add(dp.Clone(clone) as MEDMDefProperty);
            }
            return clone;
        }
        #endregion
#endif
    }
    #endregion
    #region Описатель свойства
    public class MEDMDefProperty : MEDMDef
    {
        public static string[] DATATYPELIST = { "bool", "guid", "int", "long", "short", "byte", "double", "money", "date", "string", "binary", "image", "timestamp", "interval", "doubleinterval", "moneyinterval", "dateinterval" };
        public static string[] REFTYPELIST = { "none", "restrict", "cascade" };
        public static string[] PROPTYPELIST = { "getset", "get", "set" };
        public static string[] PROPLENGTHLIST = { "none", "width", "header", "data", "fill" };
        public static string[] PROPCSSLIST = { "none", "text-left", "text-center", "text-right", "text-danger" };
        public static string[] DEFVALUELIST = { "", "default", "true", "false", "min", "max", "0", "-1", "1", "empty", "null", "currentdate", "newguid" };
        public static string[] DATAMODELIST = { "" };
        public static string[] DATASTYLELIST = { "none", "hide", "memo", "check", "radio", "reflist", "reftree", "list" };
        #region Конструктор
        public MEDMDefClass Parent = null;
        public MEDMDefProperty(MEDMDefClass parent)
        {
            Parent = parent;
        }
        public MEDMDefProperty()
        {
        }
        #endregion
        #region Первичный ключ
        public bool IsPrimary { get; set; }
        #endregion
        #region Видимость
        public bool IsVisible
        {
            get
            {
                return !IsPrimary && DataStyle.ToLower() != "hide" && Name.ToLower() != "id";
            }
        }
        #endregion
        #region Интервал
        public bool IsInterval
        {
            get
            {
                return DataType.ToLower().EndsWith("interval");
            }
        }
        #endregion
        #region Объект
        public bool IsObj
        {
            get
            {
                return IsInterval;
            }
        }
        #endregion
        #region Тип данных
        private string _DataType = default(string);
        public string DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                if (_DataType != value)
                {
                    _DataType = value;
                    OnPropertyChanged("DataType");
                    OnPropertyChanged("DataTypeBackground");
                }
            }
        }
        public object GetDefaultValue()
        {
            switch (DataType.ToLower())
            {
                case "bool": return default(bool);
                case "guid": return default(Guid);
                case "int": return default(int);
                case "long": return default(long);
                case "short": return default(short);
                case "byte": return default(byte);
                case "double": return default(double);
                case "money": return default(decimal);
                case "date": return default(DateTime);
                case "string": return "";
                case "timestamp": return default(long);
            }
            return null;
        }
        public Type GetPropertyType()
        {
            switch (DataType.ToLower())
            {
                case "bool": return typeof(bool);
                case "guid": return typeof(Guid);
                case "int": return typeof(int);
                case "long": return typeof(long);
                case "short": return typeof(short);
                case "byte": return typeof(byte);
                case "double": return typeof(double);
                case "money": return typeof(decimal);
                case "date": return typeof(DateTime);
                case "string": return typeof(string);
                case "timestamp": return typeof(long);
            }
            return null;
        }
        public object ConvertToPropertyType(object value)
        {
            if (value == null) return GetDefaultValue();

            Type pt = GetPropertyType();
            if (pt == typeof(bool))
            {
                return (value.ToString() == "1" || value.ToString().ToLower() == "true");
            }
            else if (pt == typeof(short))
            {
                if (!(value is short)) return Convert.ToInt16(value);
            }
            else if (pt == typeof(int))
            {
                if (!(value is int)) return Convert.ToInt32(value);
            }
            else if (pt == typeof(long))
            {
                if (!(value is long)) return Convert.ToInt64(value);
            }
            else if (pt == typeof(byte))
            {
                if (!(value is byte)) return Convert.ToByte(value);
            }
            else if (pt == typeof(double))
            {
                if (!(value is double)) return Convert.ToDouble(value);
            }
            else if (pt == typeof(decimal))
            {
                if (!(value is decimal)) return Convert.ToDecimal(value);
            }
            else if (pt == typeof(DateTime))
            {
                if (!(value is DateTime)) value = Convert.ToDateTime(value);
                DateTime d = (DateTime)value;
                if (d < NULLDateTime1) d = NULLDateTime;
                return d;
            }
            else if (pt == typeof(string))
            {
                if (!(value is string)) return value.ToString();
            }
            else if (pt == typeof(Guid))
            {
                if (!(value is Guid)) return new Guid(value.ToString());
            }

            return value;
        }
        public string GetDataTypeFor740()
        {
            if (this.DataStyle == "radio" || this.DataStyle == "check" || this.DataStyle == "reflist" || this.DataStyle == "reftree") return this.DataStyle;

            switch (DataType.ToLower())
            {
                case "bool":  return "check";
                case "guid": return "string";
                case "int": return "num";
                case "intinterval": return "num";
                case "long": return "num";
                case "short": return "num";
                case "byte": return "num";
                case "double": return "num";
                case "doubleinterval": return "num";
                case "money": return "num";
                case "moneyinterval": return "num";
                case "date": return "date";
                case "dateinterval": return "date";
                case "string":
                    {
                        if (this.DataStyle == "memo") return "memo";
                        return "string";
                    }
                case "timestamp": return "string";
            }
            return "???";
        }
        #endregion
        #region Длина данных
        private int _DataLength = default(int);
        public int DataLength
        {
            get
            {
                return _DataLength;
            }
            set
            {
                if (_DataLength != value)
                {
                    _DataLength = value;
                    OnPropertyChanged("DataLength");
                }
            }
        }
        #endregion
        #region Значение по умолчанию
        private string _DefValue = default(string);
        public string DefValue
        {
            get
            {
                if (string.IsNullOrEmpty(_DefValue)) _DefValue = "default";
                return _DefValue;
            }
            set
            {
                if (_DefValue != value)
                {
                    _DefValue = value;
                    OnPropertyChanged("DefValue");
                }
            }
        }
        #endregion
        #region Тип ссылки
        private string _PropType = default(string);
        public string PropType
        {
            get
            {
                if (string.IsNullOrEmpty(_PropType)) _PropType = "getset";
                return _PropType;
            }
            set
            {
                if (_PropType != value)
                {
                    _PropType = value;
                    OnPropertyChanged("PropType");
                }
            }
        }
        #endregion
        #region Ref
        private Guid _RefClassId = default(Guid);
        public Guid RefClassId
        {
            get
            {
                return _RefClassId;
            }
            set
            {
                if (_RefClassId != value)
                {
                    _RefClassId = value;
                    OnPropertyChanged("RefClass");
                    OnPropertyChanged("RefClassId");
                }
            }
        }
        public MEDMDefClass RefClass
        {
            get
            {
                return Parent.Parent.Parent.Find(RefClassId);
            }
            set
            {
                if (value == null) RefClassId = default(Guid);
                else RefClassId = value.Id;
            }
        }
        #endregion
        #region Тип ссылки
        private string _RefType = default(string);
        public string RefType
        {
            get
            {
                if (string.IsNullOrEmpty(_RefType)) _RefType = "none";
                return _RefType;
            }
            set
            {
                if (_RefType != value)
                {
                    _RefType = value;
                    OnPropertyChanged("RefType");
                }
            }
        }
        #endregion
        #region Режим
        private string _DataMode = default(string);
        public string DataMode
        {
            get
            {
                if (string.IsNullOrEmpty(_DataMode)) _DataMode = "none";
                return _DataMode;
            }
            set
            {
                if (_DataMode != value)
                {
                    _DataMode = value;
                    OnPropertyChanged("DataMode");
                }
            }
        }
        #endregion
        #region Тип ссылки
        private string _LengthType = default(string);
        public string LengthType
        {
            get
            {
                if (string.IsNullOrEmpty(_LengthType)) _LengthType = "none";
                return _LengthType;
            }
            set
            {
                if (_LengthType != value)
                {
                    _LengthType = value;
                    OnPropertyChanged("LengthType");
                }
            }
        }
        #endregion
        #region Длина поля для отображения
        private int _Length = default(int);
        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (_Length != value)
                {
                    _Length = value;
                    OnPropertyChanged("Length");
                }
            }
        }
        #endregion
        #region Знаков после точки
        private int _Dec = default(int);
        public int Dec
        {
            get
            {
                return _Dec;
            }
            set
            {
                if (_Dec != value)
                {
                    _Dec = value;
                    OnPropertyChanged("Dec");
                }
            }
        }
        #endregion
        #region Стиль
        private string _DataStyle = default(string);
        public string DataStyle
        {
            get
            {
                if (string.IsNullOrEmpty(_DataStyle)) _DataStyle = "none";
                return _DataStyle;
            }
            set
            {
                if (_DataStyle != value)
                {
                    _DataStyle = value;
                    OnPropertyChanged("DataStyle");
                }
            }
        }
        #endregion
        #region Список значений
        private string _Items = default(string);
        public string Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                    OnPropertyChanged("Items");
                }
            }
        }
        #endregion
        #region Разрешенасортировка на клиенте
        public bool IsClientSort { get; set; }
        #endregion
        #region CSS
        private string _CSS = default(string);
        public string CSS
        {
            get
            {
                return _CSS;
            }
            set
            {
                if (_CSS != value)
                {
                    _CSS = value;
                    OnPropertyChanged("CSS");
                }
            }
        }
        #endregion

        public string GetRefName()
        {
            //string rdatatype = Model.Preffix + dp.RefClass.Name;
            //string rdatatable = dp.RefClass.Name;
            //string rid = GetIdName(dp.RefClass);
            string r = "";
            if (RefClassId != default(Guid))
            {
                string rid = RefClass.GetIdPropery().Name;
                r = Name;
                if (Name.ToLower().EndsWith(rid.ToLower())) r = r.Substring(0, r.Length - rid.Length);
            }
            return r;
        }
#if WPF
        #region EditProperties
        public override void SetEditProperties(MEDMDefEditPropertyList list)
        {
            base.SetEditProperties(list);
            list.Add(new MEDMDefEditProperty(this, "IsPrimary", "Ключ", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "DataType", "Тип", MEDMDefEditType.ENUM, DATATYPELIST));
            list.Add(new MEDMDefEditProperty(this, "DataLength", "Длина данных", MEDMDefEditType.INT));
            list.Add(new MEDMDefEditProperty(this, "PropType", "Тип свойства", MEDMDefEditType.ENUM, PROPTYPELIST));
            list.Add(new MEDMDefEditProperty(this, "DefValue", "По умолчанию", MEDMDefEditType.ENUM, DEFVALUELIST));
            list.Add(new MEDMDefEditProperty(this, "RefClass", "Ссылка", MEDMDefEditType.REF));
            list.Add(new MEDMDefEditProperty(this, "RefType", "Тип ссылки", MEDMDefEditType.ENUM, REFTYPELIST));
            list.Add(new MEDMDefEditProperty(this, "DataMode", "Режим", MEDMDefEditType.ENUM, DATAMODELIST));
            list.Add(new MEDMDefEditProperty(this, "DataStyle", "Стиль", MEDMDefEditType.ENUM, DATASTYLELIST));
            list.Add(new MEDMDefEditProperty(this, "LengthType", "Ширина (тип)", MEDMDefEditType.ENUM, PROPLENGTHLIST));
            list.Add(new MEDMDefEditProperty(this, "Length", "Ширина (значение)", MEDMDefEditType.INT));
            list.Add(new MEDMDefEditProperty(this, "Dec", "После точки", MEDMDefEditType.INT));
            list.Add(new MEDMDefEditProperty(this, "Items", "Список значений", MEDMDefEditType.REM));
            list.Add(new MEDMDefEditProperty(this, "IsClientSort", "Сортировка на клиенте", MEDMDefEditType.CHK));
            list.Add(new MEDMDefEditProperty(this, "CSS", "CSS", MEDMDefEditType.ENUM, PROPCSSLIST));
        }
        #endregion
        #region IsChanged
        public override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            if (Parent != null && Parent.Parent != null && Parent.Parent.Parent != null) Parent.Parent.Parent.IsChanged = true;
        }
        #endregion
        #region Clone
        public override MEDMDef Clone(MEDMDef parent)
        {
            MEDMDefProperty clone = base.Clone(parent) as MEDMDefProperty;
            clone.Parent = parent as MEDMDefClass;
            return clone;
        }
        #endregion
        public Brush DataTypeBackground
        {
            get
            {
                if (DataType != null)
                {
                    switch (DataType.ToLower())
                    {
                        case "bool": return Brushes.DarkSalmon;
                        case "guid": return Brushes.DarkOrange;
                        case "int": return Brushes.DarkBlue;
                        case "long": return Brushes.DarkBlue;
                        case "short": return Brushes.DarkBlue;
                        case "byte": return Brushes.DarkBlue;
                        case "double": return Brushes.DarkKhaki;
                        case "money": return Brushes.DarkKhaki;
                        case "date": return Brushes.DarkMagenta;
                        case "string": return Brushes.DarkGreen;
                        case "image": return Brushes.DarkOrchid;
                        case "timestamp": return Brushes.DarkTurquoise;
                    }
                }
                return Brushes.Transparent;
            }
        }
#endif
    }
    #endregion
    #region Список описателей
#if WPF
    public class MEDMDefList<T> : ObservableCollection<T> where T : MEDMDef
#else
    public class MEDMDefList<T> : List<T> where T : MEDMDef
#endif
    {
        public T this[string index]
        {
            get
            {
                index = index.ToUpper();
                foreach (T def in this)
                {
                    if (def.Name.ToUpper() == index) return def;
                }
                return null;
            }
        }
        public T this[Guid index]
        {
            get
            {
                foreach (T def in this)
                {
                    if (def.Id == index) return def;
                }
                return null;
            }
        }
#if WPF
        public bool CanMoveUp(T def)
        {
            return (this.IndexOf(def)) > 0;
        }
        public bool CanMoveDown(T def)
        {
            return (this.IndexOf(def)) < this.Count - 1;
        }
        public void MoveUp(T def, bool gofirst)
        {
            int i = IndexOf(def);
            if (i > 0)
            {
                Move(i, gofirst ? 0 : i - 1);
            }
        }
        public void MoveDown(T def, bool golast)
        {
            int i = IndexOf(def);
            if (i >= 0 && i < Count - 1)
            {
                Move(i, golast ? Count - 1 : i + 1);
            }
        }
#endif
    }
    #endregion
#if WPF
    #region Классы для правки свойств
    public enum MEDMDefEditType { STR, INT, REM, CHK, ENUM, REF }
    public class MEDMDefEditProperty
    {
        public MEDMDef Def { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public string[] Items { get; set; }
        private PropertyInfo _PI = null;
        private PropertyInfo GetPropertyInfo(string name)
        {
            if (_PI != null) return _PI;
            name = name.ToLower();
            foreach (PropertyInfo pi in Def.GetType().GetRuntimeProperties())
            {
                if (name == pi.Name.ToLower())
                {
                    _PI = pi;
                    return _PI;
                }
            }
            throw new Exception($"Свойство {name} в {Def.GetType()} не найдено.");
        }
        public object Value
        {
            get
            {
                if (Def != null)
                {
                    return GetPropertyInfo(Name).GetValue(Def);
                }
                return null;
            }
            set
            {
                if (Def != null)
                {
                    PropertyInfo pi = GetPropertyInfo(Name);
                    try
                    {
                        if (pi.PropertyType == typeof(string))
                        {
                            pi.SetValue(Def, value);
                        }
                        else if (pi.PropertyType == typeof(int))
                        {
                            pi.SetValue(Def, Convert.ToInt32(value));
                        }
                        else if (pi.PropertyType == typeof(short))
                        {
                            pi.SetValue(Def, Convert.ToInt16(value));
                        }
                        else if (pi.PropertyType == typeof(long))
                        {
                            pi.SetValue(Def, Convert.ToInt64(value));
                        }
                        else if (pi.PropertyType == typeof(bool))
                        {
                            pi.SetValue(Def, Convert.ToBoolean(value));
                        }
                        else if (pi.PropertyType == typeof(double))
                        {
                            pi.SetValue(Def, Convert.ToDouble(value));
                        }
                        else if (MFunc.IsBaseType(pi.PropertyType, typeof(MEDMDef)))
                        {
                            pi.SetValue(Def, value);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        public MEDMDefEditType EditType { get; set; }
        public MEDMDefEditProperty(MEDMCoreLibrary.MEDMDef def, string name, string header, MEDMDefEditType type)
        {
            Def = def;
            Name = name;
            Header = header;
            EditType = type;
        }
        public MEDMDefEditProperty(MEDMCoreLibrary.MEDMDef def, string name, string header, MEDMDefEditType type, string[] items)
        {
            Def = def;
            Name = name;
            Header = header;
            EditType = type;
            Items = items;
        }
    }
    public class MEDMDefEditPropertyList : List<MEDMDefEditProperty>
    {

    }
    #endregion
#endif

#if WPF
    #region Копии сложных классов
    public class MDateTimeInterval 
    {
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }
    }
    #endregion
#endif
*/
}
