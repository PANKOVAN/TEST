using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MEDMCoreLibrary
{
    public class MEDMStore
    {
        #region Папка хранилища
        private string _StorePath = "";
        public string StorePath
        {
            get
            {
                return _StorePath;
            }
            set
            {
                _StorePath = value;
            }
        }
        #endregion
        #region Папка с данными
        private string _DataPath = "";
        public string DataPath
        {
            get
            {
                return Path.Combine(StorePath, "Data");
            }
        }
        #endregion
        #region Конструктор
        public MEDMStore(string path)
        {
            StorePath = path;
        }

        public static string GetStorePath(Guid gid, string ext, int version=-1)
        {
            //BEC721F2-6235-4045-A41A-4284FCBF663C
            int b1 = 0;
            int b2 = 0;
            byte[] a = gid.ToByteArray();
            for (int i = 0; i < 16; i += 2)
            {
                b1 = b1 + a[i];
                b2 = b2 + a[i + 1];
            }
            b1 = b1 % 256;
            b2 = b2 % 256;
            if (version<0) return $"M{b1.ToString().PadLeft(3, '0')}/M{b2.ToString().PadLeft(3, '0')}/${gid}.{ext}";
            return $"M{b1.ToString().PadLeft(3, '0')}/M{b2.ToString().PadLeft(3, '0')}/${gid}.{ext}?v={version}";

        }
        public void Save(Stream saveStream, Guid gid, string ext)
        {
            string fn = Path.Combine(DataPath, GetStorePath(gid, ext));
            Directory.CreateDirectory(Path.GetDirectoryName(fn));
            FileStream fs = new FileStream(fn, FileMode.Create, FileAccess.Write, FileShare.Read);
            saveStream.CopyTo(fs);
            fs.Flush();
            fs.Dispose();
        }
        public void Save(IFormFile saveStream, Guid gid, string ext)
        {
            string fn = Path.Combine(DataPath, GetStorePath(gid, ext));
            Directory.CreateDirectory(Path.GetDirectoryName(fn));
            FileStream fs = new FileStream(fn, FileMode.Create, FileAccess.Write, FileShare.Read);
            saveStream.CopyTo(fs);
            fs.Flush();
            fs.Dispose();
        }
        #endregion
    }
}
