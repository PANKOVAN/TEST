using MFuncCoreLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
/// <summary>
/// Классы генератора модели
/// </summary>
namespace MEDMCoreLibrary
{
    public class MEDMGen : MEDM
    {
        public MEDMGen()
        {
            EDMProjectDic.FindObj(null);
            EDMClassDic.FindObj(null);
            EDMPropDic.FindObj(null);
        }
        #region EDMProject Класс
        private MEDMObjectDic _EDMProjectDic = null;
        public MEDMObjectDic EDMProjectDic
        {
            get
            {
                if (_EDMProjectDic == null) _EDMProjectDic = new MEDMObjectDic(this, typeof(MEDMProject));
                if (!MainDic.ContainsKey(typeof(MEDMProject))) MainDic[typeof(MEDMProject)] = _EDMProjectDic;
                return _EDMProjectDic;
            }
        }
        #endregion
        #region EDMClass Класс
        private MEDMObjectDic _EDMClassDic = null;
        public MEDMObjectDic EDMClassDic
        {
            get
            {
                if (_EDMClassDic == null) _EDMClassDic = new MEDMObjectDic(this, typeof(MEDMClass));
                if (!MainDic.ContainsKey(typeof(MEDMClass))) MainDic[typeof(MEDMClass)] = _EDMClassDic;
                return _EDMClassDic;
            }
        }
        #endregion
        #region EDMProp Свойство
        private MEDMObjectDic _EDMPropDic = null;
        public MEDMObjectDic EDMPropDic
        {
            get
            {
                if (_EDMPropDic == null) _EDMPropDic = new MEDMObjectDic(this, typeof(MEDMProp));
                if (!MainDic.ContainsKey(typeof(MEDMProp))) MainDic[typeof(MEDMProp)] = _EDMPropDic;
                return _EDMPropDic;
            }
        }
        #endregion

