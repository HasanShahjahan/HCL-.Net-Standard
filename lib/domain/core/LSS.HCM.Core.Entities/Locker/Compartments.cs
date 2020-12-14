using System;

namespace LSS.HCM.Core.Entities.Locker
{
    [Serializable]
    public class Compartments 
    {
        public string LockerId { get; set; }
        public string[] CompartmentIds { get; set; }
    }
}
