using MEDMCoreLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MJsonCoreLibrary
{
    public class MAppController : MDataController
    {
        public MAppController(MEDM model) : base(model)
        {
        }
        public override IActionResult Run(string command)
        {
            string json = "";
            try
            {
                if (System.IO.File.Exists($"app/{command}.js"))
                {
                    json = System.IO.File.ReadAllText($"app/{command}.js");
                    Response.Headers.Add("Cache-Control", "no-cache");
                    return Content(json, "application/javascript");
                }
                else if (System.IO.File.Exists($"app/{command}.json"))
                {
                    json = System.IO.File.ReadAllText($"app/{command}.json");
                    JToken jt = JToken.Parse(json);
                    Response.Headers.Add("Cache-Control", "no-cache");
                    return Json(jt);
                }
                else
                {
                    json = $@"return ({{'_error_': 'Команда ""{command}"" не найдена...'}}) ";
                    Response.Headers.Add("Cache-Control", "no-cache");
                    return Content(json, "application/javascript");
                }
            }
            catch (Exception e)
            {
                json = $@"return {{'_error_': 'Ошибки при выполнении команды ""{command}"" ({e})'}} ";
                Response.Headers.Add("Cache-Control", "no-cache");
                return Content(json, "application/javascript");
            }
        }
    }
}


/*
using MEDMCoreLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MJsonCoreLibrary
{
    public class MAppController : MDataController
    {
        public MAppController(MEDM model) : base(model)
        {
        }
        public override IActionResult Run(string command)
        {
            JObject jo = new JObject();
            try
            {
                MJsonReader.ReadJson($"app/{command}.json", jo);
            }
            catch (Exception e)
            {
                string mess = e.Message.Replace(@"\", @"\\").Replace(@"'", @"\'");
                jo = JObject.Parse($@"{{'_error_': 'Команда ""{command}"" не найдена...'}} ");
            }
            Response.Headers.Add("Cache-Control", "no-cache");
            if (!MJsonModel.IsCached) MJsonModel.Reload();
            if (!MJsonAppConverter.IsCached) MJsonAppConverter.Reload();
            return Json(jo, MJsonAppConverter.GetSettings(Model));
        }
    }
}
*/