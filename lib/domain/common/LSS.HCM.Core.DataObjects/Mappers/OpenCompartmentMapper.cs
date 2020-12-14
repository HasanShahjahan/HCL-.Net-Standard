using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.Entities.Locker;

namespace LSS.HCM.Core.DataObjects.Mappers
{
    public static class OpenCompartmentMapper
    {
        public static LockerDto ToError(this LockerDto model) 
        {
            return new LockerDto() 
            {
                StatusCode = model.StatusCode,
                Error = model.Error
            };
        }
        public static LockerDto ToObject(this Locker model)
        {
            return new LockerDto()
            {
                TransactionId = model.TransactionId,
                Compartments = model.Compartments,
                StatusCode = StatusCode.Status200OK,
                Error= null
            };
        }
    }
}
