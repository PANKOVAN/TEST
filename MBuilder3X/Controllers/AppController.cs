using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MJsonCoreLibrary;
using Models.MBuilderModel;
using MBuilder3X.Data;

namespace WebixApp.Controllers
{
    public class AppController : MAppController
    {
        public AppController(MBuilderModel model) : base(model)
        {
        }

        [Authorize]
        public IActionResult Main(string command)
        {
            return Run(command);
        }


    }
}