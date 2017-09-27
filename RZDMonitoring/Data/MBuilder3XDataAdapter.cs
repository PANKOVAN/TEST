using MEDMCoreLibrary;
using Microsoft.AspNetCore.Http;
using MJsonCoreLibrary;
using Models.RZDMonitoringModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MBuilder3X.Data
{
    public class MBuilder3XDataAdapter : MDataAdapter
    {
        public MUser CurrentUser(MEDM model)
        {
            MUser currentUser = model.GetCurrentUser(Context.User) as MUser;
            return currentUser;
        }

        public override object Run(MEDM model, string path, string parms)
        {
            return Run(model as RZDMonitoringModel, path, parms);
        }
        public virtual object Run(RZDMonitoringModel model, string path, string parms)
        {
            return base.Run(model, path, parms);
        }
        /*
        public override object Save(MEDM model, string path, string parms, HttpContext context)
        {
            return Save(model as RZDMonitoringModel, path, parms, context);
        }
        public override IEnumerable<string> GetCommands(MEDM model)
        {
            return GetCommands(model as RZDMonitoringModel);
        }
        public virtual object Save(RZDMonitoringModel model, string path, string parms, HttpContext context)
        {
            return base.Save(model, path, parms, context);
        }
        public virtual IEnumerable<string> GetCommands(RZDMonitoringModel model)
        {
            return "Refresh;Add;Edit;Delete".Split(';');
        }

         */
    }
}
