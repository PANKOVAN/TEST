using MEDMCoreLibrary;
using MFuncCoreLibrary;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace M740CoreLibrary
{
    public class Form
    {
        #region Session
        public ISession Session { get; set; }
        #endregion
        public Form(ISession session)
        {
            Session = session;
        }
        public virtual void Run(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
        }
    }
}
