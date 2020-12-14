using System;
using System.Collections.Generic;

namespace LSS.HCM.Core.Entities.Locker
{
    [Serializable]
    public class Locker : ObjectBase 
    {
        public Locker()
        {
            Compartments = new List<Compartment>();
        }
        public Locker(List<Compartment> compartments)
        {
            Compartments = compartments;
        }
        public List<Compartment> Compartments { get; set; }
    }
}
