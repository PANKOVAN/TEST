using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MJsonCoreLibrary
{
    public class MJsonDataConverter : MJsonConverter
    {
        #region static
        public static bool IsCached = false;
        public static void Reload()
        {
        }
        public static MJsonDataConverter GetConverter(MEDM model)
        {
            return new MJsonDataConverter(model);
        }
        public static JsonSerializerSettings GetSettings(MEDM model, string path = "macro")
        {
            JsonSerializerSettings _Settings = null;
            _Settings = new JsonSerializerSettings();
            _Settings.Converters.Add(GetConverter(model));
            return _Settings;
        }
        #endregion
        #region Рабочие переменные
        Dictionary<TypeInfo, HashSet<MEDMObj>> ObjDic = null;
        HashSet<MEDMObj> ObjList = null;
        int ObjLevel = 0;
        int ListLevel = 0;
        static Dictionary<string, object> DefaultDic = null;
        HashSet<TypeInfo> TypeDic = null;

        #endregion

        public MJsonDataConverter(MEDM model) : base(model)
        {
            ObjDic = new Dictionary<TypeInfo, HashSet<MEDMObj>>();
            ObjList = new HashSet<MEDMObj>();
            TypeDic = new HashSet<TypeInfo>();
        }
        
        public static object GetDefaulValue(Type t)
        {
            if (DefaultDic == null)
            {
                DefaultDic = new Dictionary<string, object>();
                DefaultDic.Add(typeof(string).Name, "");
                DefaultDic.Add(typeof(bool).Name, default(bool));
                DefaultDic.Add(typeof(int).Name, default(int));
                DefaultDic.Add(typeof(short).Name, default(short));
                DefaultDic.Add(typeof(long).Name, default(long));
                DefaultDic.Add(typeof(float).Name, default(float));
                DefaultDic.Add(typeof(double).Name, default(double));
                DefaultDic.Add(typeof(decimal).Name, default(decimal));
                DefaultDic.Add(typeof(DateTime).Name, DateTime.Parse("1753-01-01T12:00:00"));// default(DateTime));
                DefaultDic.Add(typeof(char).Name, default(char));
                DefaultDic.Add(typeof(byte).Name, default(byte));
                DefaultDic.Add(typeof(Guid).Name, default(Guid));
            }

            if (DefaultDic.ContainsKey(t.Name)) return DefaultDic[t.Name];
            return null;
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                TypeInfo ti = value.GetType().GetTypeInfo();

                if (value is MEDMObj)
                {
                    if (!TypeDic.Contains(ti)) TypeDic.Add(ti);
                    if (ObjLevel > 0)
                    {
                        if (!ObjDic.ContainsKey(ti)) ObjDic[ti] = new HashSet<MEDMObj>();
                        HashSet<MEDMObj> ObjHash = ObjDic[ti];
                        if (!ObjHash.Contains((MEDMObj)value)) ObjHash.Add((MEDMObj)value);
                    }
                    else
                    {
                        if (!ObjList.Contains((MEDMObj)value)) ObjList.Add((MEDMObj)value);
                    }
                    
                    {
                        if (ObjLevel <= 0)
                        {
                            writer.WriteStartObject();
                            writer.WritePropertyName("_type");
                            writer.WriteValue(ti.Name);
                        }
                        PropertyInfo[] piList = ti.GetProperties();
                        foreach (PropertyInfo pi in piList)
                        {
                            if (pi.CanRead)
                            {
                                string n = pi.Name;
                                object v = pi.GetValue(value);
                                TypeInfo pt = pi.PropertyType.GetTypeInfo();
                                if (pi.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null && pi.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
                                {
                                    if (pt.IsSubclassOf(typeof(MEDMObj)))
                                    {
                                        if (v != null)
                                        {
                                            //writer.WritePropertyName(n.Substring(0, 1).ToLower() + n.Substring(1));
                                            //writer.WriteStartObject();
                                            //writer.WritePropertyName("_type");
                                            //writer.WriteValue(pt.Name);
                                            //writer.WritePropertyName("id");
                                            //object id = (v as MEDMObj).GetId();
                                            //if (id is string) id = ((string)id).ToUpper().Trim();
                                            //writer.WriteValue(id);
                                            //writer.WriteEndObject();
                                            ObjLevel++;
                                            WriteJson(writer, v, serializer);
                                            ObjLevel--;
                                        }
                                    }
                                    else if (ObjLevel<=0)
                                    {
                                        //if (n.ToLower().EndsWith("id"))
                                        //{
                                        //    string n1 = n.Substring(0, n.Length - 2);
                                        //    if (ti.GetProperty(n1) != null)
                                        //    {
                                        //        continue;
                                        //    }
                                        //}
                                        if (v != null && (!v.Equals(GetDefaulValue(v.GetType())) || n.ToLower()=="id" ))
                                        {
                                            writer.WritePropertyName(n.Substring(0, 1).ToLower() + n.Substring(1));
                                            WriteJson(writer, v, serializer);
                                        }
                                    }
                                }
                            }
                        }
                        if (ObjLevel <= 0) writer.WriteEndObject();
                    }

                }
                else if (value is IList)
                {
                    if (ListLevel <= 0)
                    {
                        writer.WriteStartArray();
                        foreach (object v1 in (IList)value)
                        {
                            if (v1 is MEDMObj && ListLevel > 0)
                            {
                                writer.WriteStartObject();
                                writer.WritePropertyName("_type");
                                writer.WriteValue(v1.GetType().Name);
                                writer.WritePropertyName("id");
                                object id = (v1 as MEDMObj).GetId();
                                if (id is string) id = ((string)id).ToUpper().Trim();
                                writer.WriteValue(id);
                                writer.WriteEndObject();
                                ListLevel++;
                                ObjLevel++;
                                WriteJson(writer, v1, serializer);
                                ObjLevel--;
                                ListLevel--;
                            }
                            else
                            {
                                ListLevel++;
                                WriteJson(writer, v1, serializer);
                                ListLevel--;
                            }
                        }
                        writer.WriteEndArray();
                    }
                    else
                    {
                        writer.WriteStartObject();
                        int i = 0;
                        foreach (object v1 in (IList)value)
                        {
                            if (v1 is MEDMObj && ListLevel > 0)
                            {
                                object id = (v1 as MEDMObj).GetId();
                                if (id is string) id = ((string)id).ToUpper().Trim();
                                writer.WritePropertyName(id.ToString());
                                writer.WriteStartObject();
                                writer.WritePropertyName("_type");
                                writer.WriteValue(v1.GetType().Name);
                                writer.WritePropertyName("id");
                                writer.WriteValue(id);
                                writer.WriteEndObject();
                                ListLevel++;
                                ObjLevel++;
                                WriteJson(writer, v1, serializer);
                                ObjLevel--;
                                ListLevel--;
                            }
                            else
                            {
                                ListLevel++;
                                writer.WritePropertyName(i.ToString());
                                WriteJson(writer, v1, serializer);
                                ListLevel--;
                            }
                            i++;
                        }
                        writer.WriteEndObject();
                    }
                }
                else if (ti.IsClass && !(value is string))
                {
                    writer.WriteStartObject();
                        writer.WritePropertyName("_type");
                        writer.WriteValue(ti.Name);
                    foreach (PropertyInfo pi in ti.GetProperties())
                    {
                        if (pi.CanRead)
                        {
                            string n = pi.Name;
                            object v = pi.GetValue(value);
                            TypeInfo pt = pi.PropertyType.GetTypeInfo();
                            if (pi.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null && pi.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
                            {
                                if (pt.IsSubclassOf(typeof(MEDMObj)))
                                {
                                    WriteJson(writer, v, serializer);
                                }
                                else
                                {
                                    if (v != null && (!v.Equals(GetDefaulValue(v.GetType())) || n.ToLower()=="id" ))
                                    {
                                        writer.WritePropertyName(n.Substring(0, 1).ToLower() + n.Substring(1));
                                        WriteJson(writer, v, serializer);
                                    }
                                }
                            }
                        }
                    }
                    if (value is MJsonData /* && ObjDic.Count > 0 && TypeDic.Count>0 && ObjLevel <= 0*/)
                    {
                        writer.WritePropertyName("dic");
                        writer.WriteStartObject();
                        foreach (KeyValuePair<TypeInfo, HashSet<MEDMObj>> ObjHash in ObjDic)
                        {
                            // Не нужно серилизовывать объекты конфигурации
                            if (!ObjHash.Key.IsSubclassOf(typeof(MEDMCfgObj)))
                            {
                                bool f = false;
                                foreach (MEDMObj obj in ObjHash.Value)
                                {
                                    if (!ObjList.Contains(obj))
                                    {
                                        if (!f)
                                        {
                                            f = true;
                                            writer.WritePropertyName(ObjHash.Key.Name);
                                            writer.WriteStartObject();
                                        }
                                        object id = obj.GetId();
                                        if (id is string) id = ((string)id).ToUpper();
                                        writer.WritePropertyName(id.ToString());
                                        WriteJson(writer, obj, serializer);
                                    }
                                }
                                if (f)
                                {
                                    writer.WriteEndObject();
                                }
                            }
                        }
                        writer.WriteEndObject();
                        /*
                        writer.WritePropertyName("$stub");
                        writer.WriteStartObject();
                        foreach (TypeInfo t in TypeDic)
                        {
                            writer.WritePropertyName(t.Name);
                            writer.WriteStartObject();
                            foreach (PropertyInfo pi in t.GetProperties())
                            {
                                if (pi.CanRead)
                                {
                                    string n = pi.Name;
                                    TypeInfo pt = pi.PropertyType.GetTypeInfo();
                                    if (pi.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null && pi.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
                                    {
                                        writer.WritePropertyName(n.Substring(0, 1).ToLower() + n.Substring(1));
                                        if (pt.IsSubclassOf(typeof(MEDMObj)))
                                        {
                                            writer.WriteStartObject();
                                            writer.WritePropertyName("_type");
                                            writer.WriteValue(pt.Name);
                                            writer.WriteEndObject();
                                        }
                                        else
                                        {
                                            writer.WriteValue(GetDefaulValue(pi.PropertyType));
                                        }
                                    }
                                }
                            }
                            writer.WriteEndObject();
                        }
                        writer.WriteEndObject();
                        */
                        
                    }
                    writer.WriteEndObject();
                }
                else
                {
                    writer.WriteValue(value);
                }
            }
        }
    }
}

