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
    public class UsrAdapter : MBuilder3XDataAdapter
    {

        public override object Run(MBuilderModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);

            // Чтение списка пользователей
            MPaginationList l = new MPaginationList(parms);
            List<MArt> la = new List<MArt>();

            string clsIdList = ClsAdapter.GetParenIdList(GetParm("clsid", ""), "USR");
            string f = "";
            if (string.IsNullOrEmpty(clsIdList) || GetParm("isAll", false)) f = "--";

            model.Select(l, typeof(MEntity), 
                $@"select  * from [MEntity] (nolock) where EntityTypeId='USR' 
                    and code like {model.AddParam("%"+GetParm("code", "")+"%")} 
                    and name like {model.AddParam("%" + GetParm("name", "") + "%")} 
                    {f} and clsId in ({clsIdList})"
                    );

            if (l.Count > 0)
            {
                model.Select(la, typeof(MArt), $"select  * from [MArt] (nolock) where EntityId in ({l.IdList()})");
                foreach (MArt art in la)
                {
                    art.Entity.Arts.Add(art);
                }
            }
            return l;
        }
    }
}
