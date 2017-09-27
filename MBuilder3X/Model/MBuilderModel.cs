//--------------------------------------------------------------------------------
// MBuilder3X Генератор модели данных (классы)                (13.07.2018 15:25:53)
//--------------------------------------------------------------------------------
using System;
using MEDMCoreLibrary;
using System.Collections.Generic;
#region [MBuilderModel] MBuilder
namespace Models.MBuilderModel
{
    public partial class MBuilderModel : MEDMSql
    {
        #region Конструктор
        public MBuilderModel()
        {
            MEntityDic.FindObj(null);
            MClsDic.FindObj(null);
            MRevisionDic.FindObj(null);
            MRelationDic.FindObj(null);
            MStatusDic.FindObj(null);
            MArtDic.FindObj(null);
            MLogDic.FindObj(null);
            MClsTypeDic.FindObj(null);
            MEntityTypeDic.FindObj(null);
            MRelationTypeDic.FindObj(null);
            MArtTypeDic.FindObj(null);
            MPropTypeDic.FindObj(null);
            MStatusTypeDic.FindObj(null);
            MUnitTypeDic.FindObj(null);
            MViewerTypeDic.FindObj(null);
            MEditTypeDic.FindObj(null);
            MPasswordDic.FindObj(null);
        }
        #endregion
        #region Словарь
        #region [MEntity] Сущность
        private MEDMObjectDic _MEntityDic = null;
        public MEDMObjectDic MEntityDic
        {
            get
            {
                if (_MEntityDic == null) _MEntityDic = new MEDMObjectDic(this, typeof(MEntity));
                if (!MainDic.ContainsKey(typeof(MEntity))) MainDic[typeof(MEntity)] = _MEntityDic;
                return _MEntityDic;
            }
        }
        #endregion
        #region [MCls] Классификатор
        private MEDMObjectDic _MClsDic = null;
        public MEDMObjectDic MClsDic
        {
            get
            {
                if (_MClsDic == null) _MClsDic = new MEDMObjectDic(this, typeof(MCls));
                if (!MainDic.ContainsKey(typeof(MCls))) MainDic[typeof(MCls)] = _MClsDic;
                return _MClsDic;
            }
        }
        #endregion
        #region [MRevision] Ревизия
        private MEDMObjectDic _MRevisionDic = null;
        public MEDMObjectDic MRevisionDic
        {
            get
            {
                if (_MRevisionDic == null) _MRevisionDic = new MEDMObjectDic(this, typeof(MRevision));
                if (!MainDic.ContainsKey(typeof(MRevision))) MainDic[typeof(MRevision)] = _MRevisionDic;
                return _MRevisionDic;
            }
        }
        #endregion
        #region [MRelation] Каталог
        private MEDMObjectDic _MRelationDic = null;
        public MEDMObjectDic MRelationDic
        {
            get
            {
                if (_MRelationDic == null) _MRelationDic = new MEDMObjectDic(this, typeof(MRelation));
                if (!MainDic.ContainsKey(typeof(MRelation))) MainDic[typeof(MRelation)] = _MRelationDic;
                return _MRelationDic;
            }
        }
        #endregion
        #region [MStatus] Статус
        private MEDMObjectDic _MStatusDic = null;
        public MEDMObjectDic MStatusDic
        {
            get
            {
                if (_MStatusDic == null) _MStatusDic = new MEDMObjectDic(this, typeof(MStatus));
                if (!MainDic.ContainsKey(typeof(MStatus))) MainDic[typeof(MStatus)] = _MStatusDic;
                return _MStatusDic;
            }
        }
        #endregion
        #region [MArt] Статья
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
        #region [MLog] Журнал
        private MEDMObjectDic _MLogDic = null;
        public MEDMObjectDic MLogDic
        {
            get
            {
                if (_MLogDic == null) _MLogDic = new MEDMObjectDic(this, typeof(MLog));
                if (!MainDic.ContainsKey(typeof(MLog))) MainDic[typeof(MLog)] = _MLogDic;
                return _MLogDic;
            }
        }
        #endregion
        #region [MClsType] Тип классификатора
        private static MEDMObjectDic _MClsTypeDic = null;
        public MEDMObjectDic MClsTypeDic
        {
            get
            {
                if (_MClsTypeDic == null) _MClsTypeDic = new MEDMObjectDic(this, typeof(MClsType));
                if (!MainDic.ContainsKey(typeof(MClsType))) MainDic[typeof(MClsType)] = _MClsTypeDic;
                return _MClsTypeDic;
            }
        }
        #endregion
        #region [MEntityType] Тип сущности
        private static MEDMObjectDic _MEntityTypeDic = null;
        public MEDMObjectDic MEntityTypeDic
        {
            get
            {
                if (_MEntityTypeDic == null) _MEntityTypeDic = new MEDMObjectDic(this, typeof(MEntityType));
                if (!MainDic.ContainsKey(typeof(MEntityType))) MainDic[typeof(MEntityType)] = _MEntityTypeDic;
                return _MEntityTypeDic;
            }
        }
        #endregion
        #region [MRelationType] Тип связи
        private static MEDMObjectDic _MRelationTypeDic = null;
        public MEDMObjectDic MRelationTypeDic
        {
            get
            {
                if (_MRelationTypeDic == null) _MRelationTypeDic = new MEDMObjectDic(this, typeof(MRelationType));
                if (!MainDic.ContainsKey(typeof(MRelationType))) MainDic[typeof(MRelationType)] = _MRelationTypeDic;
                return _MRelationTypeDic;
            }
        }
        #endregion
        #region [MArtType] Тип статьи
        private static MEDMObjectDic _MArtTypeDic = null;
        public MEDMObjectDic MArtTypeDic
        {
            get
            {
                if (_MArtTypeDic == null) _MArtTypeDic = new MEDMObjectDic(this, typeof(MArtType));
                if (!MainDic.ContainsKey(typeof(MArtType))) MainDic[typeof(MArtType)] = _MArtTypeDic;
                return _MArtTypeDic;
            }
        }
        #endregion
        #region [MPropType] Тип свойства
        private static MEDMObjectDic _MPropTypeDic = null;
        public MEDMObjectDic MPropTypeDic
        {
            get
            {
                if (_MPropTypeDic == null) _MPropTypeDic = new MEDMObjectDic(this, typeof(MPropType));
                if (!MainDic.ContainsKey(typeof(MPropType))) MainDic[typeof(MPropType)] = _MPropTypeDic;
                return _MPropTypeDic;
            }
        }
        #endregion
        #region [MStatusType] Тип статуса
        private static MEDMObjectDic _MStatusTypeDic = null;
        public MEDMObjectDic MStatusTypeDic
        {
            get
            {
                if (_MStatusTypeDic == null) _MStatusTypeDic = new MEDMObjectDic(this, typeof(MStatusType));
                if (!MainDic.ContainsKey(typeof(MStatusType))) MainDic[typeof(MStatusType)] = _MStatusTypeDic;
                return _MStatusTypeDic;
            }
        }
        #endregion
        #region [MUnitType] Тип единицы измерения
        private static MEDMObjectDic _MUnitTypeDic = null;
        public MEDMObjectDic MUnitTypeDic
        {
            get
            {
                if (_MUnitTypeDic == null) _MUnitTypeDic = new MEDMObjectDic(this, typeof(MUnitType));
                if (!MainDic.ContainsKey(typeof(MUnitType))) MainDic[typeof(MUnitType)] = _MUnitTypeDic;
                return _MUnitTypeDic;
            }
        }
        #endregion
        #region [MViewerType] Тип просмотрщика
        private static MEDMObjectDic _MViewerTypeDic = null;
        public MEDMObjectDic MViewerTypeDic
        {
            get
            {
                if (_MViewerTypeDic == null) _MViewerTypeDic = new MEDMObjectDic(this, typeof(MViewerType));
                if (!MainDic.ContainsKey(typeof(MViewerType))) MainDic[typeof(MViewerType)] = _MViewerTypeDic;
                return _MViewerTypeDic;
            }
        }
        #endregion
        #region [MEditType] Тип просмотрщика
        private static MEDMObjectDic _MEditTypeDic = null;
        public MEDMObjectDic MEditTypeDic
        {
            get
            {
                if (_MEditTypeDic == null) _MEditTypeDic = new MEDMObjectDic(this, typeof(MEditType));
                if (!MainDic.ContainsKey(typeof(MEditType))) MainDic[typeof(MEditType)] = _MEditTypeDic;
                return _MEditTypeDic;
            }
        }
        #endregion
        #region [MPassword] Пароль пользователя
        private MEDMObjectDic _MPasswordDic = null;
        public MEDMObjectDic MPasswordDic
        {
            get
            {
                if (_MPasswordDic == null) _MPasswordDic = new MEDMObjectDic(this, typeof(MPassword));
                if (!MainDic.ContainsKey(typeof(MPassword))) MainDic[typeof(MPassword)] = _MPasswordDic;
                return _MPasswordDic;
            }
        }
        #endregion
        #endregion
    }
    #region [MEntity] Сущность
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Сущность")]
    public partial class MEntity: MEDMIdObj
    {
        #region [GID] Глобальный ID
        private Guid _GID = default(Guid);
        [DbColumn(Type = "uniqueidentifier", Def ="newid()", Virtual =false)]
        [EDMProperty(PropType="GUID", ItemType=typeof(Guid), Header="Глобальный ID", Virtual =false)]
        [Ignore]
        public Guid GID
        {
            get 
            {
                return _GID;
            }
            set 
            {
                Model.UpdateProperty(this, "GID", _GID, value, typeof(Guid), null, false);
                _GID=value;
            }
        }
        #endregion
        #region [EntityType] Тип сущности
        private String _EntityTypeId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="CFG", ItemType=typeof(String), Header="Тип сущности (Id)", Virtual =false)]
        
        public String EntityTypeId
        {
            get 
            {
                return _EntityTypeId;
            }
            set 
            {
                Model.UpdateProperty(this, "EntityTypeId", _EntityTypeId, value, typeof(String), typeof(MEntityType), false);
                _EntityTypeId=value;
            }
        }
        private MEntityType _EntityType = default(MEntityType);
        [EDMProperty(PropType="CFG", ItemType=typeof(MEntityType), Header="Тип сущности", Virtual =false)]
        
        public MEntityType EntityType
        {
            get 
            {
                if (_EntityType==null && _EntityTypeId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MEntityType), "MEntityType", "Id", "System.String");
                    _EntityType = Model.MainDic.GetObj<MEntityType>(EntityTypeId);
                }
                return _EntityType;
            }
        }
        //public bool ShouldSerializeMEntityType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Cls] Класс
        private Int32 _ClsId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Класс (Id)", Virtual =false)]
        
        public Int32 ClsId
        {
            get 
            {
                return _ClsId;
            }
            set 
            {
                Model.UpdateProperty(this, "ClsId", _ClsId, value, typeof(Int32), typeof(MCls), false);
                _ClsId=value;
            }
        }
        private MCls _Cls = default(MCls);
        [EDMProperty(PropType="REF", ItemType=typeof(MCls), Header="Класс", Virtual =false)]
        
        public MCls Cls
        {
            get 
            {
                if (_Cls==null && _ClsId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MCls), "MCls", "Id", "System.Int32");
                    _Cls = Model.MainDic.GetObj<MCls>(ClsId);
                }
                return _Cls;
            }
        }
        //public bool ShouldSerializeMCls()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Prj] Код проекта
        private String _Prj = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Код проекта", Virtual =false)]
        
        public String Prj
        {
            get 
            {
                return _Prj;
            }
            set 
            {
                Model.UpdateProperty(this, "Prj", _Prj, value, typeof(String), null, false);
                _Prj=value;
            }
        }
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
        #region [Name] Наименование
        private String _Name = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Name
        {
            get 
            {
                return _Name;
            }
            set 
            {
                Model.UpdateProperty(this, "Name", _Name, value, typeof(String), null, false);
                _Name=value;
            }
        }
        #endregion
        #region [Description] Описание
        private String _Description = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Описание", Virtual =false)]
        
        public String Description
        {
            get 
            {
                return _Description;
            }
            set 
            {
                Model.UpdateProperty(this, "Description", _Description, value, typeof(String), null, false);
                _Description=value;
            }
        }
        #endregion
        #region [Rem] Примечания
        private String _Rem = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Примечания", Virtual =false)]
        
        public String Rem
        {
            get 
            {
                return _Rem;
            }
            set 
            {
                Model.UpdateProperty(this, "Rem", _Rem, value, typeof(String), null, false);
                _Rem=value;
            }
        }
        #endregion
        #region [Props] Свойства
        private string _SProps = default(string);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="PROPS", ItemType=typeof(String), Header="Свойства", Virtual =false)]
        
        public string SProps
        {
            get 
            {
                return _SProps;
            }
            set 
            {
                Model.UpdateProperty(this, "SProps", _SProps, value, typeof(string), null, false);
                _SProps=value;
            }
        }
        private MProps _Props = null;
        [Ignore]
        
        public MProps Props
        {
            get 
            {
                if (_Props==null) _Props= new MProps(this, "SProps");    
                return _Props;
            }
        }
        #endregion
        #region [Status] Статусы
        private Int32 _Status = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Статусы", Virtual =false)]
        
        public Int32 Status
        {
            get 
            {
                return _Status;
            }
            set 
            {
                Model.UpdateProperty(this, "Status", _Status, value, typeof(Int32), null, false);
                _Status=value;
            }
        }
        #endregion
        #region [Flags] Признаки
        private Int32 _Flags = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Признаки", Virtual =false)]
        
        public Int32 Flags
        {
            get 
            {
                return _Flags;
            }
            set 
            {
                Model.UpdateProperty(this, "Flags", _Flags, value, typeof(Int32), null, false);
                _Flags=value;
            }
        }
        #endregion
        #region [Arts] Список статей
        private List<MArt> _Arts = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MArt), Header="Список статей", Virtual =false)]
        
        public List<MArt> Arts
        {
            get 
            {
                if (_Arts==null)
                {
                    _Arts = new List<MArt>();
                }
                return _Arts;
            }
        }
        
        #endregion
        #region [IsPin] Избранная сущность
        private Boolean _IsPin = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =true)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Избранная сущность", Virtual =true)]
        
        public Boolean IsPin
        {
            get 
            {
                return _IsPin;
            }
            set 
            {
                Model.UpdateProperty(this, "IsPin", _IsPin, value, typeof(Boolean), null, true);
                _IsPin=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MCls] Классификатор
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Классификатор")]
    public partial class MCls: MEDMIdObj
    {
        #region [GID] Глобальный ID
        private Guid _GID = default(Guid);
        [DbColumn(Type = "uniqueidentifier", Def ="newid()", Virtual =false)]
        [EDMProperty(PropType="GUID", ItemType=typeof(Guid), Header="Глобальный ID", Virtual =false)]
        [Ignore]
        public Guid GID
        {
            get 
            {
                return _GID;
            }
            set 
            {
                Model.UpdateProperty(this, "GID", _GID, value, typeof(Guid), null, false);
                _GID=value;
            }
        }
        #endregion
        #region [ClsType] Тип классификатора
        private String _ClsTypeId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="CFG", ItemType=typeof(String), Header="Тип классификатора (Id)", Virtual =false)]
        
        public String ClsTypeId
        {
            get 
            {
                return _ClsTypeId;
            }
            set 
            {
                Model.UpdateProperty(this, "ClsTypeId", _ClsTypeId, value, typeof(String), typeof(MClsType), false);
                _ClsTypeId=value;
            }
        }
        private MClsType _ClsType = default(MClsType);
        [EDMProperty(PropType="CFG", ItemType=typeof(MClsType), Header="Тип классификатора", Virtual =false)]
        
        public MClsType ClsType
        {
            get 
            {
                if (_ClsType==null && _ClsTypeId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MClsType), "MClsType", "Id", "System.String");
                    _ClsType = Model.MainDic.GetObj<MClsType>(ClsTypeId);
                }
                return _ClsType;
            }
        }
        //public bool ShouldSerializeMClsType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Parent] Класс
        private Int32 _ParentId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Класс (Id)", Virtual =false)]
        
        public Int32 ParentId
        {
            get 
            {
                return _ParentId;
            }
            set 
            {
                Model.UpdateProperty(this, "ParentId", _ParentId, value, typeof(Int32), typeof(MCls), false);
                _ParentId=value;
            }
        }
        private MCls _Parent = default(MCls);
        [EDMProperty(PropType="REF", ItemType=typeof(MCls), Header="Класс", Virtual =false)]
        
        public MCls Parent
        {
            get 
            {
                if (_Parent==null && _ParentId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MCls), "MCls", "Id", "System.Int32");
                    _Parent = Model.MainDic.GetObj<MCls>(ParentId);
                }
                return _Parent;
            }
        }
        //public bool ShouldSerializeMCls()
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
        #region [Name] Наименование
        private String _Name = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Наименование", Virtual =false)]
        
        public String Name
        {
            get 
            {
                return _Name;
            }
            set 
            {
                Model.UpdateProperty(this, "Name", _Name, value, typeof(String), null, false);
                _Name=value;
            }
        }
        #endregion
        #region [Description] Описание
        private String _Description = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Описание", Virtual =false)]
        
        public String Description
        {
            get 
            {
                return _Description;
            }
            set 
            {
                Model.UpdateProperty(this, "Description", _Description, value, typeof(String), null, false);
                _Description=value;
            }
        }
        #endregion
        #region [Rem] Примечания
        private String _Rem = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Примечания", Virtual =false)]
        
        public String Rem
        {
            get 
            {
                return _Rem;
            }
            set 
            {
                Model.UpdateProperty(this, "Rem", _Rem, value, typeof(String), null, false);
                _Rem=value;
            }
        }
        #endregion
        #region [Props] Свойства
        private string _SProps = default(string);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="PROPS", ItemType=typeof(String), Header="Свойства", Virtual =false)]
        
        public string SProps
        {
            get 
            {
                return _SProps;
            }
            set 
            {
                Model.UpdateProperty(this, "SProps", _SProps, value, typeof(string), null, false);
                _SProps=value;
            }
        }
        private MProps _Props = null;
        [Ignore]
        
        public MProps Props
        {
            get 
            {
                if (_Props==null) _Props= new MProps(this, "SProps");    
                return _Props;
            }
        }
        #endregion
        #region [webix_kids] Есть дети
        private Boolean _webix_kids = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =true)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Есть дети", Virtual =true)]
        
        public Boolean webix_kids
        {
            get 
            {
                return _webix_kids;
            }
            set 
            {
                Model.UpdateProperty(this, "webix_kids", _webix_kids, value, typeof(Boolean), null, true);
                _webix_kids=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MRevision] Ревизия
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Ревизия")]
    public partial class MRevision: MEDMIdObj
    {
        #region [GID] Глобальный ID
        private Guid _GID = default(Guid);
        [DbColumn(Type = "uniqueidentifier", Def ="newid()", Virtual =false)]
        [EDMProperty(PropType="GUID", ItemType=typeof(Guid), Header="Глобальный ID", Virtual =false)]
        [Ignore]
        public Guid GID
        {
            get 
            {
                return _GID;
            }
            set 
            {
                Model.UpdateProperty(this, "GID", _GID, value, typeof(Guid), null, false);
                _GID=value;
            }
        }
        #endregion
        #region [Revision] Номер ревизии
        private String _Revision = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Номер ревизии", Virtual =false)]
        
        public String Revision
        {
            get 
            {
                return _Revision;
            }
            set 
            {
                Model.UpdateProperty(this, "Revision", _Revision, value, typeof(String), null, false);
                _Revision=value;
            }
        }
        #endregion
        #region [RevisionEntity] Описатель ревизии
        private Int32 _RevisionEntityId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Описатель ревизии (Id)", Virtual =false)]
        
        public Int32 RevisionEntityId
        {
            get 
            {
                return _RevisionEntityId;
            }
            set 
            {
                Model.UpdateProperty(this, "RevisionEntityId", _RevisionEntityId, value, typeof(Int32), typeof(MEntity), false);
                _RevisionEntityId=value;
            }
        }
        private MEntity _RevisionEntity = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Описатель ревизии", Virtual =false)]
        
        public MEntity RevisionEntity
        {
            get 
            {
                if (_RevisionEntity==null && _RevisionEntityId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _RevisionEntity = Model.MainDic.GetObj<MEntity>(RevisionEntityId);
                }
                return _RevisionEntity;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [ChildEntity] Дочерняя cущноcть
        private Int32 _ChildEntityId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Дочерняя cущноcть (Id)", Virtual =false)]
        
        public Int32 ChildEntityId
        {
            get 
            {
                return _ChildEntityId;
            }
            set 
            {
                Model.UpdateProperty(this, "ChildEntityId", _ChildEntityId, value, typeof(Int32), typeof(MEntity), false);
                _ChildEntityId=value;
            }
        }
        private MEntity _ChildEntity = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Дочерняя cущноcть", Virtual =false)]
        
        public MEntity ChildEntity
        {
            get 
            {
                if (_ChildEntity==null && _ChildEntityId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _ChildEntity = Model.MainDic.GetObj<MEntity>(ChildEntityId);
                }
                return _ChildEntity;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [MasterEntity] Главная сущноcть
        private Int32 _MasterEntityId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Главная сущноcть (Id)", Virtual =false)]
        
        public Int32 MasterEntityId
        {
            get 
            {
                return _MasterEntityId;
            }
            set 
            {
                Model.UpdateProperty(this, "MasterEntityId", _MasterEntityId, value, typeof(Int32), typeof(MEntity), false);
                _MasterEntityId=value;
            }
        }
        private MEntity _MasterEntity = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Главная сущноcть", Virtual =false)]
        
        public MEntity MasterEntity
        {
            get 
            {
                if (_MasterEntity==null && _MasterEntityId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _MasterEntity = Model.MainDic.GetObj<MEntity>(MasterEntityId);
                }
                return _MasterEntity;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
    }
    #endregion
    #region [MRelation] Каталог
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Каталог")]
    public partial class MRelation: MEDMIdObj
    {
        #region [GID] Глобальный ID
        private Guid _GID = default(Guid);
        [DbColumn(Type = "uniqueidentifier", Def ="newid()", Virtual =false)]
        [EDMProperty(PropType="GUID", ItemType=typeof(Guid), Header="Глобальный ID", Virtual =false)]
        [Ignore]
        public Guid GID
        {
            get 
            {
                return _GID;
            }
            set 
            {
                Model.UpdateProperty(this, "GID", _GID, value, typeof(Guid), null, false);
                _GID=value;
            }
        }
        #endregion
        #region [RelationType] Тип связи
        private String _RelationTypeId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="CFG", ItemType=typeof(String), Header="Тип связи (Id)", Virtual =false)]
        
        public String RelationTypeId
        {
            get 
            {
                return _RelationTypeId;
            }
            set 
            {
                Model.UpdateProperty(this, "RelationTypeId", _RelationTypeId, value, typeof(String), typeof(MRelationType), false);
                _RelationTypeId=value;
            }
        }
        private MRelationType _RelationType = default(MRelationType);
        [EDMProperty(PropType="CFG", ItemType=typeof(MRelationType), Header="Тип связи", Virtual =false)]
        
        public MRelationType RelationType
        {
            get 
            {
                if (_RelationType==null && _RelationTypeId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MRelationType), "MRelationType", "Id", "System.String");
                    _RelationType = Model.MainDic.GetObj<MRelationType>(RelationTypeId);
                }
                return _RelationType;
            }
        }
        //public bool ShouldSerializeMRelationType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Owner] Cущноcть владелец
        private Int32 _OwnerId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Cущноcть владелец (Id)", Virtual =false)]
        
        public Int32 OwnerId
        {
            get 
            {
                return _OwnerId;
            }
            set 
            {
                Model.UpdateProperty(this, "OwnerId", _OwnerId, value, typeof(Int32), typeof(MEntity), false);
                _OwnerId=value;
            }
        }
        private MEntity _Owner = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Cущноcть владелец", Virtual =false)]
        
        public MEntity Owner
        {
            get 
            {
                if (_Owner==null && _OwnerId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _Owner = Model.MainDic.GetObj<MEntity>(OwnerId);
                }
                return _Owner;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Parent] Родительская cущноcть
        private Int32 _ParentId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Родительская cущноcть (Id)", Virtual =false)]
        
        public Int32 ParentId
        {
            get 
            {
                return _ParentId;
            }
            set 
            {
                Model.UpdateProperty(this, "ParentId", _ParentId, value, typeof(Int32), typeof(MEntity), false);
                _ParentId=value;
            }
        }
        private MEntity _Parent = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Родительская cущноcть", Virtual =false)]
        
        public MEntity Parent
        {
            get 
            {
                if (_Parent==null && _ParentId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _Parent = Model.MainDic.GetObj<MEntity>(ParentId);
                }
                return _Parent;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Child] Дочерняя сущноcть
        private Int32 _ChildId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Дочерняя сущноcть (Id)", Virtual =false)]
        
        public Int32 ChildId
        {
            get 
            {
                return _ChildId;
            }
            set 
            {
                Model.UpdateProperty(this, "ChildId", _ChildId, value, typeof(Int32), typeof(MEntity), false);
                _ChildId=value;
            }
        }
        private MEntity _Child = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Дочерняя сущноcть", Virtual =false)]
        
        public MEntity Child
        {
            get 
            {
                if (_Child==null && _ChildId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _Child = Model.MainDic.GetObj<MEntity>(ChildId);
                }
                return _Child;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Index] Индекс
        private Int32 _Index = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Индекс", Virtual =false)]
        
        public Int32 Index
        {
            get 
            {
                return _Index;
            }
            set 
            {
                Model.UpdateProperty(this, "Index", _Index, value, typeof(Int32), null, false);
                _Index=value;
            }
        }
        #endregion
        #region [Group] Номер группы
        private Int32 _Group = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Номер группы", Virtual =false)]
        
        public Int32 Group
        {
            get 
            {
                return _Group;
            }
            set 
            {
                Model.UpdateProperty(this, "Group", _Group, value, typeof(Int32), null, false);
                _Group=value;
            }
        }
        #endregion
        #region [Change] Номер взаимозаменяемости
        private Int32 _Change = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Номер взаимозаменяемости", Virtual =false)]
        
        public Int32 Change
        {
            get 
            {
                return _Change;
            }
            set 
            {
                Model.UpdateProperty(this, "Change", _Change, value, typeof(Int32), null, false);
                _Change=value;
            }
        }
        #endregion
        #region [Flags] Признаки
        private Int32 _Flags = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Признаки", Virtual =false)]
        
        public Int32 Flags
        {
            get 
            {
                return _Flags;
            }
            set 
            {
                Model.UpdateProperty(this, "Flags", _Flags, value, typeof(Int32), null, false);
                _Flags=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MStatus] Статус
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Статус")]
    public partial class MStatus: MEDMIdObj
    {
        #region [Entity] Cущноcть
        private Int32 _EntityId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Cущноcть (Id)", Virtual =false)]
        
        public Int32 EntityId
        {
            get 
            {
                return _EntityId;
            }
            set 
            {
                Model.UpdateProperty(this, "EntityId", _EntityId, value, typeof(Int32), typeof(MEntity), false);
                _EntityId=value;
            }
        }
        private MEntity _Entity = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Cущноcть", Virtual =false)]
        
        public MEntity Entity
        {
            get 
            {
                if (_Entity==null && _EntityId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _Entity = Model.MainDic.GetObj<MEntity>(EntityId);
                }
                return _Entity;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Status] Статус
        private String _StatusId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="CFG", ItemType=typeof(String), Header="Статус (Id)", Virtual =false)]
        
        public String StatusId
        {
            get 
            {
                return _StatusId;
            }
            set 
            {
                Model.UpdateProperty(this, "StatusId", _StatusId, value, typeof(String), typeof(MStatusType), false);
                _StatusId=value;
            }
        }
        private MStatusType _Status = default(MStatusType);
        [EDMProperty(PropType="CFG", ItemType=typeof(MStatusType), Header="Статус", Virtual =false)]
        
        public MStatusType Status
        {
            get 
            {
                if (_Status==null && _StatusId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MStatusType), "MStatusType", "Id", "System.String");
                    _Status = Model.MainDic.GetObj<MStatusType>(StatusId);
                }
                return _Status;
            }
        }
        //public bool ShouldSerializeMStatusType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [User] Пользователь
        private Int32 _UserId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Пользователь (Id)", Virtual =false)]
        
        public Int32 UserId
        {
            get 
            {
                return _UserId;
            }
            set 
            {
                Model.UpdateProperty(this, "UserId", _UserId, value, typeof(Int32), typeof(MEntity), false);
                _UserId=value;
            }
        }
        private MEntity _User = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Пользователь", Virtual =false)]
        
        public MEntity User
        {
            get 
            {
                if (_User==null && _UserId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _User = Model.MainDic.GetObj<MEntity>(UserId);
                }
                return _User;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Date] Дата
        private DateTime _Date = default(DateTime);
        [DbColumn(Type = "datetime", Def ="'17530101'", Virtual =false)]
        [EDMProperty(PropType="DATE", ItemType=typeof(DateTime), Header="Дата", Virtual =false)]
        
        public DateTime Date
        {
            get 
            {
                return _Date;
            }
            set 
            {
                Model.UpdateProperty(this, "Date", _Date, value, typeof(DateTime), null, false);
                _Date=value;
            }
        }
        #endregion
        #region [Rem] Примечания
        private String _Rem = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Примечания", Virtual =false)]
        
        public String Rem
        {
            get 
            {
                return _Rem;
            }
            set 
            {
                Model.UpdateProperty(this, "Rem", _Rem, value, typeof(String), null, false);
                _Rem=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MArt] Статья
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Статья")]
    public partial class MArt: MEDMIdObj
    {
        #region [GID] Глобальный ID
        private Guid _GID = default(Guid);
        [DbColumn(Type = "uniqueidentifier", Def ="newid()", Virtual =false)]
        [EDMProperty(PropType="GUID", ItemType=typeof(Guid), Header="Глобальный ID", Virtual =false)]
        [Ignore]
        public Guid GID
        {
            get 
            {
                return _GID;
            }
            set 
            {
                Model.UpdateProperty(this, "GID", _GID, value, typeof(Guid), null, false);
                _GID=value;
            }
        }
        #endregion
        #region [ArtType] Тип статьи
        private String _ArtTypeId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="CFG", ItemType=typeof(String), Header="Тип статьи (Id)", Virtual =false)]
        
        public String ArtTypeId
        {
            get 
            {
                return _ArtTypeId;
            }
            set 
            {
                Model.UpdateProperty(this, "ArtTypeId", _ArtTypeId, value, typeof(String), typeof(MArtType), false);
                _ArtTypeId=value;
            }
        }
        private MArtType _ArtType = default(MArtType);
        [EDMProperty(PropType="CFG", ItemType=typeof(MArtType), Header="Тип статьи", Virtual =false)]
        
        public MArtType ArtType
        {
            get 
            {
                if (_ArtType==null && _ArtTypeId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MArtType), "MArtType", "Id", "System.String");
                    _ArtType = Model.MainDic.GetObj<MArtType>(ArtTypeId);
                }
                return _ArtType;
            }
        }
        //public bool ShouldSerializeMArtType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Entity] Cущноcть
        private Int32 _EntityId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Cущноcть (Id)", Virtual =false)]
        
        public Int32 EntityId
        {
            get 
            {
                return _EntityId;
            }
            set 
            {
                Model.UpdateProperty(this, "EntityId", _EntityId, value, typeof(Int32), typeof(MEntity), false);
                _EntityId=value;
            }
        }
        private MEntity _Entity = default(MEntity);
        [EDMProperty(PropType="REF", ItemType=typeof(MEntity), Header="Cущноcть", Virtual =false)]
        
        public MEntity Entity
        {
            get 
            {
                if (_Entity==null && _EntityId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MEntity), "MEntity", "Id", "System.Int32");
                    _Entity = Model.MainDic.GetObj<MEntity>(EntityId);
                }
                return _Entity;
            }
        }
        //public bool ShouldSerializeMEntity()
        //{
        //    return false;
        //}
        
        #endregion
        #region [BaseArt] Базовая статья
        private Int32 _BaseArtId = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(Int32), Header="Базовая статья (Id)", Virtual =false)]
        
        public Int32 BaseArtId
        {
            get 
            {
                return _BaseArtId;
            }
            set 
            {
                Model.UpdateProperty(this, "BaseArtId", _BaseArtId, value, typeof(Int32), typeof(MArt), false);
                _BaseArtId=value;
            }
        }
        private MArt _BaseArt = default(MArt);
        [EDMProperty(PropType="REF", ItemType=typeof(MArt), Header="Базовая статья", Virtual =false)]
        
        public MArt BaseArt
        {
            get 
            {
                if (_BaseArt==null && _BaseArtId!=default(Int32))
                {
                    Model.UpdateFutureRef(typeof(MArt), "MArt", "Id", "System.Int32");
                    _BaseArt = Model.MainDic.GetObj<MArt>(BaseArtId);
                }
                return _BaseArt;
            }
        }
        //public bool ShouldSerializeMArt()
        //{
        //    return false;
        //}
        
        #endregion
        #region [FileName] Имя файла
        private String _FileName = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Имя файла", Virtual =false)]
        
        public String FileName
        {
            get 
            {
                return _FileName;
            }
            set 
            {
                Model.UpdateProperty(this, "FileName", _FileName, value, typeof(String), null, false);
                _FileName=value;
            }
        }
        #endregion
        #region [Type] Тип файла(расширение)
        private String _Type = default(String);
        [DbColumn(Type = "nvarchar(32)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Тип файла(расширение)", Virtual =false)]
        
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
        #region [Vids] Виды
        private String _Vids = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Виды", Virtual =false)]
        
        public String Vids
        {
            get 
            {
                return _Vids;
            }
            set 
            {
                Model.UpdateProperty(this, "Vids", _Vids, value, typeof(String), null, false);
                _Vids=value;
            }
        }
        #endregion
        #region [Flags] Признаки
        private Int32 _Flags = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Признаки", Virtual =false)]
        
        public Int32 Flags
        {
            get 
            {
                return _Flags;
            }
            set 
            {
                Model.UpdateProperty(this, "Flags", _Flags, value, typeof(Int32), null, false);
                _Flags=value;
            }
        }
        #endregion
        #region [Index] Индекс
        private Int32 _Index = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Индекс", Virtual =false)]
        
        public Int32 Index
        {
            get 
            {
                return _Index;
            }
            set 
            {
                Model.UpdateProperty(this, "Index", _Index, value, typeof(Int32), null, false);
                _Index=value;
            }
        }
        #endregion
        #region [Length] Длина
        private Int32 _Length = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Длина", Virtual =false)]
        
        public Int32 Length
        {
            get 
            {
                return _Length;
            }
            set 
            {
                Model.UpdateProperty(this, "Length", _Length, value, typeof(Int32), null, false);
                _Length=value;
            }
        }
        #endregion
        #region [Version] Версия
        private Int32 _Version = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Версия", Virtual =false)]
        [Ignore]
        public Int32 Version
        {
            get 
            {
                return _Version;
            }
            set 
            {
                Model.UpdateProperty(this, "Version", _Version, value, typeof(Int32), null, false);
                _Version=value;
            }
        }
        #endregion
        #region [Props] Свойства
        private string _SProps = default(string);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="PROPS", ItemType=typeof(String), Header="Свойства", Virtual =false)]
        
        public string SProps
        {
            get 
            {
                return _SProps;
            }
            set 
            {
                Model.UpdateProperty(this, "SProps", _SProps, value, typeof(string), null, false);
                _SProps=value;
            }
        }
        private MProps _Props = null;
        [Ignore]
        
        public MProps Props
        {
            get 
            {
                if (_Props==null) _Props= new MProps(this, "SProps");    
                return _Props;
            }
        }
        #endregion
    }
    #endregion
    #region [MLog] Журнал
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Журнал")]
    public partial class MLog: MEDMIdObj
    {
    }
    #endregion
    #region [MCfgType] Базовый класс для конфигурации
    [DbTable(Type = "BASECLASS")]
    [EDMClass(Header = "Базовый класс для конфигурации")]
    public partial class MCfgType: MEDMCfgObj
    {
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
        #region [Description] Описание
        private String _Description = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Описание", Virtual =false)]
        
        public String Description
        {
            get 
            {
                return _Description;
            }
            set 
            {
                Model.UpdateProperty(this, "Description", _Description, value, typeof(String), null, false);
                _Description=value;
            }
        }
        #endregion
        #region [Rem] Примечания
        private String _Rem = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Примечания", Virtual =false)]
        
        public String Rem
        {
            get 
            {
                return _Rem;
            }
            set 
            {
                Model.UpdateProperty(this, "Rem", _Rem, value, typeof(String), null, false);
                _Rem=value;
            }
        }
        #endregion
        #region [Icon] Иконка
        private String _Icon = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Иконка", Virtual =false)]
        
        public String Icon
        {
            get 
            {
                return _Icon;
            }
            set 
            {
                Model.UpdateProperty(this, "Icon", _Icon, value, typeof(String), null, false);
                _Icon=value;
            }
        }
        #endregion
        #region [IsSystem] Системный тип
        private Boolean _IsSystem = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Системный тип", Virtual =false)]
        
        public Boolean IsSystem
        {
            get 
            {
                return _IsSystem;
            }
            set 
            {
                Model.UpdateProperty(this, "IsSystem", _IsSystem, value, typeof(Boolean), null, false);
                _IsSystem=value;
            }
        }
        #endregion
        public override string ToString()
        {
            return $"[{Name}] {Header}";
        }
    }
    #endregion
    #region [MClsType] Тип классификатора
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип классификатора")]
    public partial class MClsType: MCfgType
    {
    }
    #endregion
    #region [MEntityType] Тип сущности
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип сущности")]
    public partial class MEntityType: MCfgType
    {
        #region [GroupHeader] Групповое наименование
        private String _GroupHeader = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Групповое наименование", Virtual =false)]
        
        public String GroupHeader
        {
            get 
            {
                return _GroupHeader;
            }
            set 
            {
                Model.UpdateProperty(this, "GroupHeader", _GroupHeader, value, typeof(String), null, false);
                _GroupHeader=value;
            }
        }
        #endregion
        #region [NewHeader] Новое наименование
        private String _NewHeader = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Новое наименование", Virtual =false)]
        
        public String NewHeader
        {
            get 
            {
                return _NewHeader;
            }
            set 
            {
                Model.UpdateProperty(this, "NewHeader", _NewHeader, value, typeof(String), null, false);
                _NewHeader=value;
            }
        }
        #endregion
        #region [ViewerType] Вьювер
        private String _ViewerTypeId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="CFG", ItemType=typeof(String), Header="Вьювер (Id)", Virtual =false)]
        
        public String ViewerTypeId
        {
            get 
            {
                return _ViewerTypeId;
            }
            set 
            {
                Model.UpdateProperty(this, "ViewerTypeId", _ViewerTypeId, value, typeof(String), typeof(MViewerType), false);
                _ViewerTypeId=value;
            }
        }
        private MViewerType _ViewerType = default(MViewerType);
        [EDMProperty(PropType="CFG", ItemType=typeof(MViewerType), Header="Вьювер", Virtual =false)]
        
        public MViewerType ViewerType
        {
            get 
            {
                if (_ViewerType==null && _ViewerTypeId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MViewerType), "MViewerType", "Id", "System.String");
                    _ViewerType = Model.MainDic.GetObj<MViewerType>(ViewerTypeId);
                }
                return _ViewerType;
            }
        }
        //public bool ShouldSerializeMViewerType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [IsNotExpand] Не раскрывать в дереве
        private Boolean _IsNotExpand = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Не раскрывать в дереве", Virtual =false)]
        
        public Boolean IsNotExpand
        {
            get 
            {
                return _IsNotExpand;
            }
            set 
            {
                Model.UpdateProperty(this, "IsNotExpand", _IsNotExpand, value, typeof(Boolean), null, false);
                _IsNotExpand=value;
            }
        }
        #endregion
        #region [IsGroupChilds] Группировать содержимое по типу
        private Boolean _IsGroupChilds = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Группировать содержимое по типу", Virtual =false)]
        
        public Boolean IsGroupChilds
        {
            get 
            {
                return _IsGroupChilds;
            }
            set 
            {
                Model.UpdateProperty(this, "IsGroupChilds", _IsGroupChilds, value, typeof(Boolean), null, false);
                _IsGroupChilds=value;
            }
        }
        #endregion
        #region [IsDoubleChilds] Разрешать одинаковые сущности среди детей
        private Boolean _IsDoubleChilds = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Разрешать одинаковые сущности среди детей", Virtual =false)]
        
        public Boolean IsDoubleChilds
        {
            get 
            {
                return _IsDoubleChilds;
            }
            set 
            {
                Model.UpdateProperty(this, "IsDoubleChilds", _IsDoubleChilds, value, typeof(Boolean), null, false);
                _IsDoubleChilds=value;
            }
        }
        #endregion
        #region [IsHideCode] Скрывать код
        private Boolean _IsHideCode = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Скрывать код", Virtual =false)]
        
        public Boolean IsHideCode
        {
            get 
            {
                return _IsHideCode;
            }
            set 
            {
                Model.UpdateProperty(this, "IsHideCode", _IsHideCode, value, typeof(Boolean), null, false);
                _IsHideCode=value;
            }
        }
        #endregion
        #region [PropTypes] Список разрешенных типов свойств
        private List<MPropType> _PropTypes = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MPropType), Header="Список разрешенных типов свойств", Virtual =false)]
        
        public List<MPropType> PropTypes
        {
            get 
            {
                if (_PropTypes==null)
                {
                    _PropTypes = new List<MPropType>();
                }
                return _PropTypes;
            }
        }
        
        #endregion
    }
    #endregion
    #region [MRelationType] Тип связи
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип связи")]
    public partial class MRelationType: MCfgType
    {
        #region [GroupHeader] Групповое наименование
        private String _GroupHeader = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Групповое наименование", Virtual =false)]
        
        public String GroupHeader
        {
            get 
            {
                return _GroupHeader;
            }
            set 
            {
                Model.UpdateProperty(this, "GroupHeader", _GroupHeader, value, typeof(String), null, false);
                _GroupHeader=value;
            }
        }
        #endregion
        #region [NewHeader] Новое наименование
        private String _NewHeader = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Новое наименование", Virtual =false)]
        
        public String NewHeader
        {
            get 
            {
                return _NewHeader;
            }
            set 
            {
                Model.UpdateProperty(this, "NewHeader", _NewHeader, value, typeof(String), null, false);
                _NewHeader=value;
            }
        }
        #endregion
        #region [PropTypes] Список разрешенных типов свойств
        private List<MPropType> _PropTypes = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MPropType), Header="Список разрешенных типов свойств", Virtual =false)]
        
        public List<MPropType> PropTypes
        {
            get 
            {
                if (_PropTypes==null)
                {
                    _PropTypes = new List<MPropType>();
                }
                return _PropTypes;
            }
        }
        
        #endregion
        #region [OwnerTypes] Список разрешенных типов сущностей владельцев
        private List<MEntityType> _OwnerTypes = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MEntityType), Header="Список разрешенных типов сущностей владельцев", Virtual =false)]
        
        public List<MEntityType> OwnerTypes
        {
            get 
            {
                if (_OwnerTypes==null)
                {
                    _OwnerTypes = new List<MEntityType>();
                }
                return _OwnerTypes;
            }
        }
        
        #endregion
        #region [ChildTypes] Список разрешенных типов сущностей детей
        private List<MEntityType> _ChildTypes = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MEntityType), Header="Список разрешенных типов сущностей детей", Virtual =false)]
        
        public List<MEntityType> ChildTypes
        {
            get 
            {
                if (_ChildTypes==null)
                {
                    _ChildTypes = new List<MEntityType>();
                }
                return _ChildTypes;
            }
        }
        
        #endregion
    }
    #endregion
    #region [MArtType] Тип статьи
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип статьи")]
    public partial class MArtType: MCfgType
    {
        #region [GroupHeader] Групповое наименование
        private String _GroupHeader = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Групповое наименование", Virtual =false)]
        
        public String GroupHeader
        {
            get 
            {
                return _GroupHeader;
            }
            set 
            {
                Model.UpdateProperty(this, "GroupHeader", _GroupHeader, value, typeof(String), null, false);
                _GroupHeader=value;
            }
        }
        #endregion
        #region [NewHeader] Новое наименование
        private String _NewHeader = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Новое наименование", Virtual =false)]
        
        public String NewHeader
        {
            get 
            {
                return _NewHeader;
            }
            set 
            {
                Model.UpdateProperty(this, "NewHeader", _NewHeader, value, typeof(String), null, false);
                _NewHeader=value;
            }
        }
        #endregion
        #region [PropTypes] Список разрешенных типов свойств
        private List<MPropType> _PropTypes = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MPropType), Header="Список разрешенных типов свойств", Virtual =false)]
        
        public List<MPropType> PropTypes
        {
            get 
            {
                if (_PropTypes==null)
                {
                    _PropTypes = new List<MPropType>();
                }
                return _PropTypes;
            }
        }
        
        #endregion
    }
    #endregion
    #region [MPropType] Тип свойства
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип свойства")]
    public partial class MPropType: MCfgType
    {
        #region [Type] Тип данных
        #endregion
        #region [Def] Значение поумолчанию
        private String _Def = default(String);
        [DbColumn(Type = "nvarchar(MAX)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="STRING", ItemType=typeof(String), Header="Значение поумолчанию", Virtual =false)]
        
        public String Def
        {
            get 
            {
                return _Def;
            }
            set 
            {
                Model.UpdateProperty(this, "Def", _Def, value, typeof(String), null, false);
                _Def=value;
            }
        }
        #endregion
        #region [IsReadOnly] Только для чтения
        private Boolean _IsReadOnly = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Только для чтения", Virtual =false)]
        
        public Boolean IsReadOnly
        {
            get 
            {
                return _IsReadOnly;
            }
            set 
            {
                Model.UpdateProperty(this, "IsReadOnly", _IsReadOnly, value, typeof(Boolean), null, false);
                _IsReadOnly=value;
            }
        }
        #endregion
        #region [IsHide] Скрытое свойство
        private Boolean _IsHide = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Скрытое свойство", Virtual =false)]
        
        public Boolean IsHide
        {
            get 
            {
                return _IsHide;
            }
            set 
            {
                Model.UpdateProperty(this, "IsHide", _IsHide, value, typeof(Boolean), null, false);
                _IsHide=value;
            }
        }
        #endregion
        #region [IsBuilder] Показывать только в MBuilder
        private Boolean _IsBuilder = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Показывать только в MBuilder", Virtual =false)]
        
        public Boolean IsBuilder
        {
            get 
            {
                return _IsBuilder;
            }
            set 
            {
                Model.UpdateProperty(this, "IsBuilder", _IsBuilder, value, typeof(Boolean), null, false);
                _IsBuilder=value;
            }
        }
        #endregion
        #region [IsEntity] Свойство сущности
        private Boolean _IsEntity = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Свойство сущности", Virtual =false)]
        
        public Boolean IsEntity
        {
            get 
            {
                return _IsEntity;
            }
            set 
            {
                Model.UpdateProperty(this, "IsEntity", _IsEntity, value, typeof(Boolean), null, false);
                _IsEntity=value;
            }
        }
        #endregion
        #region [IsRelation] Свойство положения
        private Boolean _IsRelation = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Свойство положения", Virtual =false)]
        
        public Boolean IsRelation
        {
            get 
            {
                return _IsRelation;
            }
            set 
            {
                Model.UpdateProperty(this, "IsRelation", _IsRelation, value, typeof(Boolean), null, false);
                _IsRelation=value;
            }
        }
        #endregion
        #region [IsArt] Свойство статьи
        private Boolean _IsArt = default(Boolean);
        [DbColumn(Type = "tinyint", Def ="0", Virtual =false)]
        [EDMProperty(PropType="BOOL", ItemType=typeof(Boolean), Header="Свойство статьи", Virtual =false)]
        
        public Boolean IsArt
        {
            get 
            {
                return _IsArt;
            }
            set 
            {
                Model.UpdateProperty(this, "IsArt", _IsArt, value, typeof(Boolean), null, false);
                _IsArt=value;
            }
        }
        #endregion
        #region [DefUnit] Единица измерения по умолчанию
        private String _DefUnitId = default(String);
        [DbColumn(Type = "nvarchar(255)", Def ="''", Virtual =false)]
        [EDMProperty(PropType="REF", ItemType=typeof(String), Header="Единица измерения по умолчанию (Id)", Virtual =false)]
        
        public String DefUnitId
        {
            get 
            {
                return _DefUnitId;
            }
            set 
            {
                Model.UpdateProperty(this, "DefUnitId", _DefUnitId, value, typeof(String), typeof(MUnitType), false);
                _DefUnitId=value;
            }
        }
        private MUnitType _DefUnit = default(MUnitType);
        [EDMProperty(PropType="REF", ItemType=typeof(MUnitType), Header="Единица измерения по умолчанию", Virtual =false)]
        
        public MUnitType DefUnit
        {
            get 
            {
                if (_DefUnit==null && _DefUnitId!=default(String))
                {
                    Model.UpdateFutureRef(typeof(MUnitType), "MUnitType", "Id", "System.String");
                    _DefUnit = Model.MainDic.GetObj<MUnitType>(DefUnitId);
                }
                return _DefUnit;
            }
        }
        //public bool ShouldSerializeMUnitType()
        //{
        //    return false;
        //}
        
        #endregion
        #region [Units] Список единиц измерения
        private List<MUnitType> _Units = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MUnitType), Header="Список единиц измерения", Virtual =false)]
        
        public List<MUnitType> Units
        {
            get 
            {
                if (_Units==null)
                {
                    _Units = new List<MUnitType>();
                }
                return _Units;
            }
        }
        
        #endregion
        #region [Editor] Тип редактор
        private List<MEditType> _Editor = null;
        [EDMProperty(PropType="LIST", ItemType=typeof(MEditType), Header="Тип редактор", Virtual =false)]
        
        public List<MEditType> Editor
        {
            get 
            {
                if (_Editor==null)
                {
                    _Editor = new List<MEditType>();
                }
                return _Editor;
            }
        }
        
        #endregion
    }
    #endregion
    #region [MPropValueType] Тип значения
    public enum MPropValueType
    {
        NONE=0    // не задано
        ,STRING    // Строка
        ,INT    // Целое
        ,BOOL    // Булевское
    }
    #endregion
    #region [MStatusType] Тип статуса
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип статуса")]
    public partial class MStatusType: MCfgType
    {
        #region [Mask] Маска статуса
        private Int32 _Mask = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Маска статуса", Virtual =false)]
        
        public Int32 Mask
        {
            get 
            {
                return _Mask;
            }
            set 
            {
                Model.UpdateProperty(this, "Mask", _Mask, value, typeof(Int32), null, false);
                _Mask=value;
            }
        }
        #endregion
        #region [ClearMask] Маска очистки
        private Int32 _ClearMask = default(Int32);
        [DbColumn(Type = "int", Def ="0", Virtual =false)]
        [EDMProperty(PropType="INT", ItemType=typeof(Int32), Header="Маска очистки", Virtual =false)]
        
        public Int32 ClearMask
        {
            get 
            {
                return _ClearMask;
            }
            set 
            {
                Model.UpdateProperty(this, "ClearMask", _ClearMask, value, typeof(Int32), null, false);
                _ClearMask=value;
            }
        }
        #endregion
    }
    #endregion
    #region [MUnitType] Тип единицы измерения
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип единицы измерения")]
    public partial class MUnitType: MCfgType
    {
    }
    #endregion
    #region [MViewerType] Тип просмотрщика
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип просмотрщика")]
    public partial class MViewerType: MCfgType
    {
    }
    #endregion
    #region [MEditType] Тип просмотрщика
    [DbTable(Type = "CFG")]
    [EDMClass(Header = "Тип просмотрщика")]
    public partial class MEditType: MCfgType
    {
    }
    #endregion
    #region [MPassword] Пароль пользователя
    [DbTable(Type = "MODELCLASS")]
    [EDMClass(Header = "Пароль пользователя")]
    public partial class MPassword: MEDMNameObj
    {
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
    }
    #endregion
}
#endregion
