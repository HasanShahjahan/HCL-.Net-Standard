using System;

namespace LSS.HCM.Core.DataObjects.Settings
{
    [Serializable]
    public class Code
    {
        public string Odbmod { get; set; }
        public string Odbid { get; set; }
        public string Lcbmod { get; set; }
        public string Lcbid { get; set; }
    }
}
