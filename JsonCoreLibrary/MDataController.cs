using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MJsonCoreLibrary
{
    public class MDataController : Controller
    {

        public static MDataAdapter GetDataAdapter(string name, string folder = "Data")
        {
            name += "adapter";
            MDataAdapter da = MFunc.CreateInstanceByName<MDataAdapter>(name, folder, null);
            if (da == null) da = MFunc.CreateInstanceByName<MDataAdapter>("default", folder, null);
            return da;
        }

        public MEDM Model = null;
        public MDataController(MEDM model)
        {
            Model = model;
        }
        public virtual IActionResult Run(string command)
        {

            Response.Headers.Add("Cache-Control", "no-cache");
            try
            {
                MDataAdapter da = GetDataAdapter(command);
                if (da == null) throw new Exception($@"Дата адаптер ""{command}"" не найден.");
                da.Context = HttpContext;
                // Если в параметрах запроса стоит save=1 то считается что мы имеем дело с сохранением изменений
                if (Request.Query["save"] == "1")
                {
                    return Json(da.Save(Model, Request.Path.Value, Request.QueryString.Value), MJsonDataConverter.GetSettings(Model));
                }
                if (Request.Query["commands"] == "1")
                {
                    return Json(da.GetCommands(Model), MJsonDataConverter.GetSettings(Model));
                }
                else
                {
                    if (!MJsonDataConverter.IsCached) MJsonDataConverter.Reload();
                    return Json(MJsonData.Create(da.Run(Model, Request.Path.Value, Request.QueryString.Value)), MJsonDataConverter.GetSettings(Model));
                }
            }
            catch (Exception e)
            {
                MJsonData jd = MJsonData.Create(e);
                return Json(jd);
            }
        }
    }
}
