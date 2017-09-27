using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MJsonCoreLibrary;
using Models.MBuilderModel;

namespace WebixApp.Controllers
{
    public class TemplatesController : MTemplateController
    {
        public TemplatesController(MBuilderModel model) : base(model)
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