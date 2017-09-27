using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
/// <summary>
/// Похоже не используется
/// </summary>
namespace MJsonCoreLibrary
{
    //
    // Класс MJsonCoreLibrary содержит методы для чтение файла JSON 
    // c возможность специальной обработки комментариев
    // Конструкции типа
    //   /*
    //        onBeforeShow=function() {
    //        alert('1');
    //        }
    //  */
    // превразаются в
    //          "onBeforeShow":"function() {alert('1');}"
    //
    public class MJsonReader
    {
        public static bool IsCaсhed = false;
        public static Dictionary<string, JToken> _JsonCache = null;
        public static Dictionary<string, JToken> JsonCache
        {
            get
            {
                if (_JsonCache == null && IsCaсhed) _JsonCache = new Dictionary<string, JToken>();
                return _JsonCache;
            }
        }
        public static JToken ReadJson(string filename, JToken own)
        {
            if (IsCaсhed && JsonCache.ContainsKey(filename))
            {
                return JsonCache[filename];
            }
            else
            {
                TextReader tr = System.IO.File.OpenText(filename);
                JsonTextReader jr = new JsonTextReader(tr);
                own = ReadJson(jr, own, true);
                jr.Close();
                if (IsCaсhed) JsonCache[filename] = own;
                return own;
            }
        }
        public static int FileId = 0;
        public static JToken ReadJson(TextReader tr,  JToken own, string filename="")
        {
            if (IsCaсhed && JsonCache.ContainsKey(filename) && filename!="")
            {
                return JsonCache[filename];
            }
            else
            {
                FileId++;
                JsonTextReader jr = new JsonTextReader(tr);
                own = ReadJson(jr, own, true);
                jr.Close();
                if (IsCaсhed && filename!="") JsonCache[filename] = own;
                return own;
            }
        }
        public static JToken ReadJson(JsonTextReader jr, JToken own, bool isRoot = false)
        {
            if (own == null && isRoot)
            {
                own = new JObject();
            }
            JProperty jp = null;
            while (jr.Read())
            {
                switch (jr.TokenType)
                {
                    case JsonToken.StartObject:
                        {
                            JToken jo = null;
                            if (!isRoot)
                            {
                                jo = new JObject();
                                if (jp != null)
                                {
                                    jp.Value = jo;
                                    jp = null;
                                }
                                else if (own is JObject) ((JObject)own).Add(jo);
                                else if (own is JArray) ((JArray)own).Add(jo);
                                else
                                {
                                    throw new Exception($"Неизвестный тип владельца {own.GetType()}");
                                }
                            }
                            else
                            {
                                jo = own;
                            }
                            ReadJson(jr, jo);
                        }
                        break;
                    case JsonToken.StartArray:
                        {
                            JToken jo = null;
                            if (!isRoot)
                            {
                                jo = new JArray();
                                if (jp != null)
                                {
                                    jp.Value = jo;
                                    jp = null;
                                }
                                else if (own is JObject) ((JObject)own).Add(jo);
                                else if (own is JArray) ((JArray)own).Add(jo);
                                else
                                {
                                    throw new Exception($"Неправильный тип владельца {own.GetType()}");
                                }
                            }
                            else
                            {
                                jo = own;
                            }
                            ReadJson(jr, jo);
                        }
                        break;
                    case JsonToken.EndObject:
                    case JsonToken.EndArray:
                        return own;
                    case JsonToken.PropertyName:
                        {
                            if (jp != null)
                            {
                                throw new Exception($"Свойство не сброшено {own.GetType()}");
                            }
                            jp = new JProperty(jr.Value.ToString().Replace("%#%", $"%#%_{FileId}"), null);
                            if (own is JObject) ((JObject)own).Add(jp);
                            else
                            {
                                throw new Exception($"Неправильный тип владельца {own.GetType()}");
                            }
                        }
                        break;
                    case JsonToken.String:
                        {
                            JValue jv = new JValue(jr.Value);
                            if (jv.Value.ToString().Contains("%#%"))
                            {
                                jv.Value = jv.Value.ToString().Replace("%#%", $"%#%_{FileId}");
                            }
                            if (jp != null)
                            {
                                jp.Value = jv;
                            }
                            else
                            {
                                if (own is JArray) ((JArray)own).Add(jv);
                                else
                                {
                                    throw new Exception($"Неправильный тип владельца {own.GetType()}");
                                }
                            }
                            jp = null;
                        }
                        break;
                    case JsonToken.Boolean:
                    case JsonToken.Bytes:
                    case JsonToken.Date:
                    case JsonToken.Float:
                    case JsonToken.Integer:
                    case JsonToken.Null:
                        {
                            JValue jv = new JValue(jr.Value);
                            if (jp != null)
                            {
                                jp.Value = jv;
                            }
                            else
                            {
                                if (own is JArray) ((JArray)own).Add(jv);
                                else
                                {
                                    throw new Exception($"Неправильный тип владельца {own.GetType()}");
                                }
                            }
                            jp = null;
                        }
                        break;
                    case JsonToken.None:
                    case JsonToken.Undefined:
                        break;
                    case JsonToken.Comment:
                        {
                            if (own is JObject)
                            {
                                string s = jr.Value.ToString().Trim().Replace("%#%", $"%#%_{FileId}");
                                if (s.ToLower().Contains("function"))
                                {
                                    int i = s.IndexOf(':');
                                    if (i > 0)
                                    {
                                        ((JObject)own).Add(new JProperty(s.Substring(0, i), s.Substring(i + 1).Trim()));
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        throw new Exception($"Неправильный тип токена {jr.TokenType}");
                }
            }
            return own;
        }

    }

}
