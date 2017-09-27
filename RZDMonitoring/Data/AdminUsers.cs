using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Http;
using Models.RZDMonitoringModel;

namespace MBuilder3X.Data
{
    public class AdminUsersAdapter : MBuilder3XDataAdapter
    {
        public override object Run(RZDMonitoringModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);

            // Чтение списка
            MPaginationList l = new MPaginationList(parms);
            model.Select(l, typeof(MUser), $"select  * from [MUser] (nolock) ");
            return l;
        }
    }
}
