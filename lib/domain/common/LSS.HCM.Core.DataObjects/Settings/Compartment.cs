using System;

namespace LSS.HCM.Core.DataObjects.Settings
{
    [Serializable]
    public class Compartment
    {
        public string CompartmentId { get; set; }
        public Code CompartmentCode { get; set; }
        public string CompartmentSize { get; set; }
    }
}
