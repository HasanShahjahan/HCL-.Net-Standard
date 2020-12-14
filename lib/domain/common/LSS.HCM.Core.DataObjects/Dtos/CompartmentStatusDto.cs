using LSS.HCM.Core.Common.Exceptions;
using System.Collections.Generic;

namespace LSS.HCM.Core.DataObjects.Dtos
{
    public class CompartmentStatusDto : ApplicationError
    {
        public List<Entities.Locker.Compartment> Compartments { get; set; }
    }
}