        #region Console
        public string __
        {
            set
            {
                Console.ForegroundColor = ConsoleColor.White;
                switch (value.Split(':')[0].ToLower())
                {
                    case "error":
                        MainTrace.Add(TraceType.Error, value);
                        return;
                    case "medmproject":
                        value = "".PadLeft(4) + value;
                        break;
                    case "medmclass":
                        value = "".PadLeft(8) + value;
                        break;
                    case "medmprop":
                        value = "".PadLeft(12) + value;
                        break;
                    default: break;
                }
                MainTrace.Add(TraceType.Info, value);
            }
        }
        public int Errors { get; set; }
        #endregion
        #region Load
        public void Load(XmlDocument xdoc)
        {
            LoadStandart();

            foreach (XmlNode xproject in xdoc.SelectNodes("descendant::project"))
            {
                MEDMProject pr = Load<MEDMProject>(xproject);
                foreach (XmlNode xclass in xproject.SelectNodes("descendant::class"))
                {
                    MEDMClass c = Load<MEDMClass>(xclass);
                    pr.Classes.Add(c);
                    foreach (XmlNode xprop in xclass.SelectNodes("descendant::prop"))
                    {
                        MEDMProp p = Load<MEDMProp>(xprop, c.Name);
                        c.Props.Add(p);
                    }
                }
            }
        }
        public T Load<T>(XmlNode xnode, string pref = "") where T : MEDMObj
        {
            T p = null;
            string n = XFunc.GetAttr(xnode, "name", "");
            if (n != "")
            {
                __ = $"{typeof(T).Name}: {n}";
                if (pref != "") n = $"{pref}.{n}";
                p = MainDic.GetObj<T>(n);
                try
                {
                    p.SetValues(xnode);
                }
                catch (Exception e)
                {
                    __ = $"error: {e}";
                }
            }
            else
            {
                __ = $"error: Имя {typeof(T).Name} не задано.";
            }
            return p;
        }
        public void LoadStandart()
        {
            MEDMClass c = null;
            MEDMProp p = null;

            //----------------------------------------------------
            c = MainDic.NewObj<MEDMClass>("MEDMIdObj");
            c.Header = "Базовый класс для конфигураций";

            p = MainDic.NewObj<MEDMProp>("MEDMIdObj.Id");
            p.Header = "Id";
            c.Props.Add(p);

            //----------------------------------------------------
            c = MainDic.NewObj<MEDMClass>("MEDMNameObj");
            c.Header = "Базовый класс для объектов с автоинкрементным Id";

            p = MainDic.NewObj<MEDMProp>("MEDMNameObj.Id");
            p.Header = "Id";
            c.Props.Add(p);

            //----------------------------------------------------
            c = MainDic.NewObj<MEDMClass>("MEDMGuidObj");
            c.Header = "Базовый класс для объектов с Guid";

            p = MainDic.NewObj<MEDMProp>("MEDMGuidObj.Id");
            p.Header = "Id";
            c.Props.Add(p);

            //----------------------------------------------------
            c = MainDic.NewObj<MEDMClass>("MEDMCfgObj");
            c.Header = "Базовый класс для конфигураций";
            c.BaseId = "MEDMNameObj";

            p = MainDic.NewObj<MEDMProp>("MEDMCfgObj.Id");
            p.Header = "Наименование";
            c.Props.Add(p);
        }
        #endregion
        #region Result
        private StringBuilder _Result = null;
        public StringBuilder Result
        {
            get
            {
                if (_Result == null) _Result = new StringBuilder();
                return _Result;
            }
        }
        #endregion
        #region Вывод
        private static char[] _d = { ' ', '\r', '\n' };
        public string _
        {
            set
            {
                if (value != null)
                {
                    if (value == "}") Indent--;
                    if (value.StartsWith("\r") || value.StartsWith("\n"))
                    {
                        int i = -1;
                        foreach (string v in value.Split('\r'))
                        {
                            if (!string.IsNullOrEmpty(v))
                            {
                                if (i == -1)
                                {
                                    i = 0;
                                    while (i < v.Length && (v[i] == '\n' || v[i] == ' '))
                                    {
                                        i++;
                                    }
                                }
                                Result.Append("".PadLeft(4 * Indent) + v.Substring(i) + "\r\n");
                            }
                        }
                    }
                    else
                    {
                        foreach (string v in value.Split('\r'))
                        {
                            if (!string.IsNullOrEmpty(v))
                            {
                                Result.Append("".PadLeft(4 * Indent) + v.Trim(_d) + "\r\n");
                            }
                        }
                    }
                    if (value == "{") Indent++;
                }
            }
        }
        #endregion
        #region Indent
        private int _Indent = 0;
        public int Indent
        {
            get
            {
                if (_Indent >= 0) return _Indent;
                return 0;
            }
            set
            {
                _Indent = value;
            }
        }
        #endregion
        #region GenClasses
        // Генерация классов

