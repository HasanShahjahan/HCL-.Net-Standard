using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Mappers
{
    public static class CompartmentStatusMapper
    {
        public static CompartmentStatusDto ToError(this CompartmentStatusDto model)
        {
            return new CompartmentStatusDto()
            {
                StatusCode = model.StatusCode,
                Error = model.Error
            };
        }

        public static CompartmentStatusDto ToObject(this List<Entities.Locker.Compartment> compartments)
        {
            return new CompartmentStatusDto()
            {
                Compartments = compartments,
                StatusCode = StatusCode.Status200OK,
                Error = null
            };
        }
    }
}
