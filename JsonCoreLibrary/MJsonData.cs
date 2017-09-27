using MEDMCoreLibrary;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MJsonCoreLibrary
{
    public class MJsonData
    {

        #region Create
        public static MJsonData Create(object data)
        {
            if (!(data is MJsonData))
            {
                data = new MJsonData(data);
            }
            return data as MJsonData;
        }
        #endregion
        #region Data
        private object _Data = null;
        public object Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
            }
        }
        #endregion
        #region Pos
        private int? _Pos = null;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Pos
        {
            get
            {
                return _Pos;
            }
            set
            {
                _Pos = value;
            }
        }
        #endregion
        #region Error
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string _error { get; set; }
        #endregion
        #region Total_count
        private int? _Total_count = null;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Total_count
        {
            get
            {
                return _Total_count;
            }
            set
            {
                _Total_count = value;
            }
        }
        #endregion
        #region Parent
        private object _Parent = null;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
        #endregion
        #region DataMode
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object DataMode
        {
            get
            {
                if (Data is MTreeList) return "tree";
                else if (Data is IList) return "list";
                return null;
            }
        }
        #endregion
        #region Конструктор
        public MJsonData(object data)
        {
            if (data is Exception)
            {
                _error = (data as Exception).Message;
            }
            else
            {
                Data = data;
                if (data is IPaginationList)
                {
                    IPaginationList pl = (IPaginationList)data;
                    this.Pos = pl.StartPos;
                    if (this.Pos < 0) this.Pos = 0;
                    /*if (pl.StartPos == 0)*/
                    this.Total_count = pl.AllCount;
                }
                else if (data is MTreeList)
                {
                    Parent = ((MTreeList)data).Parent;
                }
            }
        }
        #endregion
    }
}
