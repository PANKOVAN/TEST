using System;
using Microsoft.AspNetCore.Mvc;
using MJsonCoreLibrary;
using Models.MBuilderModel;

namespace WebixApp.Controllers
{
    public class SysController : MSysController
    {
        public SysController(MBuilderModel model) : base(model)
        {
        }

        public IActionResult Main(string command)
        {
            switch (command.ToLower())
            {
                //case "base":
                //    return BaseClasses();
                case "model":
                    return ModelClasses();
                case "macros":
                    return Macros();
                case "commands":
                    return Commands();
                case "cfg":
                    return CfgDic();
            }
            throw new Exception($"Контроллер sys/main. Неизвестная команда \"{command}\"...");
        }
    }
}