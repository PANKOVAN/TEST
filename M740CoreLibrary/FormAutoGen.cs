using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using MEDMCoreLibrary;
using MFuncCoreLibrary;
using System.IO;
using System.Xml.XPath;
using Microsoft.AspNetCore.Http;

namespace M740CoreLibrary
{
    public class FormAutoGen : Form
    {
        public FormAutoGen(ISession session): base(session)
        {
        }
        public override void Run(string formName, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            base.Run(formName, model, xrequest, xresponse);
            if (!LoadFormFromXml(formName, model, xrequest, xresponse)) GenFormByDef(formName, model, xrequest, xresponse);
            else GenRowSetsAndPanels(formName, model, xrequest, xresponse);
        }
        public virtual bool LoadFormFromXml(string formName, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            string formpath = Path.Combine(MFunc.GetBaseDir(AppContext.BaseDirectory), "Forms", formName + ".xml");
            if (System.IO.File.Exists(formpath))
            {
                XmlDocument xdoc = new XmlDocument();
                FileStream fs = new FileStream(formpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                xdoc.Load(fs);
                fs.Dispose();
                string s = xdoc.InnerXml;
                int i = s.IndexOf("?>");
                if (i > 0) s = s.Substring(i + 2);
                xresponse.InnerXml = s;
                return true;
            }
            return false;
        }
        public virtual bool GenFormByDef(string name, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            MEDMDefClass ds = MEDMDefModel.MainDef.Find(name);
            if (ds != null)
            {
                XmlNode xform = XFunc.Append(xresponse, "form", "name", ds.Name.ToLower(), "caption", ds.Header);
                XmlNode xrowsets = XFunc.Append(xform, "rowsets");
                XmlNode xrowset = XFunc.Append(xrowsets, "rowset", "rowset", ds.Name.ToLower(), "datasourse", ds.Name.ToLower());
                XmlNode xpanels = XFunc.Append(xform, "panels");
                XmlNode xpanel = XFunc.Append(xpanels, "panel", "type", "grid", "caption", ds.Header, "rowset", ds.Name.ToLower());
                XmlNode xtoolbar = XFunc.Append(xpanel, "toolbar", "default", "1");
                foreach (MEDMDefProperty dp in ds.Properties)
                {
                    if (dp.Name.ToLower() != "id")
                    {
                        XmlNode xfield = XFunc.Append(xpanel, "field", "name", dp.Name.ToLower());
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Генерит описания источников данных, по описанию модели.
        /// Описание для источника генерится только в том случае если своего описание rowset не имеет
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="model"></param>
        /// <param name="xrequest"></param>
        /// <param name="xresponse"></param>
        public virtual void GenRowSetsAndPanels(string formName, MEDMSql model, XmlNode xrequest, XmlNode xresponse)
        {
            foreach (XmlNode xpanel in xresponse.SelectNodes("descendant::panel"))
            {
                if ( XFunc.GetAttr(xpanel, "autogen", false))
                {
                    string name = XFunc.GetAttr(xpanel, "rowset", "");
                    if (name != "")
                    {
                        MEDMDefClass ds = MEDMDefModel.MainDef.Find(name);
                        if (ds != null)
                        {
                            if (ds.Properties.Count > 0)
                            {
                                if (xpanel.SelectSingleNode("toolbar") == null)
                                {
                                    XFunc.Append(xpanel, "toolbar", "default", "1");
                                }
                                if (xpanel.SelectSingleNode("fields") == null)
                                {
                                    XmlNode xfields = XFunc.Append(xpanel, "fields");
                                    foreach (MEDMDefProperty dp in ds.Properties)
                                    {
                                        if (dp.IsVisible)
                                        {
                                            if (!MEDMObj.IsEmptyId(dp.RefClassId))
                                            {
                                                XmlNode xreffield = XFunc.Append(xfields, "field", "name", (dp.GetRefName() + "_tostring_").ToLower(), "stretch", "1");
                                            }
                                            else
                                            {
                                                if (dp.IsInterval)
                                                {
                                                    XFunc.Append(xfields, "field", "name", (dp.Name+".Min").ToLower());
                                                    XFunc.Append(xfields, "field", "name", (dp.Name+".Max").ToLower());
                                                }
                                                else
                                                {
                                                    XFunc.Append(xfields, "field", "name", dp.Name.ToLower(),"stretch",dp.DataType=="string"?"1":"0");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
