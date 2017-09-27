using MFuncCoreLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MEDMCoreLibrary
{
    public interface IPaginationList
    {
        int StartPos { get; set; }
        int PageSize { get; set; }
        int AllCount { get; set; }
        int PageCount { get; }
        bool Continue { get; set; }
        bool PageComplete { get; }
        string Top { get; }
        //void AddPagionatioItem(object item);
    }
    public class MPaginationList : List<object>, IPaginationList
    {
        #region StartPos
        private int _StartPos = -1;
        public int StartPos
        {
            get
            {
                return _StartPos;
            }
            set
            {
                _StartPos = value;
            }
        }
        #endregion
        #region PageSize
        private int _PageSize = -1;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }
        #endregion
        #region AllCount
        private int _AllCount = 0;
        public int AllCount
        {
            get
            {
                return _AllCount;
            }
            set
            {
                _AllCount = value;
            }
        }

        #endregion
        #region Continue
        private bool _Continue = false;
        public bool Continue
        {
            get
            {
                return StartPos >= 0;
            }
            set
            {

            }
        }
        #endregion
        #region PageComplete
        public bool PageComplete
        {
            get
            {
                return AllCount >= StartPos + PageSize;
            }
        }
        #endregion
        #region Конструкторы
        public MPaginationList() : base()
        {
        }
        public MPaginationList(string parms, int startpos = -1, int pagesize = -1) : base()
        {
            StartPos = startpos;
            PageSize = pagesize;
            if (MFunc.GetAttr(parms, "continue", false))
            {
                Continue = true;
                StartPos = MFunc.GetAttr(parms, "start", StartPos);
                PageSize = MFunc.GetAttr(parms, "count", PageSize);
            }
            else
            {
                Continue = false;
                StartPos = MFunc.GetAttr(parms, "start", StartPos);
                PageSize = MFunc.GetAttr(parms, "count", PageSize);
                if (PageSize >= 0 && StartPos < 0) StartPos = 0;
            }
        }
        #endregion
        #region PageCount
        public int PageCount
        {
            get
            {
                return Count;
            }
        }
        #endregion
        #region Top
        public string Top
        {
            get
            {
                string s = "";
                if (StartPos >= 0 && PageSize > 0)
                {
                    s = $"top {StartPos + PageSize}";
                }
                return s;

            }
        }
        #endregion
        #region IdList
        public string IdList()
        {
            string l = "";
            foreach (object o in this)
            {
                if (l != "") l += ",";
                if (o is MEDMIdObj)
                {
                    l += (o as MEDMIdObj).Id.ToString();
                }
                else if (o is MEDMNameObj)
                {
                    l += "'" + (o as MEDMNameObj).Id + "'";
                }
                else if (o is MEDMGuidObj)
                {
                    l += "'" + (o as MEDMGuidObj).Id.ToString() + "'";
                }
            }
            return l;
        }
        #endregion
    }

    public class MTreeList : List<object>
    {
        #region Parent
        private object _Parent = -1;
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
        #region IdList
        public string IdList()
        {
            string l = "";
            foreach (object o in this)
            {
                if (l != "") l += ",";
                if (o is MEDMIdObj)
                {
                    l += (o as MEDMIdObj).Id.ToString();
                }
                else if (o is MEDMNameObj)
                {
                    l += "'" + (o as MEDMNameObj).Id + "'";
                }
                else if (o is MEDMGuidObj)
                {
                    l += "'" + (o as MEDMGuidObj).Id.ToString() + "'";
                }
            }
            return l;
        }
        #endregion
        #region Конструкторы
        public MTreeList() : base()
        {
        }
        public MTreeList(object parent = null) : base()
        {
            Parent = parent;
        }
        #endregion

    }
}
