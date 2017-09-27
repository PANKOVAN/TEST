
using MEDMCoreLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
namespace MJsonCoreLibrary
{
    /// <summary>
    /// Системный контроллер. Базовый класс
    /// </summary>
    public class MSysController : Controller
    {
        public static string MacroPath = "macros";
        public static string CommandsPath = "commands";
        public static string ModelPath = "model";
        public MEDM Model = null;


        public MSysController(MEDM model)
        {
            Model = model;
        }
        /// <summary>
        /// Вернуть описание базовых классов модели в формате JSON
        /// 
        /// {
        ///     "имя класса": {
        ///         "имя свойста":"значение по умолчанию|описатель ссылки на объект"
        ///         ...
        ///         "имя свойста":"значение по умолчанию|описатель ссылки на объект"
        ///     },
        ///     ...
        ///     "имя класса": {
        ///         "имя свойста":"значение по умолчанию|описатель ссылки на объект"
        ///         ...
        ///         "имя свойста":"значение по умолчанию|описатель ссылки на объект"
        ///     }
        /// }
        /// </summary>
        /// <returns></returns>
        /// 
        /*
        public IActionResult BaseClasses()
        {
            JObject stub = new JObject();
            try
            {
                foreach (Type t in Model.GetType().GetTypeInfo().Assembly.GetTypes())
                {
                    TypeInfo ti = t.GetTypeInfo();
                    if (ti.IsSubclassOf(typeof(MEDMObj)))
                    {
                        JObject jclass = new JObject();
                        foreach (PropertyInfo pi in t.GetProperties())
                        {
                            if (pi.CanRead)
                            {
                                string n = JFunc.WebixName(pi.Name);
                                TypeInfo pt = pi.PropertyType.GetTypeInfo();
                                if (pi.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null && pi.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
                                {
                                    if (pt.IsSubclassOf(typeof(MEDMObj)))
                                    {
                                        JObject rclass = new JObject();
                                        rclass.Add("_run", $@"function(){{Object.defineProperty(this, ""{n}"", {{ set: function (value) {{m.macro.setRValue(this, ""{n}"", value); }}, get: function () {{return m.macro.getRValue(this, ""{n}""); }} }});}}");
                                        jclass.Add(n, rclass);
                                        JObject _rclass = new JObject();
                                        _rclass.Add("_type", new JValue(pt.Name));
                                        jclass.Add("_" + n, _rclass);
                                    }
                                    else
                                    {
                                        jclass.Add(JFunc.WebixName(n), new JValue(MJsonDataConverter.GetDefaulValue(pi.PropertyType)));
                                    }
                                }
                            }
                        }
                        stub.Add(ti.Name, jclass);
                    }
                }
            }
            catch (Exception e)
            {
                string mess = e.Message.Replace(@"\", @"\\").Replace(@"'", @"\'");
                stub = JObject.Parse($@"{{'_error_': 'Ошибки при формировании базовых классов модели ({e})'}} ");
            }

            Response.Headers.Add("Cache-Control", "no-cache");
            return Json(stub);
        }
        */
        /// <summary>
        /// Вернуть описатель макрокоманд в формате JavaScript
        /// m.macro.add({
        /// имя макро: значение макро,
        /// ...
        /// имя макро: значение макро
        /// });
        /// Допустимы следующие значения макро
        /// - литерал;
        /// - объект;
        /// - массив;
        /// - функция;
        /// Подстановка макро 
        /// 1. Макро используется в левой части, в этом случае значением макро может быть 
        /// либо объект либо функция возвращающая объект, при подстановке свойства объекта 
        /// с их значениями заменяют макро. Тело макро может содержать другие макро
        /// 
        /// @имя макро: {
        ///     параметры макро
        /// }
        /// 
        /// 2. Макро используется в правой части, в этом случае для подстановки значения макро
        /// используется функция m.macro.value(имя макро, {параметры макро})
        /// </summary>
        /// <returns></returns>
        public IActionResult Macros()
        {
            string macros = "";
            try
            {
                Assembly ass = typeof(MSysController).GetTypeInfo().Assembly;
                // Начитать системные макро
                foreach (string resname in ass.GetManifestResourceNames())
                {
                    if (Path.GetExtension(resname).ToLower() == ".js")
                    {
                        Stream stream = ass.GetManifestResourceStream(resname);
                        TextReader tr = new StreamReader(stream);
                        macros += tr.ReadToEnd() + "\r\n";
                    }
                }
                // Начитать макро текущего проекта
                foreach (string filename in Directory.GetFiles(MacroPath, "*.js", SearchOption.AllDirectories))
                {
                    macros += System.IO.File.ReadAllText(filename) + "\r\n";
                }
            }
            catch (Exception e)
            {
                macros = $@"{{'_error_': 'Ошибки при начитке макро ({e})'}} ";
            }

            Response.Headers.Add("Cache-Control", "no-cache");
            return Content(macros, "application/javascript", Encoding.UTF8);
        }
        public IActionResult Commands()
        {
            string commands = "";
            try
            {
                // Начитать команды текущего проекта
                foreach (string filename in Directory.GetFiles(CommandsPath, "*.js", SearchOption.AllDirectories))
                {
                    commands += System.IO.File.ReadAllText(filename) + "\r\n";
                }
            }
            catch (Exception e)
            {
                commands = $@"{{'_error_': 'Ошибки при начитке команды ({e})'}} ";
            }

            Response.Headers.Add("Cache-Control", "no-cache");
            return Content(commands, "application/javascript", Encoding.UTF8);
        }
        /// <summary>
        /// Вернуть описание модели в формате JS
        /// 
        /// {
        /// имя папки: {
        ///     _type:'folder'
        ///     _header: 'заголовок'
        ///     имя класса: {
        ///         _type:'class'
        ///         _header: 'заголовок'
        ///         имя свойства: {
        ///             _type:'class',
        ///             _header: 'заголовок',
        ///             _datatype: 'тип данных',
        ///             _element: {
        ///                 описателе для elements в формах
        ///             }
        ///             _column: {
        ///                 описателе для columns в таблицах и т.д.
        ///             }
        ///         },
        ///         ...
        ///         имя свойства: {
        ///             описатель свойства
        ///         }
        ///     },
        ///     ...
        ///     имя класса: {
        ///         описатель класса
        ///     }
        /// },
        /// ...
        /// имя папки: {
        ///     описатель папки
        /// }
        /// }
        /// 
        /// Папки могут быть вложенные и используются просто для структурирования. 
        /// При описании классов и свойств можно использовать макро
        /// </summary>
        /// <returns></returns>
        public IActionResult ModelClasses()
        {
            /*
            string model = "";
            try
            {
                foreach (string filename in Directory.GetFiles(ModelPath, "*.js", SearchOption.AllDirectories))
                {
                    model += System.IO.File.ReadAllText(filename) + "\r\n";
                }
            }
            catch (Exception e)
            {
                model = $@"{{'_error_': 'Ошибки при начитке модели ({e})'}} ";
            }
            Response.Headers.Add("Cache-Control", "no-cache");
            return Content(model, "application/javascript", Encoding.UTF8);
            */


            JObject stub = new JObject();
            try
            {
                // Набрать все классы какие есть
                HashSet<Type> classes = new HashSet<Type>();
                foreach (Type t in Model.MainDic.Keys)
                {
                    Type t1 = t;
                    while (t1 != null && !t1.GetTypeInfo().IsAbstract && !classes.Contains(t1))
                    {
                        classes.Add(t1);
                        t1 = t1.GetTypeInfo().BaseType;
                    }
                }

                // Проход по классам
                foreach (Type t in classes)
                {
                    TypeInfo ti = t.GetTypeInfo();
                    DbTableAttribute ta = ti.GetCustomAttribute<DbTableAttribute>();
                    EDMClassAttribute ca = ti.GetCustomAttribute<EDMClassAttribute>();

                    if (ti.IsSubclassOf(typeof(MEDMObj)))
                    {
                        JObject jclass = new JObject();
                        jclass.Add("_mtype", new JValue("class"));
                        jclass.Add("_header", new JValue(ca.Header));
                        jclass.Add("_base", new JValue(ti.BaseType.Name));

                        // Проход по свойствам
                        foreach (PropertyInfo pi in t.GetProperties())
                        {

                            DbColumnAttribute da = pi.GetCustomAttribute<DbColumnAttribute>();
                            EDMPropertyAttribute pa = pi.GetCustomAttribute<EDMPropertyAttribute>();
                            if (da != null || pa != null)
                            {
                                string n = JFunc.WebixName(pi.Name);
                                try
                                {
                                    TypeInfo pt = pi.PropertyType.GetTypeInfo();
                                    if (pi.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null && pi.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
                                    {
                                        JObject jprop = new JObject();
                                        jprop.Add("_mtype", new JValue("prop"));
                                        if (pa != null)
                                        {
                                            jprop.Add("_header", new JValue(pa.Header));
                                            jprop.Add("_type", new JValue(pa.ItemType.Name));
                                            jprop.Add("_ptype", new JValue(pa.PropType));
                                            jprop.Add("_default", new JValue(MJsonDataConverter.GetDefaulValue(pi.PropertyType)));
                                            jprop.Add("_visible", new JValue(true));
                                        }
                                        else
                                        {
                                            jprop.Add("_default", new JValue(MJsonDataConverter.GetDefaulValue(pi.PropertyType)));
                                            jprop.Add("_visible", new JValue(false));
                                        }
                                        jclass.Add(JFunc.WebixName(n), jprop);
                                    }
                                }
                                catch (Exception e1)
                                {
                                    stub = JObject.Parse($@"{{'_error_': ' Ошибки при формировании базовых классов модели [{t.Name}({n})] ({e1})'}} ");
                                }
                            }
                        }
                        stub.Add(ti.Name, jclass);
                    }
                }
            }
            catch (Exception e)
            {
                string mess = e.Message.Replace(@"\", @"\\").Replace(@"'", @"\'");
                stub = JObject.Parse($@"{{'_error_': 'Ошибки при формировании базовых классов модели ({e})'}} ");
            }

            Response.Headers.Add("Cache-Control", "no-cache");
            return Json(stub);
        }
        public IActionResult CfgDic()
        {
            Response.Headers.Add("Cache-Control", "no-cache");
            try
            {
                List<object> l = new List<object>();
                foreach (Type t in Model.MainDic.Keys)
                {
                    if (t.GetTypeInfo().IsSubclassOf(typeof(MEDMCfgObj)))
                    {
                        foreach(object o in Model.MainDic[t].Values)
                        {
                            l.Add(o);
                        }
                    }
                }
                return Json(MJsonData.Create(l),  MJsonDataConverter.GetSettings(Model));
            }
            catch (Exception e)
            {
                MJsonData jd = MJsonData.Create(e);
                return Json(jd);
            }
        }
    }
}
