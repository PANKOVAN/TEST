using MEDMCoreLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MJsonCoreLibrary
{
    public class MDataAdapter
    {
        public HttpContext Context = null;
        public virtual object Run(MEDM model, string path, string parms)
        {
            throw new NotImplementedException();
        }
        public virtual object Save(MEDM model, string path, string parms)
        {
            IFormCollection f= Context.Request.Form;
            string n = f["_name"];
            string t = f["_type"];
            Type type = model.GetClassTypeByClassName(t);
            if (type==null) type = model.GetClassTypeByClassName(n);
            if (type==null) throw (new Exception($"Запись изменений. Тип данных не найден ({t}/{n}) ..."));
            string id = f["_id"];
            if (string.IsNullOrEmpty(id)) id= f["id"];

            switch (f["webix_operation"])
            {
                case "insert":
                    {
                        MEDMObj o = model.CreateObject(type);//   MainDic.NewObj(type, null);
                        if (o == null) throw (new Exception($"Запись изменений. Операция insert. Объект не создан (id={id})..."));
                        foreach (KeyValuePair<string, StringValues> kv in f)
                        {
                            if (!kv.Key.StartsWith("_") && !kv.Key.StartsWith("webix_"))
                            {
                                o.SetStringValue(kv.Key, kv.Value, true);
                            }
                        }
                        model.Save(Context.Session);
                        return o;
                    }
                case "update":
                    {
                        if (string.IsNullOrEmpty(id)) throw (new Exception($"Запись изменений. Id не задан ..."));
                        model.LockUpdates++;
                        MEDMObj o = null;
                        try
                        {
                            o = model.MainDic.GetObj(type, id);
                        }
                        finally
                        {
                            model.LockUpdates--;
                        }
                        //Старый вариант с общим кешем
                        //MEDMObj o = model.MainDic.FindObj(type, id);
                        //if (o==null) throw (new Exception($"Запись изменений. Операция update. Объект не найден (id={id})..."));
                        foreach(KeyValuePair <string, StringValues> kv in  f)
                        {
                            if (kv.Key!="id" && !kv.Key.StartsWith("_") && !kv.Key.StartsWith("webix_"))
                            {
                                o.SetStringValue(kv.Key, kv.Value[0], true);
                            }
                        }
                        model.Save(Context.Session);
                        return o;
                    }
                case "delete":
                    {
                        if (string.IsNullOrEmpty(id)) throw (new Exception($"Запись изменений. Id не задан ..."));
                        MEDMObj o = model.MainDic.GetObj(type, id);
                        if (o == null) throw (new Exception($"Запись изменений. Операция delete. Объект не найден (id={id})..."));
                        model.DeleteObject(o);
                        model.Save(Context.Session);
                        return o;
                    }
                case "move":
                    break;
                default:
                    {
                        throw (new Exception($"Запись изменений. Операция \"{f["webix_operation"]}\" не определена..."));
                    }
                    break;
            }
            return null;
        }
        public virtual IEnumerable<string> GetCommands(MEDM model)
        {
            return "refresh".Split(';');
        }

        #region Прочитать параметры
        public string GetParm(string name, string def)
        {
            string s = def;
            name = name.ToLower();
            if (Context != null && Context.Request!=null)
            {
                if (Context.Request.Query != null)
                {
                    if (Context.Request.Query.ContainsKey(name))
                    {
                        s=Context.Request.Query[name];
                    }
                    
                }
            }
            return s;
        }
        public Guid GetParm(string name, Guid def)
        {
            Guid guid = def;
            string s = GetParm(name, "");
            if (s != "")
            {
                guid = new Guid(s);
            }
            return guid;
        }
        public int GetParm(string name, int def)
        {
            int i = def;
            string s = GetParm(name, "");
            if (s != "")
            {
                try
                {
                    i = Convert.ToInt32(s);
                }
                catch { }
            }
            return i;
        }
        public long GetParm(string name, long def)
        {
            long i = def;
            string s = GetParm(name, "");
            if (s != "")
            {
                try
                {
                    i = Convert.ToInt64(s);
                }
                catch { }
            }
            return i;
        }
        public bool GetParm(string name, bool def)
        {
            bool r = def;
            string s = GetParm(name, "");
            if (s != "")
            {
                s = s.ToLower();
                r = (s == "1") || (s == "true");
            }
            return r;
        }
        public DateTime GetParm(string name, DateTime def)
        {
            DateTime i = def;
            string s = GetParm(name, "");
            if (s != "")
            {
                try
                {
                    i = Convert.ToDateTime(s);
                }
                catch { }
            }
            return i;
        }
        #endregion

    }
    /*
    public class ParamValues : List<object>
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
    */
}
