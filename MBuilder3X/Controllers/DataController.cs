using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MJsonCoreLibrary;
using Models.MBuilderModel;
using System.Reflection;

namespace WebixApp.Controllers
{
    public class DataController : MDataController
    {
        public DataController(MBuilderModel model) : base(model)
        {
        }
        [Authorize]
        public IActionResult Main(string command)
        {

            return Run(command);
        }
    }
}