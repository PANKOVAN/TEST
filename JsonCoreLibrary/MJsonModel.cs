using MEDMCoreLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
/// <summary>
///  Похоже не используется
/// </summary>
namespace MJsonCoreLibrary
{
    class MJsonModel
    {
        public static string MacroPath = "models";
        #region Static
        public static bool IsCached = false;
        public static void Reload()
        {
            _JModels = null;
        }
        public static JToken GetModel()
        {
            return new MJsonModel()["Model"];
        }
        #endregion
        #region Models
        private static Dictionary<string, JToken> _JModels = null;
        private static Dictionary<string, JToken> JModels
        {
            get
            {
                if (_JModels == null)
                {
                    _JModels = new Dictionary<string, JToken>();

                    foreach (string filename in Directory.GetFiles(MacroPath, "*.json", SearchOption.AllDirectories))
                    {
                        try
                        {
                            TextReader tr = System.IO.File.OpenText(filename);
                            JObject jo = MJsonReader.ReadJson(tr, null) as JObject;
                            if (jo != null)
                            {
                                foreach (JProperty jp in jo.Children<JProperty>())
                                {
                                    _JModels[jp.Name] = jp.Value;
                                }
                            }
                        }

                        catch (Exception e)
                        {
                            throw new Exception($"Ошибки при загрузки модели {filename} ({e.Message})");
                        }
                    }
                }
                return _JModels;
            }
        }
        #endregion

        public MJsonModel()
        {
        }
        public JToken this[string index]
        {
            get
            {
                if (JModels.ContainsKey(index)) return JModels[index];
                return null;
            }
        }

    }
}
