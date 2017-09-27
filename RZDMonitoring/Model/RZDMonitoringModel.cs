//--------------------------------------------------------------------------------
// MBuilder3X Генератор модели данных (классы)                (16.07.2018 20:29:22)
//--------------------------------------------------------------------------------
using System;
using MEDMCoreLibrary;
using System.Collections.Generic;
#region [RZDMonitoringModel] Автоматизированная система поиска, обработки и хранения информаци по основным направлениям деятельности ОАО РЖД
namespace Models.RZDMonitoringModel
{
    public partial class RZDMonitoringModel : MEDMSql
    {
        #region Конструктор
        public RZDMonitoringModel()
        {
            MRefDic.FindObj(null);
            MFindDic.FindObj(null);
            MArtDic.FindObj(null);
            MPdfDic.FindObj(null);
            MImgDic.FindObj(null);
            MRubricatorDic.FindObj(null);
            MArt2RubricarorDic.FindObj(null);
            MTransDic.FindObj(null);
            MLangDic.FindObj(null);
            MUserDic.FindObj(null);
        }
        #endregion
        #region Словарь
        #region [MRef] Архив ссылок
        private MEDMObjectDic _MRefDic = null;
        public MEDMObjectDic MRefDic
        {
            get
            {
                if (_MRefDic == null) _MRefDic = new MEDMObjectDic(this, typeof(MRef));
                if (!MainDic.ContainsKey(typeof(MRef))) MainDic[typeof(MRef)] = _MRefDic;
                return _MRefDic;
            }
        }
        #endregion
        #region [MFind] Классификатор
        private MEDMObjectDic _MFindDic = null;
        public MEDMObjectDic MFindDic
        {
            get
            {
                if (_MFindDic == null) _MFindDic = new MEDMObjectDic(this, typeof(MFind));
                if (!MainDic.ContainsKey(typeof(MFind))) MainDic[typeof(MFind)] = _MFindDic;
                return _MFindDic;
            }
        }
        #endregion
        #region [MArt] Архив НТИ
        private MEDMObjectDic _MArtDic = null;
        public MEDMObjectDic MArtDic
        {
            get
            {
                if (_MArtDic == null) _MArtDic = new MEDMObjectDic(this, typeof(MArt));
                if (!MainDic.ContainsKey(typeof(MArt))) MainDic[typeof(MArt)] = _MArtDic;
                return _MArtDic;
            }
        }
        #endregion
        #region [MPdf] Архив PDF
        private MEDMObjectDic _MPdfDic = null;
        public MEDMObjectDic MPdfDic
        {
            get
            {
                if (_MPdfDic == null) _MPdfDic = new MEDMObjectDic(this, typeof(MPdf));
                if (!MainDic.ContainsKey(typeof(MPdf))) MainDic[typeof(MPdf)] = _MPdfDic;
                return _MPdfDic;
            }
        }
        #endregion
        #region [MImg] Архив изображений
        private MEDMObjectDic _MImgDic = null;
        public MEDMObjectDic MImgDic
        {
            get
            {
                if (_MImgDic == null) _MImgDic = new MEDMObjectDic(this, typeof(MImg));
                if (!MainDic.ContainsKey(typeof(MImg))) MainDic[typeof(MImg)] = _MImgDic;
                return _MImgDic;
            }
        }
        #endregion
        #region [MRubricator] Рубрикатор
        private MEDMObjectDic _MRubricatorDic = null;
        public MEDMObjectDic MRubricatorDic
        {
            get
            {
                if (_MRubricatorDic == null) _MRubricatorDic = new MEDMObjectDic(this, typeof(MRubricator));
                if (!MainDic.ContainsKey(typeof(MRubricator))) MainDic[typeof(MRubricator)] = _MRubricatorDic;
                return _MRubricatorDic;
            }
        }
        #endregion
        #region [MArt2Rubricaror]
        private MEDMObjectDic _MArt2RubricarorDic = null;
        public MEDMObjectDic MArt2RubricarorDic
        {
            get
            {
                if (_MArt2RubricarorDic == null) _MArt2RubricarorDic = new MEDMObjectDic(this, typeof(MArt2Rubricaror));
                if (!MainDic.ContainsKey(typeof(MArt2Rubricaror))) MainDic[typeof(MArt2Rubricaror)] = _MArt2RubricarorDic;
                return _MArt2RubricarorDic;
            }
        }
        #endregion
        #region [MTrans] Архив переводов
        private MEDMObjectDic _MTransDic = null;
        public MEDMObjectDic MTransDic
        {
            get
            {
                if (_MTransDic == null) _MTransDic = new MEDMObjectDic(this, typeof(MTrans));
                if (!MainDic.ContainsKey(typeof(MTrans))) MainDic[typeof(MTrans)] = _MTransDic;
                return _MTransDic;
            }
        }
        #endregion
        #region [MLang] Справочник языков
        private MEDMObjectDic _MLangDic = null;
        public MEDMObjectDic MLangDic
        {
            get
            {
                if (_MLangDic == null) _MLangDic = new MEDMObjectDic(this, typeof(MLang));
                if (!MainDic.ContainsKey(typeof(MLang))) MainDic[typeof(MLang)] = _MLangDic;
                return _MLangDic;
            }
        }
        #endregion
        #region [MUser] Справочник языков
        private MEDMObjectDic _MUserDic = null;
        public MEDMObjectDic MUserDic
        {
            get
            {
                if (_MUserDic == null) _MUserDic = new MEDMObjectDic(this, typeof(MUser));
                if (!MainDic.ContainsKey(typeof(MUser))) MainDic[typeof(MUser)] = _MUserDic;
                return _MUserDic;
            }
        }
        #endregion
        #endregion
    }
    #region [MRef] Архив ссылок
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Архив ссылок")]
    public partial class MRef: MEDMIdObj
    {
        #region [Url] Код
        private String _Url = default(String);
        [DbColumn(Type = "nvarchar(1024)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Код", Virtual =false)]
        
