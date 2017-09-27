using System;

namespace MJsonCoreLibrary
{
    public enum MJsonResultType {OK, Error, Logoff }
    public class MJsonResult
    {
        public MJsonResultType Type { get; set; }
        public object Data { get; set; }
        public MJsonResult(MJsonResultType type, object data)
        {
            Type = type;
            Data = data;
        }
    }
}
