using System;
using System.Collections.Generic;

namespace LSS.HCM.Core.DataObjects.Settings
{
    //[Serializable]
    public class LockerConfiguration
    {
        public string LockerId { get; set; }
        public List<Compartment> Compartments { get; set; }
    }
}
