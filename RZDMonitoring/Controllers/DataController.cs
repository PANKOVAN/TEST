using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MJsonCoreLibrary;
using Models.RZDMonitoringModel;
using System.Reflection;

namespace WebixApp.Controllers
{
    public class DataController : MDataController
    {
        public DataController(RZDMonitoringModel model) : base(model)
        {
        }
        [Authorize]
        public IActionResult Main(string command)
        {

            return Run(command);
        }
    }
}