using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MJsonCoreLibrary;
using Models.RZDMonitoringModel;

namespace WebixApp.Controllers
{
    public class TemplatesController : MTemplateController
    {
        public TemplatesController(RZDMonitoringModel model) : base(model)
        {
        }
        [Authorize]
        public IActionResult Main(string command)
        {
            return Run(command);
        }
        [Authorize]
        public IActionResult List(string command)
        {
            return GetTemplatesList(command);
        }
    }
}