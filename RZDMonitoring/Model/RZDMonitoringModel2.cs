using MEDMCoreLibrary;
using MFuncCoreLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Models.RZDMonitoringModel
{
    public partial class RZDMonitoringModel
    {

        public override void InitUsers()
        {
            InitUser("madmin", "Главный администратор", "madmin991188");
            InitUser("guest", "Гость", "guest");
        }
        public override void InitUser(string login, string name, string password)
        {
            MUser user = SelectFirst<MUser>($"select * from MUser where Login={AddParam(login)}");
            if (user == null)
            {

                user = this.CreateObject(typeof(MUser)) as MUser;
                user.Login = login;
                user.Fio = name;
                if (login == "madmin") user.IsAdmin = true;
            }
            if (user.PassCode==0) user.PassCode = MFunc.GetSecurityHash(password);
            Save(null);
        }
        public override void InitCfgs()
        {
        }
        public override void InitCfg(string filename)
        {
        }

        // Хранилище
        public static MEDMStore Store = null;
    }

    public partial class MUser : ICurrentUser
    {
        public string GetUserHeader()
        {
            return Fio;
        }

        public object GetUserId()
        {
            return Id;
        }

        public string GetUserName()
        {
            return Login;
        }

        public bool TestRole(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                switch (role.ToLower())
                {
                    case "iasadmin": return IsAdmin;
                    case "isviewer": return IsViewer;
                    case "isfinder": return IsFinder;
                    case "iseditor": return IsEditor;
                    case "isreditor": return IsREditor;
                    case "ismoderator": return IsModerator;
                    case "istranslator": return IsTranslator;
                }
            }
            return false;
        }

        public bool UserIsAdmin()
        {
            return IsAdmin;
        }

        public bool UserIsReadOnly()
        {
            return !IsEditor && !IsAdmin && !IsModerator && !IsTranslator;
        }
    }
}
