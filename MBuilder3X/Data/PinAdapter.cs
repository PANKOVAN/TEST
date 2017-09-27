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
    public class PinAdapter : MBuilder3XDataAdapter
    {
        public override object Run(MBuilderModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);

            // Прикнопить 
                model.Exec($@"if exists(select 1 from [MRelation] where RelationTypeId='PIN' and OwnerId={model.AddParam(currentUser.User.Id)} and ChildId in ({GetParm("id", "")}))
                              delete [MRelation] where RelationTypeId='PIN' and OwnerId={model.AddParam(currentUser.User.Id)} and ChildId in ({GetParm("id", "")}) 
                              else insert [MRelation] (RelationTypeId, OwnerId, ChildId) select 'PIN', {model.AddParam(currentUser.User.Id)}, id from [MEntity] where Id in ({GetParm("id", "")})  
                             ");
            // Чтение списка
            MPaginationList l = new MPaginationList(parms);
            List<MArt> la = new List<MArt>();
            model.Select(l, typeof(MEntity), $"select  * from [MEntity] (nolock) where Id in ({GetParm("id", "")})");
            if (l.Count > 0)
            {
                foreach (MEntity e in l)
                {
                    e.IsPin = false;
                }
                List<MRelation> lr = new List<MRelation>();
                model.Select(lr, typeof(MRelation), $"select  * from [MRelation] (nolock) where RelationTypeId='PIN' and OwnerId={model.AddParam(currentUser.User.Id)} and ChildId in ({l.IdList()})");
                foreach (MRelation r in lr)
                {
                    model.MainDic.GetObj<MEntity>(r.ChildId).IsPin = true;
                }
            }
            return l;
        }
    }
}
