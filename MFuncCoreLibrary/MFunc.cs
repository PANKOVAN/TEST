using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace MFuncCoreLibrary
{
    public class MFunc
    {
        #region Разное
        public static bool IsBaseType(Type maintype, Type basetype)
        {
            while (maintype != null)
            {
                if (maintype.Equals(basetype)) return true;
                maintype = maintype.GetTypeInfo().BaseType;
            }
            return false;
        }
        public static string GetNameFromPath(string path)
        {
            string name = "";
            if (!string.IsNullOrEmpty(path))
            {
                string[] s = path.Split('/');
                name = s[s.Length - 1];
            }
            return name;
        }
        #endregion
        #region Security
        public static int GetSecurityHash(string s)
        {
            MD5 md5 = MD5.Create();

            byte[] b = md5.ComputeHash(Encoding.Unicode.GetBytes(s));
            int h = 0;
            h = b[0] ^ (b[1] * 256) ^ (b[2] * 256 * 256) ^ (b[3] * 256 * 256 * 256) ^
                b[4] ^ (b[5] * 256) ^ (b[6] * 256 * 256) ^ (b[7] * 256 * 256 * 256) ^
                b[8] ^ (b[9] * 256) ^ (b[10] * 256 * 256) ^ (b[11] * 256 * 256 * 256) ^
                b[12] ^ (b[13] * 256) ^ (b[14] * 256 * 256) ^ (b[15] * 256 * 256 * 256);
            md5.Dispose();
            return h;
        }
        #endregion
        #region Path
        public static string GetBaseDir(string basepath)
        {
            int i = basepath.ToLower().LastIndexOf("\\bin\\");
            if (i > 0) basepath = basepath.Substring(0, i);
            return basepath;
        }

        #endregion
        #region Assembly
        public static T CreateInstanceByName<T>(string name, string folder, Assembly assembly = null, params object[] args)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            name = name.ToLower();
            folder = folder.ToLower();
            foreach (Type t in assembly.GetTypes())
            {
                if (t.Name.ToLower() == name && (folder == "" || t.Namespace.ToLower().EndsWith(folder)))
                {
                    object o = Activator.CreateInstance(t, args);
                    if (o is T) return (T)o;
                    throw new Exception($@"По имени ""{name}"" создан объект типа {o.GetType()}, который не наследует от {typeof(T).GetType()}");
                }
            }
            return default(T);
        }
        public static IEnumerable<T> CreateInstancesByType<T>(Assembly assembly = null, params object[] args)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            List<T> l = new List<T>();
            foreach (Type t in assembly.GetTypes())
            {
                if (t.GetTypeInfo().IsSubclassOf(typeof(T)))
                {
                    object o = Activator.CreateInstance(t, args);
                    l.Add((T)o);
                }
                
            }
            return l;
        }
        public static Type FindTypeByName(string name, string folder, Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            name = name.ToLower();
            folder = folder.ToLower();
            foreach (Type t in assembly.GetTypes())
            {
                if (t.Name.ToLower() == name && (folder == "" || t.Namespace.ToLower().EndsWith(folder)))
                {
                    return t;
                }
            }
            return null;
        }
        #endregion

        #region Прочитать параметр из строки
        private static char[] GetAttrDelim = { '?', '&', '\r', '\n', ';', ',' };
        public static string GetAttr(string atrs, string name, string def)
        {
            if (atrs == null || atrs == "") return def;
            string s = def;
            name = name + "=";
            int i = atrs.IndexOf(name, StringComparison.CurrentCultureIgnoreCase);
            if (i >= 0)
            {
                int j = atrs.IndexOf('"', i + name.Length);
                // Значение в кавычках
                if (j >= 0)
                {
                    int k = atrs.IndexOf('"', j + 1);
                    if (k >= 0)
                    {
                        s = atrs.Substring(j + 1, k - j - 1);
                    }
                }
                // Значение без кавычек
                else
                {
                    j = i + name.Length;
                    int k = (atrs + '\n').IndexOfAny(GetAttrDelim, j + 1);
                    if (k >= 0)
                    {
                        s = atrs.Substring(j, k - j);
                    }
                    else
                    {
                        s = atrs.Substring(j);
                    }
                }

            }

            return s;
        }
        public static bool GetAttr(string atrs, string name, bool def)
        {
            bool r = def;
            string s = MFunc.GetAttr(atrs, name, "");
            if (s != "")
            {
                s = s.ToLower();
                r = (s == "1") || (s == "true");
            }
            return r;
        }
        public static int GetAttr(string atrs, string name, int def)
        {
            if (atrs == null || atrs == "") return def;
            int i = def;
            string s = MFunc.GetAttr(atrs, name, "");
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
        public static DateTime GetAttr(string atrs, string name, DateTime def)
        {
            if (atrs == null || atrs == "") return def;
            DateTime i = def;
            string s = MFunc.GetAttr(atrs, name, "");
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
        public static Guid GetAttr(string atrs, string name, Guid def)
        {
            if (atrs == null || atrs == "") return def;
            Guid i = def;
            string s = MFunc.GetAttr(atrs, name, "");
            if (s != "")
            {
                try
                {
                    i = new Guid(s);
                }
                catch { }
            }
            return i;
        }
        #endregion
        #region Установить параметр в строке
        public static string SetAttr(string name, string value)
        {
            string result = "";
            if (value != "")
            {
                result = " " + name + "=\"" + value + "\"";
            }
            return result;
        }
        public static string SetAttr(string source, string name, string value)
        {
            string s = " " + source;
            s = s.Replace("\t", " ");
            s = s.Replace("\r", " ");
            s = s.Replace("\n", " ");
            int i = s.IndexOf(" " + name + "=\"");
            if (i >= 0)
            {
                i += name.Length + 3;
                int j = s.IndexOf('"', i);
                if (j > i)
                {
                    s = s.Remove(i, j - i);
                    s = s.Insert(i, value);
                }
            }
            else
            {
                s += " " + name + "=\"" + value + "\"";
            }

            return s.Substring(1); ;
        }
        #endregion

        #region Преобоазования
        private static CultureInfo _Culture = null;
        public static CultureInfo Culture
        {
            get
            {
                if (_Culture == null)
                {
                    _Culture = new CultureInfo("en-US");
                    //_Culture.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd";
                    _Culture.DateTimeFormat.FullDateTimePattern = "dd.MM.yyyy HH:mm:ss";
                    _Culture.DateTimeFormat.LongDatePattern = "dd.MM.yyyy";
                    _Culture.DateTimeFormat.ShortDatePattern = "dd.MM.yy";

                    _Culture.NumberFormat.CurrencyDecimalSeparator = ".";
                }
                return _Culture;
            }
        }
        public static DateTime NULLDateTime = new DateTime(1753, 1, 1, 12, 0, 0);
        private static string decimalSeparator = "";
        public static string DecimalSeparator
        {
            get
            {
                if (decimalSeparator == "")
                {
                    try
                    {
                        Convert.ToDouble("1.1");
                        decimalSeparator = ".";
                    }
                    catch (Exception)
                    {
                        decimalSeparator = ",";
                    }
                }
                return decimalSeparator;
            }
        }
        public static bool StringToBool(string val, bool def = false)
        {
            bool result = def;
            try
            {
                if (val != null)
                {
                    result = val == "1" || val.ToLower() == "true";
                }
            }
            catch { }
            return result;
        }
        public static int StringToInt(string val, int def = 0)
        {
            try
            {
                def = int.Parse(val);
            }
            catch { }
            return def;
        }
        public static long StringToLong(string val, long def = 0)
        {
            try
            {
                def = long.Parse(val);
            }
            catch { }
            return def;
        }
        public static double StringToDouble(String value, double def = 0)
        {
            if (value == null || value.Trim() == "") return def;
            value = value.Replace(",", DecimalSeparator);
            value = value.Replace(".", DecimalSeparator);
            try
            {
                return Convert.ToDouble(value);
            }
            catch { }
            return def;
        }
        public static decimal StringToDecimal(String value, decimal def = 0)
        {
            if (value == null || value.Trim() == "") return def;
            value = value.Replace(",", DecimalSeparator);
            value = value.Replace(".", DecimalSeparator);
            try
            {
                return Convert.ToDecimal(value);
            }
            catch { }
            return def;
        }
        public static DateTime StringToDate(string value, DateTime def = default(DateTime))
        {
            DateTime result = def;
            try
            {
                result = DateTime.Parse(value, Culture);
            }
            catch
            {
                result = def;
            }
            return result;
        }
        public static Guid StringToGuid(String value, Guid def = default(Guid))
        {
            Guid result = def;
            try
            {
                result = new Guid(value);
            }
            catch { }
            return result;
        }
        public static bool IsInteger(string s)
        {
            if (s == null || s.Length == 0) return false;
            bool f = true;
            foreach (char c in s)
            {
                if (f)
                {
                    if (!((c >= '0' && c <= '9') || c == '-' || c == '+')) return false;
                    f = false;
                }
                else
                {
                    if (!((c >= '0' && c <= '9'))) return false;
                }
            }
            return true;
        }
        static public bool IsDouble(string s)
        {
            if (s == null || s.Length == 0) return false;
            bool f = true;
            foreach (char c in s)
            {
                if (f)
                {
                    if (!((c >= '0' && c <= '9') || c == '-' || c == '+' || c == '.' || c == ',')) return false;
                    f = false;
                }
                else
                {
                    if (!((c >= '0' && c <= '9' || c == '.' || c == ','))) return false;
                }
            }
            return true;
        }
        static public bool IsGuid(string s)
        {
            if (s == null || s.Length == 0) return false;
            try
            {
                Guid g = new Guid(s);
                return true;
            }
            catch { }
            return false;
        }
        public static string DoubleToString(double value, int dec = -1)
        {
            string result = "";
            if (dec < 0) result = value.ToString("F" + dec.ToString());
            else result = value.ToString("F" + dec.ToString());
            return result.Replace(',', '.');
        }
        public static string DecimalToString(decimal value, int dec = -1)
        {
            string result = "";
            if (dec < 0) result = value.ToString("F" + dec.ToString());
            else result = value.ToString("F" + dec.ToString());
            return result.Replace(',', '.');
        }
        public static string DateToString(DateTime d)
        {
            return d.ToString(Culture.DateTimeFormat.ShortDatePattern);
        }
        public static string DateTimeToString(DateTime d)
        {
            return d.ToString(Culture.DateTimeFormat.FullDateTimePattern);
        }
        public static string ListToString(IEnumerable list, object emptylistobj = null, string delim = "\r\n")
        {
            string s = "";
            if (list != null)
            {
                foreach (object o in list)
                {
                    if (s != "") s += delim;
                    s += o.ToString();
                }
                if (s == "" && emptylistobj != null)
                {
                    s = emptylistobj.ToString();
                }
            }
            return s;
        }
        #endregion
    }
}
