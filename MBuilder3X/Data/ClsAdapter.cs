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
    public class ClsAdapter : MBuilder3XDataAdapter
    {
        public static string GetParenIdList(string parentId, string clsType)
        {
            return parentId;
        }
        public override object Run(MBuilderModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);
            string clsType = GetParm("clsType", "");
            int parentId = GetParm("parentId", 0);
            if (GetParm("clstypelist", false))
            {
                List<MClsType> lt = model.MainDic.GetAll<MClsType>((object o)=>(clsType=="" || clsType==((MClsType)o).Id));
                return lt;
            }

            MTreeList l = new MTreeList(parentId);
            
            model.Select(l, typeof(MCls), $"select  * from [MCls] (nolock) where ClsTypeId={model.AddParam(clsType)} and ParentId={model.AddParam(parentId)}" );

            List<MCls> k = new List<MCls>();
            if (clsType != "")
            {
                model.Select(k, typeof(MCls), $"select * from [MCls] where id in ({l.IdList()}) and id in (select ParentId from MCls)");
                foreach (MCls cls in l)
                {
                    cls.webix_kids = true;
                }
            }
            return l;
        }
    }
}