        // Генерация модели
        public string GenClasses(MEDMProject mr)
        {
            Result.Clear();
            _ = $"//--------------------------------------------------------------------------------";
            _ = $"// MBuilder3X Генератор модели данных (классы)                ({DateTime.Now})";
            _ = $"//--------------------------------------------------------------------------------";

            _ = $"using System;";
            _ = $"using MEDMCoreLibrary;";
            _ = $"using System.Collections.Generic;";

            _ = $"#region [{mr.Name}] {mr.Header}";
            _ = $"namespace Models.{mr.Name}";
            _ = $"{{";
            _ = $"public partial class {mr.Name} : MEDMSql";
            _ = $"{{";
            GenClassesConstructor(mr);
            GenClassesDic(mr);
            _ = $"}}";
            foreach (MEDMClass mc in mr.Classes)
            {
                GenClassesClass(mc);
            }
            _ = $"}}";
            _ = $"#endregion";

            return Result.ToString();
        }
        // Конструктор модели
        public string GenClassesConstructor(MEDMProject mr)
        {
            _ = $"#region Конструктор";
            _ = $"public {mr.Name}()";
            _ = $"{{";
            foreach (MEDMClass mc in mr.Classes)
            {
                if (mc.Type == MEDMClassType.MODELCLASS || mc.Type == MEDMClassType.CLASS || mc.Type == MEDMClassType.CFG)
                {
                    _ = $"{mc.Name}Dic.FindObj(null);";
                }
            }
            _ = $"}}";
            _ = $"#endregion";

            return Result.ToString();
        }
        // Словарь модели
        public string GenClassesDic(MEDMProject mr)
        {
            _ = $"#region Словарь";
            foreach (MEDMClass mc in mr.Classes)
            {
                if (mc.Type == MEDMClassType.MODELCLASS || mc.Type == MEDMClassType.CLASS || mc.Type == MEDMClassType.CFG)
                {
                    _ = $"#region [{mc.Name}] {mc.Header}";
                    if (mc.Type == MEDMClassType.CFG)
                        _ = $"private static MEDMObjectDic _{mc.Name}Dic = null;";
                    else
                        _ = $"private MEDMObjectDic _{mc.Name}Dic = null;";
                    _ = $"public MEDMObjectDic {mc.Name}Dic";
                    _ = $"{{";
                    _ = $"get";
                    _ = $"{{";
                    _ = $"if (_{mc.Name}Dic == null) _{mc.Name}Dic = new MEDMObjectDic(this, typeof({mc.Name}));";
                    _ = $"if (!MainDic.ContainsKey(typeof({mc.Name}))) MainDic[typeof({mc.Name})] = _{mc.Name}Dic;";
                    _ = $"return _{mc.Name}Dic;";
                    _ = $"}}";
                    _ = $"}}";
                    _ = $"#endregion";
                }
            }
            _ = $"#endregion";

            return Result.ToString();
        }
        // Класс модели
        public string GenClassesClass(MEDMClass mc)
        {
            _ = $@"#region [{mc.Name}] {mc.Header}";
            if (mc.Type == MEDMClassType.ENUM)
            {
                _ = $@"public enum {mc.Name}";
                _ = $"{{";
                _ = $@"NONE=0    // не задано";
                foreach (MEDMProp mp in mc.Props)
                {
                    _ = $@",{mp.Name}    // {mp.Header}";
                }
                _ = $"}}";
            }
            else
            {
                _ = $@"[DbTable(Type = ""{mc.Type}"")]";
                _ = $@"[EDMClass(Header = ""{mc.Header}"")]";
                _ = $@"public partial class {mc.Name}: {mc.Base.Name}";
                _ = $"{{";
                foreach (MEDMProp mp in mc.Props)
                {
                    GenClassesProp(mp, mc);
                }
                if (!string.IsNullOrEmpty(mc.ToString))
                {
                    _ = $@"public override string ToString()";
                    _ = $@"{{";
                    _ = $@"return $""{mc.ToString}"";";
                    _ = $@"}}";
                }
                _ = $"}}";
            }
            _ = $"#endregion";

            return Result.ToString();
        }
        // Свойство класса
        public string GenClassesProp(MEDMProp mp, MEDMClass mc)
        {
            _ = $"#region [{mp.Name}] {mp.Header}";
            switch (mp.Type)
            {
                // Не задано
                case MEDMPropType.NONE:
                    {
                        break;
                    }
                // Ссылка или конфигурация
                case MEDMPropType.CFG:
                case MEDMPropType.REF:
                    {
                        if (mp.Ref == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - ссылка не задан или задан неправильно ({mp.RefId})";
                            break;
                        }
                        if (mp.Ref.IdType == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - тип ссылки определить невозможно ({mp.RefId})";
                            break;
                        }
                        _ = $@"
                            private {mp.Ref.IdType.Name} _{mp.Name}Id = default({mp.Ref.IdType.Name});
                            [DbColumn(Type = ""{mp.Ref.IdColumnType}"", Def =""{mp.Ref.IdColumnDef}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            [EDMProperty(PropType=""{mp.Type}"", ItemType=typeof({mp.Ref.IdType.Name}), Header=""{mp.Header} (Id)"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public {mp.Ref.IdType.Name} {mp.Name}Id
                            {{
                                get 
                                {{
                                    return _{mp.Name}Id;
                                }}
                                set 
                                {{
                                    Model.UpdateProperty(this, ""{mp.Name}Id"", _{mp.Name}Id, value, typeof({mp.Ref.IdType.Name}), typeof({mp.Ref.Name}), {mp.Virtual.ToString().ToLower()});
                                    _{mp.Name}Id=value;
                                }}
                            }}";


                        _ = $@"
                            private {mp.Ref.Name} _{mp.Name} = default({mp.Ref.Name});
                            [EDMProperty(PropType=""{mp.Type}"", ItemType=typeof({mp.Ref.Name}), Header=""{mp.Header}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public {mp.Ref.Name} {mp.Name}
                            {{
                                get 
                                {{
                                    if (_{mp.Name}==null && _{mp.Name}Id!=default({mp.Ref.IdType.Name}))
                                    {{
                                        Model.UpdateFutureRef(typeof({mp.Ref.Name}), ""{mp.Ref.Name}"", ""Id"", ""{mp.Ref.IdType}"");
                                        _{mp.Name} = Model.MainDic.GetObj<{mp.Ref.Name}>({mp.Name}Id);
                                    }}
                                    return _{mp.Name};
                                }}
                            }}
                            //public bool ShouldSerialize{mp.Ref.Name}()
                            //{{
                            //    return false;
                            //}}
                            ";
                        break;
                    }
                // Свойства
                case MEDMPropType.PROPS:
                    {
                        _ = $@"
                            private string _S{mp.Name} = default(string);
                            [DbColumn(Type = ""nvarchar(MAX)"", Def =""''"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            [EDMProperty(PropType=""PROPS"", ItemType=typeof(String), Header=""{mp.Header}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public string S{mp.Name}
                            {{
                                get 
                                {{
                                    return _S{mp.Name};
                                }}
                                set 
                                {{
                                    Model.UpdateProperty(this, ""S{mp.Name}"", _S{mp.Name}, value, typeof(string), null, {mp.Virtual.ToString().ToLower()});
                                    _S{mp.Name}=value;
                                }}
                            }}
                            private MProps _{mp.Name} = null;
                            [Ignore]
                            {mp.JsonIgnoreAttribute}
                            public MProps {mp.Name}
                            {{
                                get 
                                {{
                                    if (_{mp.Name}==null) _{mp.Name}= new MProps(this, ""S{mp.Name}"");    
                                    return _{mp.Name};
                                }}
                            }}";
                        break;
                    }
                // Список
                case MEDMPropType.LIST:
                    {
                        if (mp.Ref == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - ссылка не задан или задан неправильно ({mp.RefId})";
                            break;
                        }
                        if (mp.Ref.IdType == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - тип ссылки определить невозможно ({mp.RefId})";
                            break;
                        }
                        _ = $@"
                            private List<{mp.Ref.Name}> _{mp.Name} = null;
                            [EDMProperty(PropType=""{mp.Type}"", ItemType=typeof({mp.Ref.Name}), Header=""{mp.Header}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public List<{mp.Ref.Name}> {mp.Name}
                            {{
                                get 
                                {{
                                    if (_{mp.Name}==null)
                                    {{
                                        _{mp.Name} = new List<{mp.Ref.Name}>();
                                    }}
                                    return _{mp.Name};
                                }}
                            }}
                            ";
                        break;
                    }
                    {
                        if (mp.Ref == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - ссылка не задан или задан неправильно ({mp.RefId})";
                            break;
                        }
                        if (mp.Ref.IdType == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - тип ссылки определить невозможно ({mp.RefId})";
                            break;
                        }
                        _ = $@"
                            private Dictionary<string, List<{mp.Ref.Name}>> _{mp.Name} = null;
                            [EDMProperty(PropType=""{mp.Type}"", ItemType=typeof({mp.Ref.Name}), Header=""{mp.Header}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public Dictionary<string, List<{mp.Ref.Name}>> {mp.Name}
                            {{
                                get 
                                {{
                                    if (_{mp.Name}==null)
                                    {{
                                        _{mp.Name} = new Dictionary<string, List<{mp.Ref.Name}>>();
                                    }}
                                    return _{mp.Name};
                                }}
                            }}
                            ";
                        break;
                    }
                case MEDMPropType.ENUM:
                    {
                        if (mp.Ref == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - ссылка не задан или задан неправильно ({mp.RefId})";
                            break;
                        }
                        if (mp.Ref.IdType == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - тип ссылки определить невозможно ({mp.RefId})";
                            break;
                        }
                        break;
                        _ = $@"
                            private {mp.Ref.Name} _{mp.Name} = default({mp.Ref.Name});
                            [DbColumn(Type = ""{mp.ColumnType}"", Def =""{mp.ColumnDef}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            [EDMProperty(PropType=""{mp.Type}"", ItemType=typeof({mp.Ref.Name}), Header=""{mp.Header}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public {mp.Ref.Name} {mp.Name}
                            {{
                                get 
                                {{
                                    return _{mp.Name};
                                }}
                                set 
                                {{
                                    Model.UpdateProperty(this, ""{mp.Name}"", _{mp.Name}, value, typeof({mp.Ref.Name}), null, {mp.Virtual.ToString().ToLower()});
                                    _{mp.Name}=value;
                                }}
                            }}";
                        break;
                    }
                // По умолчанию
                default:
                    {
                        if (mp.PropType == null)
                        {
                            __ = $"error: {mc.Name}.{mp.Name} - тип не задан или задан неправильно ({mp.Type})";
                            break;
                        }
                        _ = $@"
                            private {mp.PropType.Name} _{mp.Name} = default({mp.PropType.Name});
                            [DbColumn(Type = ""{mp.ColumnType}"", Def =""{mp.ColumnDef}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            [EDMProperty(PropType=""{mp.Type}"", ItemType=typeof({mp.PropType.Name}), Header=""{mp.Header}"", Virtual ={mp.Virtual.ToString().ToLower()})]
                            {mp.JsonIgnoreAttribute}
                            public {mp.PropType.Name} {mp.Name}
                            {{
                                get 
                                {{
                                    return _{mp.Name};
                                }}
                                set 
                                {{
                                    Model.UpdateProperty(this, ""{mp.Name}"", _{mp.Name}, value, typeof({mp.PropType.Name}), null, {mp.Virtual.ToString().ToLower()});
                                    _{mp.Name}=value;
                                }}
                            }}";
                        break;
                    }
            }
            _ = $"#endregion";

            return Result.ToString();
        }
        #endregion
    }
    public class MEDMProject : MEDMCfgObj
    {
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
                }
            }
        }
        #endregion
        #region Short
        private string _Short = default(string);
        public string Short
        {
            get
            {
                return _Short;
            }
            set
            {
                if (_Short != value)
                {
                    _Short = value;
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
                }
            }
        }
        #endregion

        #region Classes
        private List<MEDMClass> _Classes = null;
        public List<MEDMClass> Classes
        {
            get
            {
                if (_Classes == null)
                {
                    _Classes = new List<MEDMClass>();
                }
                return _Classes;
            }
        }
        #endregion
    }
    public enum MEDMClassType { NONE = 0, MODELCLASS, CLASS, TABLE, BASECLASS, CFG, ENUM };
    public class MEDMClass : MEDMCfgObj
    {
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
                }
            }
        }
        #endregion
        #region Short
        private string _Short = default(string);
        public string Short
        {
            get
            {
                return _Short;
            }
            set
            {
                if (_Short != value)
                {
                    _Short = value;
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
                }
            }
        }
        #endregion

        #region Type Тип класса
        private MEDMClassType _Type = MEDMClassType.MODELCLASS;
        public MEDMClassType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                }
            }
        }
        public Type IdType
        {
            get
            {
                Type v = null;
                MEDMClass c = this;
                while (c != null)
                {
                    switch (c.Name.ToLower())
                    {
                        case "medmidobj":
                            v = typeof(int);
                            break;
                        case "medmnameobj":
                            v = typeof(string);
                            break;
                        case "medmguidobj":
                            v = typeof(Guid);
                            break;
                    }
                    if (v != null) break;
                    c = c.Base;
                }
                return v;
            }
        }
        public string IdColumnType
        {
            get
            {
                string v = "???";
                MEDMClass c = this;
                while (c != null)
                {
                    switch (c.Name.ToLower())
                    {
                        case "medmidobj":
                            v = "int";
                            break;
                        case "medmnameobj":
                            v = "nvarchar(255)";
                            break;
                        case "medmguidobj":
                            v = "uniqueidentifier";
                            break;
                    }
                    if (v != "???") break;
                    c = c.Base;
                }
                return v;
            }
        }
        public string IdColumnDef
        {
            get
            {
                string v = "???";
                MEDMClass c = this;
                while (c != null)
                {
                    switch (c.Name.ToLower())
                    {
                        case "medmidobj":
                            v = "0";
                            break;
                        case "medmnameobj":
                            v = "''";
                            break;
                        case "medmguidobj":
                            v = $"'{default(Guid)}'";
                            break;
                    }
                    if (v != "???") break;
                    c = c.Base;
                }
                return v;
            }
        }

        #endregion
        #region [Base] Базовый класс
        private string _BaseId = default(string);
        public string BaseId
        {
            get
            {
                return _BaseId;
            }
            set
            {
                if (_BaseId != value)
                {
                    Model.UpdateProperty(this, "BaseId", _BaseId, value, typeof(Guid), typeof(MEDMClass), false);
                    _BaseId = value;
                    _Base = null;
                }
            }
        }
        private MEDMClass _Base = default(MEDMClass);
        public MEDMClass Base
        {
            get
            {


                if (_Base == null && _BaseId != default(string))
                {
                    Model.UpdateFutureRef(typeof(MEDMClass), "EDMClass", "Id", "Guid");
                    _Base = Model.MainDic.GetObj<MEDMClass>(BaseId);
                }
                return _Base;
            }
        }
        public bool ShouldSerializeBase()
        {
            return false;
        }

        #endregion

        #region Props
        private List<MEDMProp> _Props = null;
        public List<MEDMProp> Props
        {
            get
            {
                if (_Props == null)
                {
                    _Props = new List<MEDMProp>();
                }
                return _Props;
            }
        }
        #endregion
        #region ToString
        private string _ToString = default(string);
        public string ToString
        {
            get
            {
                return _ToString;
            }
            set
            {
                if (_ToString != value)
                {
                    _ToString = value;
                }
            }
        }
        #endregion
    }
    public enum MEDMPropType { NONE = 0, STRING, LONG, INT, SHORT, BYTE, FLOAT, DECIMAL, MONEY, DATE, BOOL, GUID, REF, CFG, ENUM, LIST, PROPS };
    public enum MEDMPropRefType { NONE = 0, RESTRICT, CASCADE };
    public class MEDMProp : MEDMCfgObj
    {
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
                }
            }
        }
        #endregion
        #region Short
        private string _Short = default(string);
        public string Short
        {
            get
            {
                return _Short;
            }
            set
            {
                if (_Short != value)
                {
                    _Short = value;
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
                }
            }
        }
        #endregion

        #region Type Тип данных
        private MEDMPropType _Type = MEDMPropType.NONE;
        public MEDMPropType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                }
            }
        }
        public Type PropType
        {
            get
            {
                Type v = null;
                switch (Type)
                {
                    case MEDMPropType.STRING:
                        v = typeof(string);
                        break;
                    case MEDMPropType.LONG:
                        v = typeof(long);
                        break;
                    case MEDMPropType.INT:
                        v = typeof(int);
                        break;
                    case MEDMPropType.SHORT:
                        v = typeof(short);
                        break;
                    case MEDMPropType.BYTE:
                        v = typeof(byte);
                        break;
                    case MEDMPropType.FLOAT:
                        v = typeof(float);
                        break;
                    case MEDMPropType.DECIMAL:
                        v = typeof(decimal);
                        break;
                    case MEDMPropType.MONEY:
                        v = typeof(decimal);
                        break;
                    case MEDMPropType.DATE:
                        v = typeof(DateTime);
                        break;
                    case MEDMPropType.BOOL:
                        v = typeof(bool);
                        break;
                    case MEDMPropType.GUID:
                        v = typeof(Guid);
                        break;
                }
                return v;
            }
        }
        public string ColumnType
        {
            get
            {
                string v = "???";
                switch (Type)
                {
                    case MEDMPropType.STRING:
                        if (Length == 0) v = $"nvarchar(MAX)";
                        else v = $"nvarchar({Length})";
                        break;
                    case MEDMPropType.LONG:
                        v = $"bigint";
                        break;
                    case MEDMPropType.INT:
                        v = $"int";
                        break;
                    case MEDMPropType.SHORT:
                        v = $"smallint";
                        break;
                    case MEDMPropType.BYTE:
                        v = $"tinyint";
                        break;
                    case MEDMPropType.FLOAT:
                        v = $"float";
                        break;
                    case MEDMPropType.DECIMAL:
                        v = $"decimal({Length}, {Dec})";
                        break;
                    case MEDMPropType.MONEY:
                        v = $"money";
                        break;
                    case MEDMPropType.DATE:
                        v = $"datetime";
                        break;
                    case MEDMPropType.BOOL:
                        v = $"tinyint";
                        break;
                    case MEDMPropType.GUID:
                        v = $"uniqueidentifier";
                        break;
                    case MEDMPropType.ENUM:
                        v = $"string";
                        break;
                }
                return v;
            }
        }
        public string ColumnDef
        {
            get
            {
                string v = "???";
                switch (Type)
                {
                    case MEDMPropType.STRING:
                        v = $"''";
                        break;
                    case MEDMPropType.LONG:
                        v = $"0";
                        break;
                    case MEDMPropType.INT:
                        v = $"0";
                        break;
                    case MEDMPropType.SHORT:
                        v = $"0";
                        break;
                    case MEDMPropType.BYTE:
                        v = $"0";
                        break;
                    case MEDMPropType.FLOAT:
                        v = $"0";
                        break;
                    case MEDMPropType.DECIMAL:
                        v = $"0";
                        break;
                    case MEDMPropType.MONEY:
                        v = $"0";
                        break;
                    case MEDMPropType.DATE:
                        v = $"'17530101'";
                        break;
                    case MEDMPropType.BOOL:
                        v = $"0";
                        break;
                    case MEDMPropType.GUID:
                        v = $"newid()";
                        break;
                }
                return v;
            }
        }
        #endregion
        #region Length Длина
        private int _Length = 0;
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
                }
            }
        }
        #endregion
        #region Dec Знаков после точки
        private int _Dec = 0;
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
                }
            }
        }
        #endregion

        #region RefType Тип ссылки
        private MEDMPropRefType _RefType = MEDMPropRefType.NONE;
        public MEDMPropRefType RefType
        {
            get
            {
                return _RefType;
            }
            set
            {
                if (_RefType != value)
                {
                    _RefType = value;
                }
            }
        }
        #endregion
        #region [Ref] Базовый класс
        private string _RefId = "";
        public string RefId
        {
            get
            {
                return _RefId;
            }
            set
            {
                if (_RefId != value)
                {
                    Model.UpdateProperty(this, "RefId", _RefId, value, typeof(Guid), typeof(MEDMClass), false);
                    _RefId = value;
                    _Ref = null;
                }
            }
        }
        private MEDMClass _Ref = default(MEDMClass);
        public MEDMClass Ref
        {
            get
            {
                if (_Ref == null)
                {
                    Model.UpdateFutureRef(typeof(MEDMClass), "EDMClass", "Id", "Guid");
                    _Ref = Model.MainDic.GetObj<MEDMClass>(RefId);
                }
                return _Ref;
            }
        }
        public bool ShouldSerializeRef()
        {
            return false;
        }

        #endregion
        #region JsonIgnore
        private bool _JsonIgnore = default(bool);
        public bool JsonIgnore
        {
            get
            {
                return _JsonIgnore;
            }
            set
            {
                if (_JsonIgnore != value)
                {
                    _JsonIgnore = value;
                }
            }
        }
        public string JsonIgnoreAttribute
        {
            get
            {
                if (JsonIgnore) return "[Ignore]";
                return "";
            }
        }
        #endregion
        #region ReadOnly
        private bool _ReadOnly = default(bool);
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                if (_ReadOnly != value)
                {
                    _ReadOnly = value;
                }
            }
        }
        #endregion
        #region Virtual
        private bool _Virtual = default(bool);
        public bool Virtual
        {
            get
            {
                return _Virtual;
            }
            set
            {
                if (_Virtual != value)
                {
                    _Virtual = value;
                }
            }
        }
        #endregion
    }
}
