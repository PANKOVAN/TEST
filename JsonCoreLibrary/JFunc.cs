using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MJsonCoreLibrary
{
    class JFunc
    {
        public static string GetValue(JToken jt, string name, string def)
        {
            JObject jo = null;
            if (jt is JObject) jo = (JObject)jt;
            else if (jt is JProperty) jo = ((JProperty)jt).Value as JObject;
            if (jo != null)
            {
                try
                {
                    JToken jt1 = jo[name];
                    if (jt1 is JValue)
                    {
                        def = ((JValue)jt1).Value.ToString();
                    }
                }
                catch { }
            }
            return def;
        }
        public static bool GetValue(JToken jt, string name, bool def)
        {
            string v = GetValue(jt, name, "");
            if (v == "") return def;
            else return (v == "1" || v.ToLower() == "true");
        }
        public static bool HasValue(JToken jt, string name)
        {
            JObject jo = null;
            if (jt is JObject) jo = (JObject)jt;
            else if (jt is JProperty) jo = ((JProperty)jt).Value as JObject;
            if (jo != null)
            {
                try
                {
                    JToken jt1 = jo[name];
                    return jt1 != null;
                }
                catch { }
            }
            return false;
        }
        public static string WebixName(string name)
        {
            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }
        public static T Find<T>(JToken jt, string name, Predicate<JToken> cond = null) where T : JToken
        {
            if (jt != null)
            {
                JProperty jp = jt as JProperty;
                if (jp != null)
                {
                    if (jp.Value is T && jp.Name.ToLower() == name.ToLower() && (cond == null || cond(jp.Value)))
                    {
                        return (T)(jp.Value);
                    }
                }
                foreach (JToken jt1 in jt.Children())
                {
                    T jt2 = Find<T>(jt1, name, cond);
                    if (jt2 != null) return jt2;
                }
            }
            return null;
        }
        public static void ForEach<T>(JToken jt, Action<T> func, Predicate<T> cond = null) where T : JToken
        {
            if (jt != null)
            {
                if (jt is T && (cond == null || cond((T)jt)))
                {
                    func((T)jt);
                }
                foreach (JToken jt1 in jt.Children())
                {
                    ForEach<T>(jt1, func, cond);
                }
            }
        }

    }
}
