using LSS.HCM.Core.Common.Exceptions;
using System.Collections.Generic;

namespace LSS.HCM.Core.DataObjects.Dtos
{
    public class LockerDto : ApplicationError
    {
        public string TransactionId { get; set; }
        public List<Entities.Locker.Compartment> Compartments { get; set; }
    }
}