        public String Url
        {
            get 
            {
                return _Url;
            }
            set 
            {
                Model.UpdateProperty(this, "Url", _Url, value, typeof(String), null, false);
                _Url=value;
            }
        }
        #endregion
        #region [Header] Наименование
        private String _Header = default(String);
        [DbColumn(Type = "nvarchar(1024)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Header
        {
            get 
            {
                return _Header;
            }
            set 
            {
                Model.UpdateProperty(this, "Header", _Header, value, typeof(String), null, false);
                _Header=value;
            }
        }
        #endregion
        #region [WriteDate] Дата записи
        private DateTime _WriteDate = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата записи", Virtual =false)]
        
        public DateTime WriteDate
        {
            get 
            {
                return _WriteDate;
            }
            set 
            {
                Model.UpdateProperty(this, "WriteDate", _WriteDate, value, typeof(DateTime), null, false);
                _WriteDate=value;
            }
        }
        #endregion
        #region [WriteUser] Кто записал
        private Int32 _WriteUserId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Кто записал (Id)", Virtual =false)]
        
        public Int32 WriteUserId
        {
            get 
            {
                return _WriteUserId;
            }
            set 
            {
                Model.UpdateProperty(this, "WriteUserId", _WriteUserId, value, typeof(Int32), typeof(MUser), false);
                _WriteUserId=value;
            }
        }
        private MUser _WriteUser = default(MUser);
        [EDMProperty(PropType="REF", ItemType=typeof(MUser), Header="Кто записал", Virtual =false)]
        
        public MUser WriteUser
        {
            get 
            {
                if (_WriteUser==null && _WriteUserId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MUser), "MUser", "Id", "System.Int32");
                    _WriteUser = Model.MainDic.GetObj<MUser>(WriteUserId);
                }
                return _WriteUser;
            }
        }
        //public bool ShouldSerializeMUser()
        //{
        //    return false;
        //}
        
        #endregion
        #region [WriteFind] Настройка поиска
        private Int32 _WriteFindId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Настройка поиска (Id)", Virtual =false)]
        
