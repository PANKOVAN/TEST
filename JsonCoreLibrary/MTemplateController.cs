using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MJsonCoreLibrary
{
    public class MTemplateController: Controller 
    {

        public MEDM Model = null;
        public MTemplateController(MEDM model)
        {
            Model = model;
        }
        public virtual string GetTemplatePath(string command)
        {
            string path = HttpContext.Request.Query["path"];

            return System.IO.Path.Combine("templates",path, command+".html");
        }
        private string GetTemplateItem(Match m)
        {
            if (string.IsNullOrEmpty(m.Value)) return "";
            string s = m.Value.Substring(1, m.Value.Length - 2).Trim();
            if (string.IsNullOrEmpty(s)) return "";

            string fn = "value";

            if (s.StartsWith("! "))
            {
                fn = "html";
                s = s.Substring(2).Trim();
            }
            else if (s.StartsWith("!"))
            {
                fn = s.Split(' ')[0].Substring(1).Trim();
                s = s.Substring(fn.Length+1).Trim();
            }
            int i = s.IndexOf('@');
            string p = "";
            string v = "";
            if (i >= 0) {
                v = s.Substring(0, i).Replace("#", "_o_.").Replace("\"", "\\\"");
                foreach(string p1 in s.Substring(i).Split('@'))
                {
                    if (!string.IsNullOrEmpty(p1))
                    {
                        if (p != "") p += ",";
                        int j = p1.IndexOf('=');
                        string n = "_";
                        string p2 = "";
                        if (j>0)
                        {
                            n = p1.Substring(0, j);
                            p2 = p1.Substring(j + 1);
                        }
                        else if (j == 0)
                        {
                            p2 = p1.Substring(j + 1);
                        }
                        else
                        {
                            p2 = p1;
                        }
                        p += $@"{n}:""{p2}""";
                    }
                }
            }
            else
            {
                v = s.Replace("#", "_o_.").Replace("\"", "\\\"");
            }
            return $@"""+m.templates._{fn}(_o_, {v},_(_{p}_)_)+""";
        }
        public virtual IActionResult Run(string command)
        {

            Response.Headers.Add("Cache-Control", "no-cache");
            string fn = GetTemplatePath(command);
            try
            {
                if (System.IO.File.Exists(fn))
                {
                    string s = System.IO.File.ReadAllText(fn);
                    s = s.Replace("\"", "\\\"");
                    //s = s.Replace("\r", " ");
                    //s = s.Replace("\n", " ");
                    Regex r1 = new Regex(@"\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    s = r1.Replace(s, " ");
                    Regex r = new Regex(@"\{[^\{\}]+\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    try
                    {
                        while (r.IsMatch(s))
                        {
                            s = r.Replace(s, new MatchEvaluator(GetTemplateItem));
                        }
                    }
                    catch (Exception e)
                    {
                        return Content($@"<div>Ошибки при разборе шаблона ""{fn}"" не найден ({e})...</div>");
                    }
                    s = s.Replace("_(_", "{").Replace("_)_", "}").Replace("+\"\"", "").Replace("+\" \"", "");
                    s = "function(_o_){return \"" + s + "\"}";
                    return Content(s, "text/html");
                }
                else
                {
                    return Content($@"<div>Файл шалона ""{fn}"" не найден...</div>");
                }
            }
            catch (Exception e)
            {
                return Content($@"<div>Ошибки при загрузке шаблона ""{fn}"" ({e}) ...</div>");
            }
        }
        public virtual IActionResult GetTemplatesList(string command)
        {
            Response.Headers.Add("Cache-Control", "no-cache");
            try
            {
                List<MTemplateItem> l = new List<MTemplateItem>();
                string path = HttpContext.Request.Query["path"];
                foreach(string fn in System.IO.Directory.GetFiles(System.IO.Path.Combine("templates", path), "*.html", System.IO.SearchOption.AllDirectories))
                {
                    string fn1 = System.IO.Path.GetFileNameWithoutExtension(fn);
                    if (!fn1.StartsWith("_"))
                    {
                        l.Add(new MTemplateItem(fn1, fn1, fn));
                    }
                }
                return Json(l);
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибки при получении списка шаблонов ({e}) ... ");
            }
        }
    }
    public class MTemplateItem
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public MTemplateItem(string id, string value, string path)
        {
            Id = id;
            Value = value;
            Path = path;
        }
    }
}
