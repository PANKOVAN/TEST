using MEDMCoreLibrary;
using MFuncCoreLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Models.MBuilderModel
{
    public partial class MBuilderModel
    {
        public override void InitUsers()
        {
            InitUser("madmin", "Главный администратор", "madmin991188");
            InitUser("guest", "Гость", "guest");
        }
        public override void InitUser(string login, string name, string password)
        {
            MEntity entity = SelectFirst<MEntity>($"select * from MEntity where EntityTypeId='USR' and code={AddParam(login)}");
            if (entity == null)
            {
                entity = MainDic.GetObj<MEntity>(null);
                entity.Code = login;
                entity.Name = name;
                entity.EntityTypeId = "USR";
            }
            
            MPassword pass= SelectFirst<MPassword>($"select * from MPassword where Id={AddParam(login)}");
            if (pass==null)
            {
                pass = MainDic.GetObj<MPassword>(login);
                pass.PassCode = MFunc.GetSecurityHash(password);
            }
            Save(null);
            
        }
        public override void InitCfgs()
        {
            foreach (string filename in Directory.GetFiles(Path.Combine(BaseDir, "Model", "Cfg"), "*.xml", SearchOption.AllDirectories)) {
                InitCfg(filename);
            }
        }
        public override void InitCfg(string filename)
        {
            MainTrace.Add(TraceType.Cfg, $"file => {Path.GetFileName(filename)} ");
            XmlDocument xdoc = XFunc.Load(filename);
            foreach(XmlNode cfgNode in xdoc.SelectNodes("descendant::cfg"))
            {
                foreach(XmlNode typeNode in cfgNode.SelectNodes("*"))
                {
                    Type t = GetClassTypeByClassName(typeNode.Name);
                    if (t != null)
                    {
                        MainTrace.Add(TraceType.Cfg, $"Class => {t.Name}");
                        foreach(XmlNode itemNode in typeNode.SelectNodes("*"))
                        {
                            LockUpdates++;
                            try
                            {
                                MEDMObj obj = MainDic.GetObj(t, itemNode.Name);
                                obj.SetValues(itemNode);
                                MainTrace.Add(TraceType.Cfg, $"Item => {itemNode.Name} ({obj})");
                            }
                            finally
                            {
                                LockUpdates--;
                            }
                        }
                    }
                    else
                    {
                        MainTrace.Add(TraceType.Error, $"Для узла конфигурации {typeNode.Name} класс не найден");
                    }
                }
            }
        }

        // Хранилище
        public static MEDMStore Store = null;
    }
    public class MUser : ICurrentUser
    {
        public MEntity User { get; set; }
        public MUser(MEntity user)
        {
            User = user;
        }

        public string GetUserHeader()
        {
            return User.Name;
        }

        public object GetUserId()
        {
            return User.Id;
        }

        public string GetUserName()
        {
            return User.Code;
        }

        public bool TestRole(string role)
        {
            return false;
        }

        public bool UserIsAdmin()
        {
            return User.Code.ToLower()=="madmin";
        }

        public bool UserIsReadOnly()
        {
            return User.Code.ToLower() == "guest";
        }
    }

    public partial class MArt
    {
        public string ArtPath
        {
            get
            {
                return MEDMStore.GetStorePath(this.GID, this.Type, this.Version);
            }
        }
    }
}
