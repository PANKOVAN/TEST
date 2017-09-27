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
    public class FavouritesAdapter : MBuilder3XDataAdapter
    {
        public override object Run(MBuilderModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);

            MPaginationList l = new MPaginationList(parms);
            List<MArt> la = new List<MArt>();
            model.Select(l, typeof(MEntity), $"select  * from [MEntity] (nolock) where Id in (select ChildId from [MRelation] where RelationTypeId='PIN' and OwnerId={model.AddParam(currentUser.User.Id)})");
            if (l.Count > 0)
            {
                model.Select(la, typeof(MArt), $"select  * from [MArt] (nolock) where EntityId in ({l.IdList()})");
                foreach (MArt art in la)
                {
                    art.Entity.Arts.Add(art);
                }
                foreach (MEntity e in l)
                {
                    e.IsPin = true;
                }
            }
            return l;
        }
    }
}
