using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MJsonCoreLibrary
{
    public class MJsonAppConverter : MJsonConverter
    {
        #region static
        public static bool IsCached = false;
        public static void Reload()
        {
            _JMacros = null;
        }
        public static MJsonAppConverter GetConverter(MEDM model)
        {
            return new MJsonAppConverter(model);
        }
        public static JsonSerializerSettings GetSettings(MEDM model)
        {
            JsonSerializerSettings _Settings = new JsonSerializerSettings();
            _Settings.Converters.Add(GetConverter(model));
            return _Settings;
        }

        #endregion
        public static string MacroPath = "macro";
        #region Macros
        private static Dictionary<string, JToken> _JMacros = null;
        private static Dictionary<string, JToken> JMacros
        {
            get
            {
                if (_JMacros == null)
                {
                    _JMacros = new Dictionary<string, JToken>();


                    Assembly ass = typeof(MJsonAppConverter).GetTypeInfo().Assembly;
                    foreach (string resname in ass.GetManifestResourceNames())
                    {
                        if (Path.GetExtension(resname).ToLower() == ".json")
                        {
                            Stream stream = ass.GetManifestResourceStream(resname);
                            TextReader tr = new StreamReader(stream);
                            JObject jo = MJsonReader.ReadJson(tr, null) as JObject;
                            if (jo != null)
                            {
                                foreach (JProperty jp in jo.Children<JProperty>())
                                {
                                    if (jp.Name.StartsWith("@"))
                                    {
                                        _JMacros[jp.Name] = jp.Value;
                                    }
                                }
                            }
                        }
                    }

                    foreach (string filename in Directory.GetFiles(MacroPath, "*.json", SearchOption.AllDirectories))
                    {
                        TextReader tr = System.IO.File.OpenText(filename);
                        JObject jo = MJsonReader.ReadJson(tr, null) as JObject;
                        if (jo != null)
                        {
                            foreach (JProperty jp in jo.Children<JProperty>())
                            {
                                if (jp.Name.StartsWith("@"))
                                {
                                    _JMacros[jp.Name] = jp.Value;
                                }
                            }
                        }
                    }
                }
                return _JMacros;
            }
        }
        private static Dictionary<string, MJsonMacro> _CSMacros = null;
        private static Dictionary<string, MJsonMacro> CSMacros
        {
            get
            {
                if (_CSMacros == null)
                {
                    _CSMacros = new Dictionary<string, MJsonMacro>();
                    foreach (MJsonMacro jm in MFunc.CreateInstancesByType<MJsonMacro>())
                    {
                        _CSMacros["@" + jm.GetType().Name.ToLower()] = jm;
                    }
                    foreach (MJsonMacro jm in MFunc.CreateInstancesByType<MJsonMacro>(typeof(MJsonAppConverter).GetTypeInfo().Assembly))
                    {
                        _CSMacros["@" + jm.GetType().Name.ToLower()] = jm;
                    }
                }
                return _CSMacros;
            }
        }
        #endregion

        #region Генерация уникальных ID
        public static int _AutoIncrementId = 0;
        public static int AutoIncrementId
        {
            get
            {
                _AutoIncrementId++;
                return _AutoIncrementId;
            }
        }
        public int ConverterId
        {
            get; set;
        }
        public string CID
        {
            get
            {
                return $"{ConverterId}";
            }
        }
        #endregion

        public MJsonAppConverter(MEDM model) : base(model)
        {
            ConverterId = AutoIncrementId;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            WriteJson(writer, value, null, "", null, serializer);
        }
        public void WriteJson(JsonWriter writer, object value, Dictionary<string, object> mparam, string param, JToken content, JsonSerializer serializer)
        {
            if (value != null)
            {
                Type t = value.GetType();
                if (t == typeof(JObject))
                {
                    JObject jo = (JObject)value;
                    writer.WriteStartObject();
                    foreach (JToken jt in jo.Children())
                    {
                        WriteJson(writer, jt, mparam, param, content, serializer);
                    }
                    writer.WriteEndObject();
                }
                else if (t == typeof(JArray))
                {
                    JArray ja = (JArray)value;
                    writer.WriteStartArray();
                    foreach (JToken jt in ja.Children())
                    {
                        WriteJson(writer, jt, mparam, param, content, serializer);
                    }
                    writer.WriteEndArray();
                }
                else if (t == typeof(JProperty))
                {
                    JProperty jp = (JProperty)value;
                    if (jp.Name.StartsWith("@"))
                    {
                        string name = "";
                        string[] v = jp.Name.Split('?');
                        name = v[0];
                        if (v.Length > 1)
                        {
                            param = v[1];
                            param = ReplaceParam(param, mparam);
                        }
                        WriteMacro(writer, name, mparam, param, (jp.Value.Type != JTokenType.Null) ? (jp.Value) : (content), serializer, false);
                    }
                    else
                    {
                        writer.WritePropertyName(jp.Name);
                        WriteJson(writer, jp.Value, mparam, param, content, serializer);
                    }
                }
                else if (t == typeof(JValue) || t == typeof(string))
                {
                    string v = value.ToString();
                    v = ReplaceParam(v, mparam);
                    if (v.StartsWith("@"))
                    {
                        string name = "";
                        string[] vv = v.Split('?');
                        name = vv[0];
                        if (vv.Length > 1) param = vv[1];
                        WriteMacro(writer, name, mparam, param, content, serializer, true);
                    }
                    else
                    {
                        JToken jt = JToken.FromObject(v);
                        jt.WriteTo(writer);
                    }
                }
                else
                {
                    JToken jt = JToken.FromObject(value);
                    jt.WriteTo(writer);
                }
            }
        }
        private string ReplaceParam(string v, Dictionary<string, object> mparam)
        {
            //if (mparam != null)
            {
                int i = 0;
                int j = 0;
                while (true)
                {
                    i = v.IndexOf('%', i);
                    if (i < 0) break;
                    j = v.IndexOf('%', i + 1);
                    if (j <= i)
                    {
                        break;
                    }
                    else if (j == i + 1)
                    {
                        i = j + 1;
                    }
                    else
                    {
                        string n = v.Substring(i + 1, j - i - 1);
                        if (n == "#" || n == "##")
                        {
                            v = v.Remove(i, j - i + 1).Insert(i, CID);
                        }
                        else if (mparam != null && mparam.ContainsKey(n))
                        {
                            v = v.Remove(i, j - i + 1).Insert(i, mparam[n].ToString());
                        }
                        else
                        {
                            i = j + 1;
                        }
                    }
                }
            }
            return v;
        }
        private Dictionary<string, object> SetParam(JToken macroObject, string param, Dictionary<string, object> mparam1)
        {
            Dictionary<string, object> mparam = null;
            if (mparam1 == null) mparam = new Dictionary<string, object>();
            else mparam = new Dictionary<string, object>(mparam1);


            if (macroObject is JObject)
            {
                JToken jparam = ((JObject)macroObject).GetValue("params", StringComparison.CurrentCultureIgnoreCase);
                int i = 0;
                string[] names = new string[32];
                // Параметры по умолчанию
                if (jparam != null)
                {
                    foreach (JProperty jp1 in jparam.Children())
                    {
                        string name = jp1.Name.ToLower();
                        if (i < names.Length) names[i] = name;
                        mparam[name] = jp1.Value.ToString();
                        i++;
                    }
                }
                // Параметры из param
                i = 0;
                foreach (string p in param.Split('&'))
                {
                    if (!string.IsNullOrEmpty(p))
                    {
                        int j = p.IndexOf('=');
                        string n = "";
                        string v = "";
                        if (j >= 0)
                        {
                            n = p.Substring(0, j).ToLower();
                            v = p.Substring(j + 1);
                        }
                        else
                        {
                            if (names != null && i < names.Length && names[i] != null)
                            {
                                v = p;
                                n = names[i];
                            }
                        }
                        if (n != "")
                        {
                            mparam[n] = v;
                        }
                        i++;
                    }
                }

            }
            return mparam;
        }
        public void WriteMacro(JsonWriter writer, string name, Dictionary<string, object> mparam, string param, JToken content, JsonSerializer serializer, bool writeStartObject)
        {
            if (JMacros != null && name != "")
            {
                try
                {
                    //name = ReplaceParam(name);
                    if (!JMacros.ContainsKey(name)) throw new Exception($"Макро \"{name}\" не найдено...");
                    JToken jmacro = JMacros[name];
                    if (CSMacros.ContainsKey(name))
                    {
                        mparam = SetParam(jmacro, param, mparam);
                        MJsonMacro jm = CSMacros[name];
                        jm.WriteMacro(this, writer, jmacro, name, mparam, param, content, serializer, writeStartObject);
                    }
                    else
                    {
                        if (jmacro is JObject)
                        {
                            mparam = SetParam(jmacro, param, mparam);
                            JToken jbody = ((JObject)jmacro).GetValue("body", StringComparison.CurrentCultureIgnoreCase);
                            if (jbody != null)
                            {
                                if (writeStartObject) writer.WriteStartObject();
                                foreach (JToken jt in jbody.Children())
                                {
                                    WriteJson(writer, jt, mparam, param, content, serializer);
                                }
                                if (writeStartObject) writer.WriteEndObject();
                            }
                            else
                            {
                                throw new Exception($"Maкро {name} определена неверно...");
                            }
                        }
                        else if (jmacro is JValue)
                        {
                            jmacro.WriteTo(writer);
                        }
                        else
                        {
                            throw new Exception($"Maкро {name} не найдена...");
                        }
                    }
                }
                catch (Exception e)
                {
                    if (writeStartObject) writer.WriteStartObject();
                    writer.WritePropertyName("_error_");
                    WriteJson(writer, (e.InnerException != null) ? (e.InnerException.Message) : (e.Message), serializer);
                    if (writeStartObject) writer.WriteEndObject();
                }
            }
        }
    }
    public class MJsonMacro
    {
        protected void Write(JsonWriter writer, string name, object value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                writer.WritePropertyName(name);
            }
            if (value is JObject)
            {
                writer.WriteStartObject();
                foreach (JProperty jp in ((JObject)value).Properties())
                {
                    Write(writer, jp.Name, jp.Value);
                }
                writer.WriteEndObject();
            }
            else if (value is JArray)
            {
                writer.WriteStartArray();
                foreach (JToken jt in ((JArray)value).Values())
                {
                    Write(writer, null, jt);
                }
                writer.WriteEndArray();
            }
            else if (value is JProperty)
            {
                Write(writer, ((JProperty)value).Name, ((JProperty)value).Value);
            }
            else if (value is JValue)
            {
                writer.WriteValue(((JValue)value).Value);
            }
            else
            {
                writer.WriteValue(value);
            }

        }
        public virtual void WriteMacro(MJsonAppConverter converter, JsonWriter writer, JToken jmacro, string name, Dictionary<string, object> mparam, string param, JToken content, JsonSerializer serializer, bool writeStartObject)
        {

        }
        public string GetCommands(string controllerName, string controllerFolder = "Data")
        {
            string commands = "";
            //MDataAdapter da = DataController
            return commands;
        }
    }
}
