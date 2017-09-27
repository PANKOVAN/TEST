using MEDMCoreLibrary;
using Microsoft.AspNetCore.Http;
using MJsonCoreLibrary;
using Models.MBuilderModel;
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
            return Run(model as MBuilderModel, path, parms);
        }
        public virtual object Run(MBuilderModel model, string path, string parms)
        {
            return base.Run(model, path, parms);
        }
        /*
        public override object Save(MEDM model, string path, string parms, HttpContext context)
        {
            return Save(model as MBuilderModel, path, parms, context);
        }
        public override IEnumerable<string> GetCommands(MEDM model)
        {
            return GetCommands(model as MBuilderModel);
        }
        public virtual object Save(MBuilderModel model, string path, string parms, HttpContext context)
        {
            return base.Save(model, path, parms, context);
        }
        public virtual IEnumerable<string> GetCommands(MBuilderModel model)
        {
            return "Refresh;Add;Edit;Delete".Split(';');
        }

         */
    }
}
