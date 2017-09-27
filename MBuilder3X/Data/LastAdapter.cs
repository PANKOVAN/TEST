using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Http;
using Models.MBuilderModel;

namespace MBuilder3X.Data
{
    public class LastAdapter : MBuilder3XDataAdapter
    {
        public override object Run(MBuilderModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);

            // Чтение списка
            MPaginationList l = new MPaginationList(parms);
            return l;
        }
    }
}
