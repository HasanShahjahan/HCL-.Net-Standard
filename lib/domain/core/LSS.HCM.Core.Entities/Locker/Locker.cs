using System;
using System.Collections.Generic;

namespace LSS.HCM.Core.Entities.Locker
{
    /// <summary>
    ///   Represents locker as a sequence of compartment open and it's status.
    ///</summary>
    
    [Serializable]
    public class Locker : ObjectBase 
    {
        /// <summary>
        ///   Initialization information for locker object.
        ///</summary>
        public Locker()
        {
            Compartments = new List<Compartment>();
        }

        /// <summary>
        ///   Initialization information for locker object.
        ///</summary>
        public Locker(List<Compartment> compartments)
        {
            Compartments = compartments;
        }

        /// <summary>
        ///     Gets and sets the list of compartment of locker object.
        /// </summary>
        /// <returns>
        ///     List of compartment with it's status.
        ///</returns>
        public List<Compartment> Compartments { get; set; }
    }
}
