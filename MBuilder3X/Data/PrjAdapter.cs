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
    public class PrjAdapter : MBuilder3XDataAdapter
    {
        public override object Run(MBuilderModel model, string path, string parms)
        {
            MUser currentUser = CurrentUser(model);

            // Загрузка файла картинки
            if (GetParm("upload", false))
            {
                int id = GetParm("id", 0);
                string filename = GetParm("name", "");
                string ext = GetParm("type", "").ToLower();
                if (id == 0) throw new Exception("Текуший проект не выбран...");
                if (filename == "") throw new Exception("Файл не задан...");
                if (!(ext == "png" || ext == "jpg" || ext == "bmp" || ext == "svg")) throw new Exception("Разрешены только следущие типы файлов png, jpg, bmp, svg");
                MArt art = model.SelectFirst<MArt>($"select * from [MArt] where EntityId={model.AddParam(id)} and ArtTypeId='IMG'");
                if (art == null)
                {
                    art = model.CreateObject(typeof(MArt)) as MArt;
                    art.EntityId = id;
                    art.ArtTypeId = "IMG";
                }
                art.FileName = filename;
                art.Type = ext;
                model.Save(Context.Session);

                foreach (IFormFile ff in Context.Request.Form.Files)
                {
                    MBuilderModel.Store.Save(ff, art.GID, art.Type);
                    art.Version++;
                    model.Save(Context.Session);
                    break;
                }
                return null;
            }
            // Удаление файла картинки
            if (GetParm("deleteImage", false))
            {
                model.Exec($"delete [MArt] where EntityId in ({GetParm("id", "")}) and ArtTypeId='IMG'");
            }
            // Прикнопить проект
            if (GetParm("pin", false))
            {
                model.Exec($@"if exists(select 1 from [MRelation] where RelationTypeId='PIN' and OwnerId={model.AddParam(currentUser.User.Id)} and ChildId in ({GetParm("id", "")}))
                              delete [MRelation] where RelationTypeId='PIN' and OwnerId={model.AddParam(currentUser.User.Id)} and ChildId in ({GetParm("id", "")}) 
                              else insert [MRelation] (RelationTypeId, OwnerId, ChildId) select 'PIN', {model.AddParam(currentUser.User.Id)}, id from [MEntity] where EntityTypeId='PRJ' and Id in ({GetParm("id", "")})  
                             ");
            }
            // Чтение списка проектов
            MPaginationList l = new MPaginationList(parms);
            List<MArt> la = new List<MArt>();
            model.Select(l, typeof(MEntity), $"select  * from [MEntity] (nolock) where EntityTypeId='PRJ'");
            if (l.Count > 0)
            {
                model.Select(la, typeof(MArt), $"select  * from [MArt] (nolock) where EntityId in ({l.IdList()})");
                foreach (MArt art in la)
                {
                    art.Entity.Arts.Add(art);
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