        public Int32 WriteFindId
        {
            get 
            {
                return _WriteFindId;
            }
            set 
            {
                Model.UpdateProperty(this, "WriteFindId", _WriteFindId, value, typeof(Int32), typeof(MFind), false);
                _WriteFindId=value;
            }
        }
        private MFind _WriteFind = default(MFind);
        [EDMProperty(PropType="REF", ItemType=typeof(MFind), Header="Настройка поиска", Virtual =false)]
        
        public MFind WriteFind
        {
            get 
            {
                if (_WriteFind==null && _WriteFindId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MFind), "MFind", "Id", "System.Int32");
                    _WriteFind = Model.MainDic.GetObj<MFind>(WriteFindId);
                }
                return _WriteFind;
            }
        }
        //public bool ShouldSerializeMFind()
        //{
        //    return false;
        //}
        
        #endregion
        #region [DisabledDate] Дата закрытия
        private DateTime _DisabledDate = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата закрытия", Virtual =false)]
        
        public DateTime DisabledDate
        {
            get 
            {
                return _DisabledDate;
            }
            set 
            {
                Model.UpdateProperty(this, "DisabledDate", _DisabledDate, value, typeof(DateTime), null, false);
                _DisabledDate=value;
            }
        }
        #endregion
        #region [DisabledUser] Кто закрыл
        private Int32 _DisabledUserId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Кто закрыл (Id)", Virtual =false)]
        
        public Int32 DisabledUserId
        {
            get 
            {
                return _DisabledUserId;
            }
            set 
            {
                Model.UpdateProperty(this, "DisabledUserId", _DisabledUserId, value, typeof(Int32), typeof(MUser), false);
                _DisabledUserId=value;
            }
        }
        private MUser _DisabledUser = default(MUser);
        [EDMProperty(PropType="REF", ItemType=typeof(MUser), Header="Кто закрыл", Virtual =false)]
        
        public MUser DisabledUser
        {
            get 
            {
                if (_DisabledUser==null && _DisabledUserId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MUser), "MUser", "Id", "System.Int32");
                    _DisabledUser = Model.MainDic.GetObj<MUser>(DisabledUserId);
                }
                return _DisabledUser;
            }
        }
        //public bool ShouldSerializeMUser()
        //{
        //    return false;
        //}
        
        #endregion
    }
    #endregion
    #region [MFind] Классификатор
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Классификатор")]
    public partial class MFind: MEDMIdObj
    {
        #region [Header] Наименование
        private String _Header = default(String);
        [DbColumn(Type = "nvarchar(1024)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Header
        {
            get 
            {
                return _Header;
            }
            set 
            {
                Model.UpdateProperty(this, "Header", _Header, value, typeof(String), null, false);
                _Header=value;
            }
        }
        #endregion
        #region [Props] Свойства
        private String _Props = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Свойства", Virtual =false)]
        
        public String Props
        {
            get 
            {
                return _Props;
            }
            set 
            {
                Model.UpdateProperty(this, "Props", _Props, value, typeof(String), null, false);
                _Props=value;
            }
        }
        #endregion
        #region [OwnUser] Владелец
        private Int32 _OwnUserId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Владелец (Id)", Virtual =false)]
        
        public Int32 OwnUserId
        {
            get 
            {
                return _OwnUserId;
            }
            set 
            {
                Model.UpdateProperty(this, "OwnUserId", _OwnUserId, value, typeof(Int32), typeof(MUser), false);
                _OwnUserId=value;
            }
        }
        private MUser _OwnUser = default(MUser);
        [EDMProperty(PropType="REF", ItemType=typeof(MUser), Header="Владелец", Virtual =false)]
        
        public MUser OwnUser
        {
            get 
            {
                if (_OwnUser==null && _OwnUserId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MUser), "MUser", "Id", "System.Int32");
                    _OwnUser = Model.MainDic.GetObj<MUser>(OwnUserId);
                }
                return _OwnUser;
            }
        }
        //public bool ShouldSerializeMUser()
        //{
        //    return false;
        //}
        
        #endregion
        #region [WriteDate] Дата записи
        private DateTime _WriteDate = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата записи", Virtual =false)]
        
        public DateTime WriteDate
        {
            get 
            {
                return _WriteDate;
            }
            set 
            {
                Model.UpdateProperty(this, "WriteDate", _WriteDate, value, typeof(DateTime), null, false);
                _WriteDate=value;
            }
        }
        #endregion
        #region [IsEnabled] Разрешена
        private Boolean _IsEnabled = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешена", Virtual =false)]
        
        public Boolean IsEnabled
        {
            get 
            {
                return _IsEnabled;
            }
            set 
            {
                Model.UpdateProperty(this, "IsEnabled", _IsEnabled, value, typeof(Boolean), null, false);
                _IsEnabled=value;
            }
        }
        #endregion
        #region [RunDate] Дата последнего поиска
        private DateTime _RunDate = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата последнего поиска", Virtual =false)]
        
        public DateTime RunDate
        {
            get 
            {
                return _RunDate;
            }
            set 
            {
                Model.UpdateProperty(this, "RunDate", _RunDate, value, typeof(DateTime), null, false);
                _RunDate=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MArt] Архив НТИ
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Архив НТИ")]
    public partial class MArt: MEDMIdObj
    {
        #region [Ref] Архив ссылок
        private Int32 _RefId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Архив ссылок (Id)", Virtual =false)]
        
        public Int32 RefId
        {
            get 
            {
                return _RefId;
            }
            set 
            {
                Model.UpdateProperty(this, "RefId", _RefId, value, typeof(Int32), typeof(MRef), false);
                _RefId=value;
            }
        }
        private MRef _Ref = default(MRef);
        [EDMProperty(PropType="REF", ItemType=typeof(MRef), Header="Архив ссылок", Virtual =false)]
        
        public MRef Ref
        {
            get 
            {
                if (_Ref==null && _RefId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MRef), "MRef", "Id", "System.Int32");
                    _Ref = Model.MainDic.GetObj<MRef>(RefId);
                }
                return _Ref;
            }
        }
        //public bool ShouldSerializeMRef()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Header] Наименование
        private String _Header = default(String);
        [DbColumn(Type = "nvarchar(1024)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Header
        {
            get 
            {
                return _Header;
            }
            set 
            {
                Model.UpdateProperty(this, "Header", _Header, value, typeof(String), null, false);
                _Header=value;
            }
        }
        #endregion
        #region [Short] Аннотациция
        private String _Short = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Аннотациция", Virtual =false)]
        
        public String Short
        {
            get 
            {
                return _Short;
            }
            set 
            {
                Model.UpdateProperty(this, "Short", _Short, value, typeof(String), null, false);
                _Short=value;
            }
        }
        #endregion
        #region [Text] Текст
        private String _Text = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Текст", Virtual =false)]
        
        public String Text
        {
            get 
            {
                return _Text;
            }
            set 
            {
                Model.UpdateProperty(this, "Text", _Text, value, typeof(String), null, false);
                _Text=value;
            }
        }
        #endregion
        #region [WriteDate] Дата записи
        private DateTime _WriteDate = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата записи", Virtual =false)]
        
        public DateTime WriteDate
        {
            get 
            {
                return _WriteDate;
            }
            set 
            {
                Model.UpdateProperty(this, "WriteDate", _WriteDate, value, typeof(DateTime), null, false);
                _WriteDate=value;
            }
        }
        #endregion
        #region [WriteUser] Кто записал
        private Int32 _WriteUserId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Кто записал (Id)", Virtual =false)]
        
        public Int32 WriteUserId
        {
            get 
            {
                return _WriteUserId;
            }
            set 
            {
                Model.UpdateProperty(this, "WriteUserId", _WriteUserId, value, typeof(Int32), typeof(MUser), false);
                _WriteUserId=value;
            }
        }
        private MUser _WriteUser = default(MUser);
        [EDMProperty(PropType="REF", ItemType=typeof(MUser), Header="Кто записал", Virtual =false)]
        
        public MUser WriteUser
        {
            get 
            {
                if (_WriteUser==null && _WriteUserId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MUser), "MUser", "Id", "System.Int32");
                    _WriteUser = Model.MainDic.GetObj<MUser>(WriteUserId);
                }
                return _WriteUser;
            }
        }
        //public bool ShouldSerializeMUser()
        //{
        //    return false;
        //}
        
        #endregion
        #region [IsReady] Готово
        private Boolean _IsReady = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Готово", Virtual =false)]
        
        public Boolean IsReady
        {
            get 
            {
                return _IsReady;
            }
            set 
            {
                Model.UpdateProperty(this, "IsReady", _IsReady, value, typeof(Boolean), null, false);
                _IsReady=value;
            }
        }
        #endregion
        #region [Language] Язык
        private Int32 _LanguageId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Язык (Id)", Virtual =false)]
        
        public Int32 LanguageId
        {
            get 
            {
                return _LanguageId;
            }
            set 
            {
                Model.UpdateProperty(this, "LanguageId", _LanguageId, value, typeof(Int32), typeof(MLang), false);
                _LanguageId=value;
            }
        }
        private MLang _Language = default(MLang);
        [EDMProperty(PropType="REF", ItemType=typeof(MLang), Header="Язык", Virtual =false)]
        
        public MLang Language
        {
            get 
            {
                if (_Language==null && _LanguageId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MLang), "MLang", "Id", "System.Int32");
                    _Language = Model.MainDic.GetObj<MLang>(LanguageId);
                }
                return _Language;
            }
        }
        //public bool ShouldSerializeMLang()
        //{
        //    return false;
        //}
        
        #endregion
    }
    #endregion
    #region [MPdf] Архив PDF
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Архив PDF")]
    public partial class MPdf: MEDMIdObj
    {
        #region [Ref] Архив НТИ
        private Int32 _RefId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Архив НТИ (Id)", Virtual =false)]
        
        public Int32 RefId
        {
            get 
            {
                return _RefId;
            }
            set 
            {
                Model.UpdateProperty(this, "RefId", _RefId, value, typeof(Int32), typeof(MArt), false);
                _RefId=value;
            }
        }
        private MArt _Ref = default(MArt);
        [EDMProperty(PropType="REF", ItemType=typeof(MArt), Header="Архив НТИ", Virtual =false)]
        
        public MArt Ref
        {
            get 
            {
                if (_Ref==null && _RefId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MArt), "MArt", "Id", "System.Int32");
                    _Ref = Model.MainDic.GetObj<MArt>(RefId);
                }
                return _Ref;
            }
        }
        //public bool ShouldSerializeMArt()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Type] Тип файла
        private String _Type = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Тип файла", Virtual =false)]
        
        public String Type
        {
            get 
            {
                return _Type;
            }
            set 
            {
                Model.UpdateProperty(this, "Type", _Type, value, typeof(String), null, false);
                _Type=value;
            }
        }
        #endregion
        #region [Data] Данные
        private String _Data = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Данные", Virtual =false)]
        
        public String Data
        {
            get 
            {
                return _Data;
            }
            set 
            {
                Model.UpdateProperty(this, "Data", _Data, value, typeof(String), null, false);
                _Data=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MImg] Архив изображений
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Архив изображений")]
    public partial class MImg: MEDMIdObj
    {
        #region [Ref] Архив НТИ
        private Int32 _RefId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Архив НТИ (Id)", Virtual =false)]
        
        public Int32 RefId
        {
            get 
            {
                return _RefId;
            }
            set 
            {
                Model.UpdateProperty(this, "RefId", _RefId, value, typeof(Int32), typeof(MArt), false);
                _RefId=value;
            }
        }
        private MArt _Ref = default(MArt);
        [EDMProperty(PropType="REF", ItemType=typeof(MArt), Header="Архив НТИ", Virtual =false)]
        
        public MArt Ref
        {
            get 
            {
                if (_Ref==null && _RefId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MArt), "MArt", "Id", "System.Int32");
                    _Ref = Model.MainDic.GetObj<MArt>(RefId);
                }
                return _Ref;
            }
        }
        //public bool ShouldSerializeMArt()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Type] Тип файла
        private String _Type = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Тип файла", Virtual =false)]
        
        public String Type
        {
            get 
            {
                return _Type;
            }
            set 
            {
                Model.UpdateProperty(this, "Type", _Type, value, typeof(String), null, false);
                _Type=value;
            }
        }
        #endregion
        #region [Data] Данные
        private String _Data = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Данные", Virtual =false)]
        
        public String Data
        {
            get 
            {
                return _Data;
            }
            set 
            {
                Model.UpdateProperty(this, "Data", _Data, value, typeof(String), null, false);
                _Data=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MRubricator] Рубрикатор
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Рубрикатор")]
    public partial class MRubricator: MEDMIdObj
    {
        #region [Parent] Родительский узел
        private Int32 _ParentId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Родительский узел (Id)", Virtual =false)]
        
        public Int32 ParentId
        {
            get 
            {
                return _ParentId;
            }
            set 
            {
                Model.UpdateProperty(this, "ParentId", _ParentId, value, typeof(Int32), typeof(MRubricator), false);
                _ParentId=value;
            }
        }
        private MRubricator _Parent = default(MRubricator);
        [EDMProperty(PropType="REF", ItemType=typeof(MRubricator), Header="Родительский узел", Virtual =false)]
        
        public MRubricator Parent
        {
            get 
            {
                if (_Parent==null && _ParentId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MRubricator), "MRubricator", "Id", "System.Int32");
                    _Parent = Model.MainDic.GetObj<MRubricator>(ParentId);
                }
                return _Parent;
            }
        }
        //public bool ShouldSerializeMRubricator()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Code] Код
        private String _Code = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Код", Virtual =false)]
        
        public String Code
        {
            get 
            {
                return _Code;
            }
            set 
            {
                Model.UpdateProperty(this, "Code", _Code, value, typeof(String), null, false);
                _Code=value;
            }
        }
        #endregion
        #region [Header] Наименование
        private String _Header = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Header
        {
            get 
            {
                return _Header;
            }
            set 
            {
                Model.UpdateProperty(this, "Header", _Header, value, typeof(String), null, false);
                _Header=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MArt2Rubricaror]
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "")]
    public partial class MArt2Rubricaror: MEDMIdObj
    {
        #region [Rubricator] Рубрикатор
        private Int32 _RubricatorId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Рубрикатор (Id)", Virtual =false)]
        
        public Int32 RubricatorId
        {
            get 
            {
                return _RubricatorId;
            }
            set 
            {
                Model.UpdateProperty(this, "RubricatorId", _RubricatorId, value, typeof(Int32), typeof(MRubricator), false);
                _RubricatorId=value;
            }
        }
        private MRubricator _Rubricator = default(MRubricator);
        [EDMProperty(PropType="REF", ItemType=typeof(MRubricator), Header="Рубрикатор", Virtual =false)]
        
        public MRubricator Rubricator
        {
            get 
            {
                if (_Rubricator==null && _RubricatorId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MRubricator), "MRubricator", "Id", "System.Int32");
                    _Rubricator = Model.MainDic.GetObj<MRubricator>(RubricatorId);
                }
                return _Rubricator;
            }
        }
        //public bool ShouldSerializeMRubricator()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Art] Статья НТИ
        private Int32 _ArtId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Статья НТИ (Id)", Virtual =false)]
        
        public Int32 ArtId
        {
            get 
            {
                return _ArtId;
            }
            set 
            {
                Model.UpdateProperty(this, "ArtId", _ArtId, value, typeof(Int32), typeof(MArt), false);
                _ArtId=value;
            }
        }
        private MArt _Art = default(MArt);
        [EDMProperty(PropType="REF", ItemType=typeof(MArt), Header="Статья НТИ", Virtual =false)]
        
        public MArt Art
        {
            get 
            {
                if (_Art==null && _ArtId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MArt), "MArt", "Id", "System.Int32");
                    _Art = Model.MainDic.GetObj<MArt>(ArtId);
                }
                return _Art;
            }
        }
        //public bool ShouldSerializeMArt()
        //{
        //    return false;
        //}
        
        #endregion
    }
    #endregion
    #region [MTrans] Архив переводов
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Архив переводов")]
    public partial class MTrans: MEDMIdObj
    {
        #region [Text] Текст
        private String _Text = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Текст", Virtual =false)]
        
        public String Text
        {
            get 
            {
                return _Text;
            }
            set 
            {
                Model.UpdateProperty(this, "Text", _Text, value, typeof(String), null, false);
                _Text=value;
            }
        }
        #endregion
        #region [TransDate] Дата перевода
        private DateTime _TransDate = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата перевода", Virtual =false)]
        
        public DateTime TransDate
        {
            get 
            {
                return _TransDate;
            }
            set 
            {
                Model.UpdateProperty(this, "TransDate", _TransDate, value, typeof(DateTime), null, false);
                _TransDate=value;
            }
        }
        #endregion
        #region [TrasUser] Переводчик
        private Int32 _TrasUserId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Переводчик (Id)", Virtual =false)]
        
        public Int32 TrasUserId
        {
            get 
            {
                return _TrasUserId;
            }
            set 
            {
                Model.UpdateProperty(this, "TrasUserId", _TrasUserId, value, typeof(Int32), typeof(MUser), false);
                _TrasUserId=value;
            }
        }
        private MUser _TrasUser = default(MUser);
        [EDMProperty(PropType="REF", ItemType=typeof(MUser), Header="Переводчик", Virtual =false)]
        
        public MUser TrasUser
        {
            get 
            {
                if (_TrasUser==null && _TrasUserId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MUser), "MUser", "Id", "System.Int32");
                    _TrasUser = Model.MainDic.GetObj<MUser>(TrasUserId);
                }
                return _TrasUser;
            }
        }
        //public bool ShouldSerializeMUser()
        //{
        //    return false;
        //}
        
        #endregion
        #region [IsReady] Готово
        private Boolean _IsReady = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Готово", Virtual =false)]
        
        public Boolean IsReady
        {
            get 
            {
                return _IsReady;
            }
            set 
            {
                Model.UpdateProperty(this, "IsReady", _IsReady, value, typeof(Boolean), null, false);
                _IsReady=value;
            }
        }
        #endregion
        #region [IsAuto] Автоматический перевод
        private Boolean _IsAuto = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Автоматический перевод", Virtual =false)]
        
        public Boolean IsAuto
        {
            get 
            {
                return _IsAuto;
            }
            set 
            {
                Model.UpdateProperty(this, "IsAuto", _IsAuto, value, typeof(Boolean), null, false);
                _IsAuto=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MLang] Справочник языков
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Справочник языков")]
    public partial class MLang: MEDMIdObj
    {
        #region [Code] Код
        private String _Code = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Код", Virtual =false)]
        
        public String Code
        {
            get 
            {
                return _Code;
            }
            set 
            {
                Model.UpdateProperty(this, "Code", _Code, value, typeof(String), null, false);
                _Code=value;
            }
        }
        #endregion
        #region [Header] Наименование
        private String _Header = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Header
        {
            get 
            {
                return _Header;
            }
            set 
            {
                Model.UpdateProperty(this, "Header", _Header, value, typeof(String), null, false);
                _Header=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MUser] Справочник языков
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Справочник языков")]
    public partial class MUser: MEDMIdObj
    {
        #region [Login] Логин
        private String _Login = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Логин", Virtual =false)]
        
        public String Login
        {
            get 
            {
                return _Login;
            }
            set 
            {
                Model.UpdateProperty(this, "Login", _Login, value, typeof(String), null, false);
                _Login=value;
            }
        }
        #endregion
        #region [Fio] ФИО
        private String _Fio = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="ФИО", Virtual =false)]
        
        public String Fio
        {
            get 
            {
                return _Fio;
            }
            set 
            {
                Model.UpdateProperty(this, "Fio", _Fio, value, typeof(String), null, false);
                _Fio=value;
            }
        }
        #endregion
        #region [IsAdmin] Администратор
        private Boolean _IsAdmin = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Администратор", Virtual =false)]
        
        public Boolean IsAdmin
        {
            get 
            {
                return _IsAdmin;
            }
            set 
            {
                Model.UpdateProperty(this, "IsAdmin", _IsAdmin, value, typeof(Boolean), null, false);
                _IsAdmin=value;
            }
        }
        #endregion
        #region [IsEditor] Разрешена правка статей
        private Boolean _IsEditor = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешена правка статей", Virtual =false)]
        
        public Boolean IsEditor
        {
            get 
            {
                return _IsEditor;
            }
            set 
            {
                Model.UpdateProperty(this, "IsEditor", _IsEditor, value, typeof(Boolean), null, false);
                _IsEditor=value;
            }
        }
        #endregion
        #region [IsModerator] Разрешена правка и перевод чужих статей
        private Boolean _IsModerator = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешена правка и перевод чужих статей", Virtual =false)]
        
        public Boolean IsModerator
        {
            get 
            {
                return _IsModerator;
            }
            set 
            {
                Model.UpdateProperty(this, "IsModerator", _IsModerator, value, typeof(Boolean), null, false);
                _IsModerator=value;
            }
        }
        #endregion
        #region [IsREditor] Разрешена правка справочников
        private Boolean _IsREditor = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешена правка справочников ", Virtual =false)]
        
        public Boolean IsREditor
        {
            get 
            {
                return _IsREditor;
            }
            set 
            {
                Model.UpdateProperty(this, "IsREditor", _IsREditor, value, typeof(Boolean), null, false);
                _IsREditor=value;
            }
        }
        #endregion
        #region [IsTranslator] Разрешен паревод
        private Boolean _IsTranslator = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешен паревод", Virtual =false)]
        
        public Boolean IsTranslator
        {
            get 
            {
                return _IsTranslator;
            }
            set 
            {
                Model.UpdateProperty(this, "IsTranslator", _IsTranslator, value, typeof(Boolean), null, false);
                _IsTranslator=value;
            }
        }
        #endregion
        #region [IsFinder] Разрешен мониторинг
        private Boolean _IsFinder = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешен мониторинг", Virtual =false)]
        
        public Boolean IsFinder
        {
            get 
            {
                return _IsFinder;
            }
            set 
            {
                Model.UpdateProperty(this, "IsFinder", _IsFinder, value, typeof(Boolean), null, false);
                _IsFinder=value;
            }
        }
        #endregion
        #region [IsViewer] Разрешен просмотр
        private Boolean _IsViewer = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешен просмотр", Virtual =false)]
        
        public Boolean IsViewer
        {
            get 
            {
                return _IsViewer;
            }
            set 
            {
                Model.UpdateProperty(this, "IsViewer", _IsViewer, value, typeof(Boolean), null, false);
                _IsViewer=value;
            }
        }
        #endregion
        #region [PassCode] Код
        private Int32 _PassCode = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Код", Virtual =false)]
        
        public Int32 PassCode
        {
            get 
            {
                return _PassCode;
            }
            set 
            {
                Model.UpdateProperty(this, "PassCode", _PassCode, value, typeof(Int32), null, false);
                _PassCode=value;
            }
        }
        #endregion
        #region [IsDismiss] Уволен
        private Boolean _IsDismiss = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Уволен", Virtual =false)]
        
        public Boolean IsDismiss
        {
            get 
            {
                return _IsDismiss;
            }
            set 
            {
                Model.UpdateProperty(this, "IsDismiss", _IsDismiss, value, typeof(Boolean), null, false);
                _IsDismiss=value;
            }
        }
        #endregion
    }
    #endregion
}
#endregion
